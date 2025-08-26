using ETModel;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.Master)]
    public class S2M_DispatchCommandHandler : AMRpcHandler<S2M_DispatchCommand, M2S_DispatchCommand>
    {
        protected override async void Run(Session session, S2M_DispatchCommand message, Action<M2S_DispatchCommand> reply)
        {
            M2S_DispatchCommand response = new M2S_DispatchCommand();
            try
            {
                NetInnerComponent netInnerComponent = Game.Scene.GetComponent<NetInnerComponent>();
                StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
                StartConfig[] startConfigs = startConfigComponent.GetAllOnline();
                ETTask<IResponse>[] tasks = new ETTask<IResponse>[startConfigs.Length];
                StartConfig startConfig = null;
                AppType appType = (AppType)message.Apptype;
                bool isBreak = false;
                for (int i = 0; i < startConfigs.Length; i++)
                {
                    startConfig = startConfigs[i];
                    if (message.ConsoleService == (int)ConsoleService.Any)
                    {
                        // 該指令在同類型的任何一台服務器上執行
                        if (startConfig.AppType.Is(appType))
                        {
                            isBreak = true;
                        }
                        else
                            continue;
                    }
                    else if (message.ConsoleService == (int)ConsoleService.Self)
                    {
                        // 該指令只有自己的服務器能執行
                        if (startConfig.AppId == message.AppId)
                        {
                            isBreak = true;
                        }
                        else
                            continue;
                    }
                    else if (!startConfig.AppType.Is(appType))
                    {
                        // 該指令對全部同類型的服務器都執行
                        continue;
                    }

                    InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
                    Session serverSession = netInnerComponent.Get(innerConfig.IPEndPoint);
                    tasks[i] = serverSession.Call(new M2A_DispatchCommand
                    {
                        Command = message.Command,
                    });

                    if (isBreak)
                        break;
                }
                IResponse[] results = await ETTask.WaitAll(tasks, null);
                List<string> messages = new List<string>();
                bool error = false;
                for (int i = 0; i < results.Length; i++)
                {
                    startConfig = startConfigs[i];
                    if (results[i] == null)
                    {
                        continue;
                    }
                    A2M_DispatchCommand result = (A2M_DispatchCommand)results[i];
                    if (result.Error == ErrorCode.ERR_ConsoleNotSupported)
                    {
                        continue;
                    }
                    if (!error && result.Error != ErrorCode.ERR_Success)
                    {
                        error = true;
                        response.Error = ErrorCode.ERR_ConsoleError;
                    }
                    messages.Add($"Server[AppType:{startConfig.AppType}, AppId:{startConfig.AppId}]> msg:{result.Message}");
                }
                response.Message = string.Join("\r\n", messages);
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}