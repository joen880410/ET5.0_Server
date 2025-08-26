using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class R2D_AuthenticateHttpHandler : AMRpcHandler<R2D_AuthenticateHttp, D2R_AuthenticateHttp>
    {
        protected override async void Run(Session session, R2D_AuthenticateHttp message, Action<D2R_AuthenticateHttp> reply)
        {
            D2R_AuthenticateHttp response = new D2R_AuthenticateHttp();
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
