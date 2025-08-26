using HttpMultipartParser;
using Microsoft.IO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ETModel
{


    public class ParseFormData
    {
        public string contentDisposition = "";

        public string name = "";

        public string fileName = "";

        public string contentType = "";

        public byte[] data = null;

        private Encoding encoding;

        public ParseFormData(Encoding enc)
        {
            encoding = enc;
        }

        public string GetString()
        {
            if (data == null)
                return string.Empty;
            return encoding.GetString(data);
        }
    }

    public enum HttpMethod
    {
        Web = 1,
        Jmarket = 2,
        App = 3,
        Server = 4
    }

    public enum ApiType
    {
        Web = 1,
        Jmarket = 2,
        App = 3,
        Server = 4
    }
    /// <summary>
    /// http请求分发器
    /// </summary>
    public class HttpComponent : Component
    {
        public AppType appType;
        public HttpListener listener;
        public HttpConfig HttpConfig;
        public static string JsportsAPIKey => Game.Scene.GetComponent<HttpComponent>().HttpConfig.JSportsConfig.AppKey;
        public static string JsportsHttpURL => Game.Scene.GetComponent<HttpComponent>().HttpConfig.JSportsConfig.Url;
        public readonly MultiMap<string, IHttpHandler> dispatcher = new MultiMap<string, IHttpHandler>();

        // 处理方法
        private readonly Dictionary<MethodInfo, IHttpHandler> handlersMapping = new Dictionary<MethodInfo, IHttpHandler>();

        // Get处理
        private readonly Dictionary<string, (ApiType, MethodInfo)> getHandlers = new Dictionary<string, (ApiType, MethodInfo)>();
        private readonly Dictionary<string, (bool, ApiType, MethodInfo)> postHandlers = new Dictionary<string, (bool, ApiType, MethodInfo)>();

        // 白名單
        public Dictionary<string, long> appWhiteMap { private set; get; } = new Dictionary<string, long>();
        // 授權key
        public const string authenticateKey = "Authenticate-Key";
        // 授權uid
        public const string authenticateUid = "Authenticate-Uid";
        //Web請求方式
        public const string authenticateWeb = "User-Agent";
        //請求來源
        public const string authenticateType = "Client-Type";


        private RecyclableMemoryStreamManager MemoryStreamManager = new RecyclableMemoryStreamManager();

        private const long bodySizeLimitMB = 50;

        private const long bodySizeLimit = bodySizeLimitMB * 1000 * 1000;

        public void Awake()
        {
            StartConfig startConfig = StartConfigComponent.Instance.StartConfig;
            this.appType = startConfig.AppType;
            this.HttpConfig = startConfig.GetComponent<HttpConfig>();
            this.Load();
        }

        public void Load()
        {
            List<Type> types = Game.EventSystem.GetTypes(typeof(HttpHandlerAttribute));

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(HttpHandlerAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                HttpHandlerAttribute httpHandlerAttribute = (HttpHandlerAttribute)attrs[0];
                if (!httpHandlerAttribute.AppType.Is(this.appType))
                {
                    continue;
                }

                object obj = Activator.CreateInstance(type);

                IHttpHandler ihttpHandler = obj as IHttpHandler;
                if (ihttpHandler == null)
                {
                    throw new Exception($"HttpHandler handler not inherit IHttpHandler class: {obj.GetType().FullName}");
                }

                this.dispatcher.Add(httpHandlerAttribute.Path, ihttpHandler);

                LoadMethod(type, httpHandlerAttribute, ihttpHandler);
            }
        }

        public void Start()
        {
            try
            {
                this.listener = new HttpListener();

                if (this.HttpConfig.Url == null)
                {
                    this.HttpConfig.Url = "";
                }

                foreach (string s in this.HttpConfig.Url.Split(';'))
                {
                    if (s.Trim() == "")
                    {
                        continue;
                    }

                    this.listener.Prefixes.Add(s);
                }

                this.listener.Start();

                this.Accept().Coroutine();
            }
            catch (HttpListenerException e)
            {
                if (e.ErrorCode == 5)
                {
                    throw new Exception($"CMD管理員輸入: netsh http add urlacl url = {this.HttpConfig.Url.Split(';').FirstOrDefault()} user = Everyone", e);
                }
                Log.Error(e);
            }
            catch (Exception e)
            {

            }
        }



        public void LoadMethod(Type type, HttpHandlerAttribute httpHandlerAttribute, IHttpHandler httpHandler)
        {
            // 扫描这个类里面的方法
            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance);
            foreach (MethodInfo method in methodInfos)
            {
                object[] getAttrs = method.GetCustomAttributes(typeof(GetAttribute), false);
                if (getAttrs.Length != 0)
                {
                    GetAttribute get = (GetAttribute)getAttrs[0];
                    getHandlers.Add(httpHandlerAttribute.Path, (get.apiType, method));
                }

                object[] postAttrs = method.GetCustomAttributes(typeof(PostAttribute), false);
                if (postAttrs.Length != 0)
                {
                    // Post处理方法
                    PostAttribute post = (PostAttribute)postAttrs[0];
                    postHandlers.Add(httpHandlerAttribute.Path, (post.IsAuthorized, post.apiType, method));
                }

                if (getAttrs.Length == 0 && postAttrs.Length == 0)
                {
                    continue;
                }

                handlersMapping.Add(method, httpHandler);
            }
        }

        public async ETVoid Accept()
        {
            long instanceId = this.InstanceId;

            while (true)
            {
                if (this.InstanceId != instanceId)
                {
                    return;
                }

                try
                {
                    HttpListenerContext context = await this.listener.GetContextAsync();
                    HandlePacket(context).Coroutine();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private async ETTask HandlePacket(HttpListenerContext context)
        {
            await InvokeHandler(context);
            context.Response.Close();
        }

        /// <summary>
        /// 调用处理方法
        /// </summary>
        /// <param name="context"></param>
        private async ETTask InvokeHandler(HttpListenerContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            // 大小約束
            // 如果有body
            if (context.Request.HasEntityBody)
            {
                if (context.Request.ContentLength64 > bodySizeLimit)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;
                    return;
                }
            }

            if (!Enum.TryParse(context.Request.Headers.Get(authenticateType), out ApiType clientType))
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                return;
            }

            MethodInfo methodInfo = null;
            IHttpHandler httpHandler = null;
            object postbody = null;
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    this.getHandlers.TryGetValue(context.Request.Url.AbsolutePath, out var getValue);
                    methodInfo = getValue.Item2;
                    if (clientType != getValue.Item1)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.RequestedRangeNotSatisfiable;
                        break;
                    }
                    if (methodInfo != null)
                    {
                        this.handlersMapping.TryGetValue(methodInfo, out httpHandler);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    }
                    break;
                case "POST":
                    this.postHandlers.TryGetValue(context.Request.Url.AbsolutePath, out var postValue);
                    methodInfo = postValue.Item3;
                    if (methodInfo != null)
                    {
                        if (clientType != postValue.Item2)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.RequestedRangeNotSatisfiable;
                        }
                        if (postValue.Item1)
                        {
                            var result = IsAuth(clientType, context);
                            if (!result)
                            {
                                return;
                            }
                        }
                        this.handlersMapping.TryGetValue(methodInfo, out httpHandler);
                        // 用Content-Type分類
                        var contentType = "";
                        if (!string.IsNullOrEmpty(context.Request.ContentType))
                        {
                            contentType = context.Request.ContentType;
                            contentType = contentType.Split(';')[0];
                        }
                        switch (contentType)
                        {
                            case "multipart/form-data":
                                // 高耗效能運算給執行緒代管，以防止阻塞
                                ETTaskCompletionSource<MultipartFormDataParser> formDataTcs = new ETTaskCompletionSource<MultipartFormDataParser>();
                                ThreadPool.QueueUserWorkItem(e =>
                                {
                                    // 執行緒例外如果沒catch，會導致進程崩潰
                                    try
                                    {
                                        var result = MultipartFormDataParser.Parse(context.Request.InputStream, context.Request.ContentEncoding);
                                        OneThreadSynchronizationContext.Instance.Post(r => { formDataTcs.TrySetResult((MultipartFormDataParser)r); }, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex);
                                        OneThreadSynchronizationContext.Instance.Post(r => { formDataTcs.TrySetResult((MultipartFormDataParser)r); }, null);
                                    }
                                }, context);

                                await formDataTcs.Task;
                                MultipartFormDataParser list = formDataTcs.Task.Result;
                                Dictionary<string, ParseFormData> dict = new Dictionary<string, ParseFormData>();

                                for (int i = 0; i < list?.Files.Count; i++)
                                {
                                    var file = list.Files[i];
                                    var rawMemoryStream = MemoryStreamManager.GetStream("httpRawMessage");
                                    rawMemoryStream.SetLength(0);
                                    using (Stream stream = file.Data)
                                    {
                                        stream.CopyTo(rawMemoryStream);
                                        dict.TryAdd(file.Name.ToLower(), new ParseFormData(context.Request.ContentEncoding)
                                        {
                                            data = rawMemoryStream.ToArray(),
                                            fileName = file.FileName
                                        });
                                    }

                                }

                                for (int i = 0; i < list?.Parameters.Count; i++)
                                {
                                    var parameter = list.Parameters[i];

                                    dict.TryAdd(parameter.Name.ToLower(), new ParseFormData(context.Request.ContentEncoding)
                                    {
                                        data = parameter.Data.ToByteArray(context.Request.ContentEncoding),
                                        contentType = methodInfo.GetParameterByName(parameter.Name)?.ParameterType.Name
                                    });
                                }
                                postbody = dict;
                                break;
                            default:
                                // 高耗效能運算給執行緒代管，以防止阻塞
                                ETTaskCompletionSource<string> defaultTcs = new ETTaskCompletionSource<string>();
                                ThreadPool.QueueUserWorkItem(e =>
                                {
                                    // 執行緒例外如果沒catch，會導致進程崩潰
                                    try
                                    {
                                        StreamReader sr = new StreamReader(context.Request.InputStream);
                                        var result = sr.ReadToEnd();
                                        sr.Dispose();
                                        OneThreadSynchronizationContext.Instance.Post(r => { defaultTcs.TrySetResult((string)r); }, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex);
                                        OneThreadSynchronizationContext.Instance.Post(r => { defaultTcs.TrySetResult((string)r); }, null);
                                    }
                                }, context);
                                await defaultTcs.Task;
                                postbody = defaultTcs.Task.Result;
                                break;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    }

                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    break;
            }

            if (httpHandler != null)
            {
                object[] args = InjectParameters(context, methodInfo, postbody);

                // 自动把返回值，以json方式响应。
                object resp = methodInfo.Invoke(httpHandler, args);
                object result = resp;
                if (resp is ETTask<HttpUtility.HttpResult> t)
                {
                    await t;
                    result = t.GetType().GetProperty("Result").GetValue(t, null);
                }

                if (result != null)
                {
                    using (StreamWriter sw = new StreamWriter(context.Response.OutputStream, Encoding.UTF8))
                    {
                        if (result.GetType() == typeof(string))
                        {
                            sw.Write(result.ToString());
                        }
                        else
                        {
                            sw.Write(JsonHelper.ToJson(result));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 注入参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodInfo"></param>
        /// <param name="postbody"></param>
        /// <returns></returns>
        private static object[] InjectParameters(HttpListenerContext context, MethodInfo methodInfo, object postbody)
        {
            context.Response.StatusCode = 200;
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            object[] args = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                ParameterInfo item = parameterInfos[i];

                if (item.ParameterType == typeof(HttpListenerRequest))
                {
                    args[i] = context.Request;
                    continue;
                }

                if (item.ParameterType == typeof(HttpListenerResponse))
                {
                    args[i] = context.Response;
                    continue;
                }

                try
                {
                    switch (context.Request.HttpMethod)
                    {
                        case "POST":
                            if (postbody is Dictionary<string, ParseFormData> parseFormDataList)
                            {
                                parseFormDataList.TryGetValue(item.Name.ToLower(), out var parseFormData);
                                if (item.ParameterType == typeof(ParseFormData))
                                    args[i] = parseFormData;
                                else if (item.ParameterType.Name == parseFormData.contentType)
                                {
                                    args[i] = JsonHelper.FromJson(item.ParameterType, parseFormData.GetString());
                                }
                            }
                            else if (postbody is string postbodyString)
                            {
                                if (item.Name == "postBody") // 约定参数名称为postBody,只传string类型。本来是byte[]，有需求可以改。
                                {
                                    args[i] = postbodyString;
                                }
                                else if (item.ParameterType.IsClass && item.ParameterType != typeof(string) && !string.IsNullOrEmpty(postbodyString))
                                {
                                    object entity = JsonHelper.FromJson(item.ParameterType, postbodyString);
                                    args[i] = entity;
                                }
                            }

                            break;
                        case "GET":
                            string query = context.Request.QueryString[item.Name];
                            if (query != null)
                            {
                                object value = Convert.ChangeType(query, item.ParameterType);
                                args[i] = value;
                            }

                            break;
                        default:
                            args[i] = null;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    args[i] = null;
                }
            }

            return args;
        }

        private bool IsAuth(ApiType apiType, HttpListenerContext context)
        {
            //判斷請求方式

            switch (apiType)
            {
                case ApiType.Web:
                    break;
                case ApiType.App:
                case ApiType.Server:
                    string key = context.Request.Headers.Get(authenticateKey);
                    // 沒傳送授權key一律阻擋
                    if (!string.IsNullOrEmpty(key))
                    {
                        var uid = GetFromWhiteMap(key);
                        // 沒找到uid一律阻擋
                        if (uid <= 0)
                        {
                            return false;
                        }
                        // 把uid塞到檔頭，方便傳遞
                        context.Request.Headers.Add(authenticateUid, uid.ToString());
                    }

                    return true;
                default:
                    return false;
            }
            return false;
        }

        public void AddIntoWhiteMap(string key, long uid)
        {
            try
            {
                if (!this.appWhiteMap.TryGetValue(key, out var value))
                {
                    this.appWhiteMap.Add(key, uid);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return;
            }
        }

        public long GetFromWhiteMap(string key)
        {
            try
            {
                this.appWhiteMap.TryGetValue(key, out var uid);
                return uid;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return OtherHelper.INVALID_ID;
            }
        }

        public void RemoveFromWhiteMap(string key)
        {
            try
            {
                this.appWhiteMap.Remove(key);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.listener.Stop();
            this.listener.Close();
        }

        public class OverMaxCountForIterationException : Exception
        {
            private string error;

            private string tag;

            public OverMaxCountForIterationException(string tag, Encoding enc, Stream rawMemoryStream)
            {
                Console.WriteLine($"{tag}");
                this.tag = tag;
                rawMemoryStream.Seek(0, SeekOrigin.Begin);
                rawMemoryStream.Seek(0, SeekOrigin.Current);
                error = enc.GetString(((MemoryStream)rawMemoryStream).ToArray());
            }

            public override string ToString()
            {
                return $"OverMaxCountForIterationException:{tag}:\r\n{error}";
            }
        }
    }
}