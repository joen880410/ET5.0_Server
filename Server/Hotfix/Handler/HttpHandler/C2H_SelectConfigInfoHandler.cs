using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [HttpHandler(AppType.DB, "/select/config")]
    public class C2H_SelectConfigInfoHandler : AHttpHandler
    {
        [Get(ApiType.App)]
        public async ETTask<HttpUtility.HttpResult> SelectConfigInfoHandle(HttpListenerRequest req, string fileNameStr)
        {
            await ETTask.CompletedTask;
            try
            {
                if (fileNameStr.IsEmpty())
                {
                    return Error(msg: $"Message: FileName is Empty");
                }

                var config = await ConfigDataHelper.FindConfig(fileNameStr);
                if (config == null)
                {
                    return Error(msg: $"Message: config is null");
                }

                return Ok(msg: config.context);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return Error(msg: $"Message: {e.Message}\r\nStackTrace: {e.StackTrace}");
            }
        }
    }
}
