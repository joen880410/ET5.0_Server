using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using MongoDB.Bson;
using System.Reflection;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class ConsoleComponentAwakeSystem : StartSystem<ConsoleComponent>
    {
        public override void Start(ConsoleComponent self)
        {
            self.Start().Coroutine();
        }
    }

    [ObjectSystem]
    public class ConsoleComponentLoadSystem : LoadSystem<ConsoleComponent>
    {
        public override void Load(ConsoleComponent self)
        {
            self.Load();
        }
    }

    public static class ConsoleMode
    {
        public const string None = "";
        public const string Repl = "repl";
    }

    public enum CreateUserMode
    {
        Random,
    }

    public enum DeleteUserMode
    {
        AllTestPlayer,
    }

    public class Range<T> where T : struct
    {
        public T Max { get; }

        public T Min { get; }

        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }

    public class ConsoleComponent : Entity
    {
        public CancellationTokenSource CancellationTokenSource;

        public string Mode = "";

        private Dictionary<string, List<IConsoleHandler>> allConsoles = new Dictionary<string, List<IConsoleHandler>>();

        private const string notSupportedMessage = "the command is not supported on the server";

        public void Load()
        {
            allConsoles.Clear();
            var types = Game.EventSystem.GetTypes(typeof(ConsoleAttribute));
            var startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(ConsoleAttribute), false);
                ConsoleAttribute consoleHandlerAttribute = (ConsoleAttribute)attrs.First();
                object obj = Activator.CreateInstance(type);
                IConsoleHandler iConsoleHandler = obj as IConsoleHandler;
                if (iConsoleHandler == null)
                {
                    Log.Error($"{obj.GetType().Name} 沒有繼承IConsoleHandler");
                }
                iConsoleHandler.ConsoleService = consoleHandlerAttribute.ConsoleService;
                iConsoleHandler.AppType = consoleHandlerAttribute.AppType;
                iConsoleHandler.CommandPatternString = consoleHandlerAttribute.CommandPattern;
                iConsoleHandler.parameterTypes = type.GetMethod("Execute").GetParameters().Select(e => e.ParameterType).ToArray();

                var appType = startConfigComponent.StartConfig.AppType;
                if (appType == AppType.AllServer)
                {
                    this.RegisterEvent(consoleHandlerAttribute.CommandPattern, iConsoleHandler);
                }
                else if (((int)consoleHandlerAttribute.AppType & (int)appType) == (int)appType)
                {
                    this.RegisterEvent(consoleHandlerAttribute.CommandPattern, iConsoleHandler);
                }
            }

            allConsoles = allConsoles.OrderByDescending(e => e.Key).ToDictionary(e => e.Key, e => e.Value);
        }

        public void RegisterEvent(string eventId, IConsoleHandler e)
        {
            if (!this.allConsoles.ContainsKey(eventId))
            {
                this.allConsoles.Add(eventId, new List<IConsoleHandler>());
            }
            this.allConsoles[eventId].Add(e);
        }

        public async ETVoid Start()
        {
            this.CancellationTokenSource = new CancellationTokenSource();
            this.Load();

            while (true)
            {
                string line = string.Empty;
                try
                {
                    line = await Task.Factory.StartNew(() =>
                    {
                        Console.Write($"{this.Mode}> ");
                        return Console.In.ReadLine();
                    }, this.CancellationTokenSource.Token);

                    line = line.Trim();

                    if (this.Mode != "")
                    {
                        bool isExited = true;
                        switch (this.Mode)
                        {
                            case ConsoleMode.Repl:
                                {
                                    ReplComponent replComponent = this.GetComponent<ReplComponent>();
                                    if (replComponent == null)
                                    {
                                        Console.WriteLine($"no command: {line}!");
                                        break;
                                    }

                                    try
                                    {
                                        isExited = await replComponent.Run(line, this.CancellationTokenSource.Token);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                }
                        }

                        if (isExited)
                        {
                            this.Mode = "";
                        }

                        continue;
                    }
                    await BroadcastCommand(line.ToLower());


                }
                catch (Exception e) when (e.GetType() != typeof(InvalidOperationException))
                {
                    Console.WriteLine(e);
                }
            }
        }

        public async ETTask<int> BroadcastCommand(string line)
        {
            if (line == "")
            {
                Console.WriteLine("Please enter command");
                return ErrorCode.ERR_ConsoleNotSupported;
            }

            var types = Game.EventSystem.GetTypes(typeof(ConsoleAttribute));
            var kp = allConsoles.FirstOrDefault(pair => Regex.IsMatch(line, OtherHelper.WildCardToRegular(pair.Key)));
            if (string.IsNullOrEmpty(kp.Key))
            {
                Console.WriteLine($"no such command: {line}");
                return ErrorCode.ERR_ConsoleNotSupported;
            }
            var Pattern = kp.Key;

            NetInnerComponent netInnerComponent = Game.Scene.GetComponent<NetInnerComponent>();
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            InnerConfig innerConfig = startConfigComponent.MasterConfig.GetComponent<InnerConfig>();
            AppType Apptype = kp.Value.First().AppType;
            int AppID = startConfigComponent.StartConfig.AppId;
            Session masterSession = netInnerComponent.Get(innerConfig.IPEndPoint);
            M2S_DispatchCommand m2S_DispatchCommand = (M2S_DispatchCommand)await masterSession.Call(new S2M_DispatchCommand
            {
                Command = line,
                Apptype = (int)Apptype,
                ConsoleService = (int)kp.Value[0].ConsoleService,
                AppId = AppID
            });
            if (m2S_DispatchCommand.Error == ErrorCode.ERR_Success)
            {
                Console.WriteLine($"{line}:ok");
                Console.WriteLine($"{m2S_DispatchCommand.Message}");
            }
            else
            {
                Console.WriteLine($"Error Code:{m2S_DispatchCommand.Error} Msg:{m2S_DispatchCommand.Message}");
            }

            return m2S_DispatchCommand.Error;
        }
        public async ETTask BroadcastCommandOnClassValue<T>(string line, params T[] value) where T : class
        {
            Queue<T> qValue = new Queue<T>(value);
            if (line == "")
            {
                Console.WriteLine("Please enter command");
                return;
            }
            var argv = Enumerable.Union(line.Split(' ').Where(e => !string.IsNullOrEmpty(e)), Enumerable.Repeat("", 8)).ToList();
            for (int i = 0; i < argv.Count; i++)
            {
                if (argv[i] == "*")
                {
                    argv[i] = argv[i].Replace("*", qValue.Dequeue().ToString());
                }
            }
            await BroadcastCommand(argv.ListToString(true, " "));
        }
        public async ETTask BroadcastCommandOnStructValue<T>(string line, params T[] value) where T : struct
        {
            Queue<T> qValue = new Queue<T>(value);
            if (line == "")
            {
                Console.WriteLine("Please enter command");
                return;
            }
            var argv = Enumerable.Union(line.Split(' ').Where(e => !string.IsNullOrEmpty(e)), Enumerable.Repeat("", 8)).ToList();
            for (int i = 0; i < argv.Count; i++)
            {
                if (argv[i] == "*")
                {
                    argv[i] = argv[i].Replace("*", qValue.Dequeue().ToString());
                }
            }
            await BroadcastCommand(argv.ListToString(true, " "));
        }
        private class NoSuchCommandException : Exception
        {
            public string line { private set; get; }

            public NoSuchCommandException(string line)
            {
                this.line = line;
            }
        }

        public async ETTask<ConsoleResult> ExecuteCommand(string line)
        {
            try
            {
                string Pattern = null;
                string[] argv = line.Split(' ').Where(e => !string.IsNullOrEmpty(e)).ToArray();
                var types = Game.EventSystem.GetTypes(typeof(ConsoleAttribute));

                var kp = allConsoles.FirstOrDefault(pair => Regex.IsMatch(line, OtherHelper.WildCardToRegular(pair.Key)));

                if (string.IsNullOrEmpty(kp.Key))
                {
                    return ConsoleResult.Error(msg: $"no such command: {line}");
                }
                Pattern = kp.Key;

                string[] argv2 = Pattern.Split(' ').ToArray();
                var tasks = new ETTask<ConsoleResult>[kp.Value.Count];

                for (int x = 0; x < kp.Value.Count; x++)
                {
                    var type = kp.Value[x].GetType();
                    object obj = Activator.CreateInstance(type);
                    MethodInfo method = type.GetMethods().FirstOrDefault();
                    var parameters = method.GetParameters();
                    object[] parametersA = new object[kp.Value[x].parameterTypes.Length];
                    var j = argv2.Count(e => e != "*");
                    for (int a = 0; a < parametersA.Length; a++)
                    {
                        if (argv[j + a].ParseType(parameters[a].ParameterType, out var value))
                        {
                            parametersA[a] = value;
                            continue;
                        }
                        return ConsoleResult.Error(msg: $"parseTypeError:{argv[j]},parseType:{parameters[a].ParameterType.Name}");
                    }
                    tasks[x] = (ETTask<ConsoleResult>)method.Invoke(obj, parametersA);
                }
                var result = await ETTask.WaitAll(tasks);
                if (result.Any(e => e.ErrorCode != ErrorCode.ERR_Success))
                    return ConsoleResult.Error(msg: result.Select(e => e.Message).ToArray().ToJson());
                else
                    return ConsoleResult.Ok(msg: result.Select(e => e.Message).ToArray().ToJson());
            }
            catch (Exception e)
            {
                if (e is NoSuchCommandException noSuchCommandException)
                {
                    return ConsoleResult.Error(msg: $"no such command: {noSuchCommandException.line}");
                }
                else if (e is IndexOutOfRangeException indexOutOfRangeException)
                {
                    return ConsoleResult.Error(msg: $"no such command: {line}");
                }
                else
                {
                    return ConsoleResult.Error(msg: $"Message:{e.Message}\r\nStackTrace:{e.StackTrace}");
                }
            }
        }
        public async ETTask<ConsoleResult> Run<A, B, C, D>(string type, A a, B b, C c, D d)
        {
            if (!this.allConsoles.TryGetValue(type, out List<IConsoleHandler> iConsoleHandlers))
            {
                return await ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            ETTask<ConsoleResult>[] tasks = new ETTask<ConsoleResult>[iConsoleHandlers.Count];
            for (int i = 0; i < iConsoleHandlers.Count; i++)
            {
                IConsoleHandler iConsoleHandler = iConsoleHandlers[i];
                tasks[i] = iConsoleHandler != null ? iConsoleHandler.Handle(a, b, c, d) : ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            var results = await ETTask.WaitAll(tasks);
            var consoleResult = new ConsoleResult();
            var messages = new List<string>();
            var error = false;
            for (int i = 0, j = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result.ErrorCode == ErrorCode.ERR_ConsoleNotImplement)
                {
                    continue;
                }
                if (!error && result.ErrorCode != ErrorCode.ERR_Success)
                {
                    error = true;
                    consoleResult.ErrorCode = ErrorCode.ERR_ConsoleError;
                }
                messages.Add($"Command:{type}[Number:{j}]> ErrorCode:{result.ErrorCode}, Message:{result.Message}");
                consoleResult.info = result.info;
                j++;
            }
            consoleResult.Message = string.Join("\r\n", messages);
            return consoleResult;
        }
        public async ETTask<ConsoleResult> Run<A, B, C>(string type, A a, B b, C c)
        {
            if (!this.allConsoles.TryGetValue(type, out List<IConsoleHandler> iConsoleHandlers))
            {
                return await ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            ETTask<ConsoleResult>[] tasks = new ETTask<ConsoleResult>[iConsoleHandlers.Count];
            for (int i = 0; i < iConsoleHandlers.Count; i++)
            {
                IConsoleHandler iConsoleHandler = iConsoleHandlers[i];
                tasks[i] = iConsoleHandler != null ? iConsoleHandler.Handle(a, b, c) : ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            var results = await ETTask.WaitAll(tasks);
            var consoleResult = new ConsoleResult();
            var messages = new List<string>();
            var error = false;
            for (int i = 0, j = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result.ErrorCode == ErrorCode.ERR_ConsoleNotImplement)
                {
                    continue;
                }
                if (!error && result.ErrorCode != ErrorCode.ERR_Success)
                {
                    error = true;
                    consoleResult.ErrorCode = ErrorCode.ERR_ConsoleError;
                }
                messages.Add($"Command:{type}[Number:{j}]> ErrorCode:{result.ErrorCode}, Message:{result.Message}");
                consoleResult.info = result.info;
                j++;
            }
            consoleResult.Message = string.Join("\r\n", messages);
            return consoleResult;
        }
        public async ETTask<ConsoleResult> Run<A, B>(string type, A a, B b)
        {
            if (!this.allConsoles.TryGetValue(type, out List<IConsoleHandler> iConsoleHandlers))
            {
                return await ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            ETTask<ConsoleResult>[] tasks = new ETTask<ConsoleResult>[iConsoleHandlers.Count];
            for (int i = 0; i < iConsoleHandlers.Count; i++)
            {
                IConsoleHandler iConsoleHandler = iConsoleHandlers[i];
                tasks[i] = iConsoleHandler != null ? iConsoleHandler.Handle(a, b) : ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            var results = await ETTask.WaitAll(tasks);
            var consoleResult = new ConsoleResult();
            var messages = new List<string>();
            var error = false;
            for (int i = 0, j = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result.ErrorCode == ErrorCode.ERR_ConsoleNotImplement)
                {
                    continue;
                }
                if (!error && result.ErrorCode != ErrorCode.ERR_Success)
                {
                    error = true;
                    consoleResult.ErrorCode = ErrorCode.ERR_ConsoleError;
                }
                messages.Add($"Command:{type}[Number:{j}]> ErrorCode:{result.ErrorCode}, Message:{result.Message}");
                consoleResult.info = result.info;
                j++;
            }
            consoleResult.Message = string.Join("\r\n", messages);
            return consoleResult;
        }
        public async ETTask<ConsoleResult> Run<A>(string type, A a)
        {
            if (!this.allConsoles.TryGetValue(type, out List<IConsoleHandler> iConsoleHandlers))
            {
                return await ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            ETTask<ConsoleResult>[] tasks = new ETTask<ConsoleResult>[iConsoleHandlers.Count];
            for (int i = 0; i < iConsoleHandlers.Count; i++)
            {
                IConsoleHandler iConsoleHandler = iConsoleHandlers[i];
                tasks[i] = iConsoleHandler != null ? iConsoleHandler.Handle(a) : ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            var results = await ETTask.WaitAll(tasks);
            var consoleResult = new ConsoleResult();
            var messages = new List<string>();
            var error = false;
            for (int i = 0, j = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result == default)
                {
                    continue;
                }
                if (result.ErrorCode == ErrorCode.ERR_ConsoleNotImplement)
                {
                    continue;
                }
                if (!error && result.ErrorCode != ErrorCode.ERR_Success)
                {
                    error = true;
                    consoleResult.ErrorCode = ErrorCode.ERR_ConsoleError;
                }
                messages.Add($"Command:{type}[Number:{j}]> ErrorCode:{result.ErrorCode}, Message:{result.Message}");
                consoleResult.info = result.info;
                j++;
            }
            consoleResult.Message = string.Join("\r\n", messages);
            return consoleResult;
        }
        public async ETTask<ConsoleResult> Run(string type)
        {
            if (!this.allConsoles.TryGetValue(type, out List<IConsoleHandler> iConsoleHandlers))
            {
                return await ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            ETTask<ConsoleResult>[] tasks = new ETTask<ConsoleResult>[iConsoleHandlers.Count];
            for (int i = 0; i < iConsoleHandlers.Count; i++)
            {
                IConsoleHandler iConsoleHandler = iConsoleHandlers[i];
                tasks[i] = iConsoleHandler != null ? iConsoleHandler.Handle() : ETTask.FromResult(ConsoleResult.Error(msg: notSupportedMessage));
            }
            var results = await ETTask.WaitAll(tasks);
            var consoleResult = new ConsoleResult();
            var messages = new List<string>();
            var error = false;
            for (int i = 0, j = 0; i < results.Length; i++)
            {
                var result = results[i];
                if (result.ErrorCode == ErrorCode.ERR_ConsoleNotImplement)
                {
                    continue;
                }
                if (!error && result.ErrorCode != ErrorCode.ERR_Success)
                {
                    error = true;
                    consoleResult.ErrorCode = ErrorCode.ERR_ConsoleError;
                }
                messages.Add($"Command:{type}[Number:{j}]> ErrorCode:{result.ErrorCode}, Message:{result.Message}");
                consoleResult.info = result.info;
                j++;
            }
            consoleResult.Message = string.Join("\r\n", messages);

            return consoleResult;
        }

        public bool GetAutoCompleteCommand(string commandStart, out string command)
        {
            command = string.Empty;

            var match = allConsoles.Keys.Where(item => item != commandStart && item.StartsWith(commandStart)).ToList();

            if (match.Count == 1)
            {
                command = match.FirstOrDefault();
                return true;
            }
            else if (match.Count > 1)
            {
                var str = new StringBuilder();
                str.Append("\n");
                for (int i = 0; i < match.Count; i++)
                {
                    str.Append($"{match[i]}   ");
                }
                Console.WriteLine($"{str}");
            }
            ClearCurrentLine();
            return false;
        }
        private static void ClearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }

    }
}
