using System;
using System.Net.Http;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Http)]
    public class L2H_CreateLinkHttpHandler : AMRpcHandler<L2H_CreateLinkHttp, H2L_CreateLinkHttp>
    {
        protected override async void Run(Session session, L2H_CreateLinkHttp message, Action<H2L_CreateLinkHttp> reply)
        {
            var response = new H2L_CreateLinkHttp();
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent
                {
                    { new StringContent(message.JacfitUid.ToString()), "jacfituserId" },
                    { new StringContent(message.JSportUid.ToString()), "jsportuserId" },
                };
                client.DefaultRequestHeaders.Add("Client-Type", "Server");
                // 设置请求头中的Content-Type为application/json
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authenticate-Key", HttpComponent.JsportsAPIKey);
                await client.PostAsync(StringHelper.CombineText(HttpComponent.JsportsHttpURL, "/create/link/user"), multipartFormDataContent);

                response.Error = ErrorCode.ERR_Success;
                reply(response);
            }
        }
    }
}
