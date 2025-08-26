using System;
using System.Collections.Generic;
using ETModel;
using Google.Protobuf.Collections;

namespace ETHotfix
{
    [MessageHandler(AppType.Http)]
    public class R2H_AuthenticateHttpHandler : AMRpcHandler<R2H_AuthenticateHttp, H2R_AuthenticateHttp>
    {
        protected override async void Run(Session session, R2H_AuthenticateHttp message, Action<H2R_AuthenticateHttp> reply)
        {
            H2R_AuthenticateHttp response = new H2R_AuthenticateHttp();
            try
            {
                string token = message.Token;
                if (string.IsNullOrEmpty(token))
                {
                    User user = await UserDataHelper.FindOneUser(message.Uid);
                    SignInCryptographyHelper.Token tok = new SignInCryptographyHelper.Token
                    {
                        uid = user.Id,
                        lastCreateTokenAt = user.lastCreateTokenAt,
                        salt = user.salt,
                    };

                    token = SignInCryptographyHelper.EncodeToken(tok);
                }
                Game.Scene.GetComponent<HttpComponent>().AddIntoWhiteMap(token, message.Uid);
                response.Error = ErrorCode.ERR_Success;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
