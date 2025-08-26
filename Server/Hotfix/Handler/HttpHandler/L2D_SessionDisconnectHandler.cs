using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class L2D_SessionDisconnectHandler : AMHandler<L2D_SessionDisconnect>
    {
        protected override void Run(Session session, L2D_SessionDisconnect message)
        {
            RunAsync(session, message);
        }

        private async void RunAsync(Session session, L2D_SessionDisconnect message)
        {
            User user = await UserDataHelper.FindOneUser(message.Uid);
            SignInCryptographyHelper.Token tok = new SignInCryptographyHelper.Token
            {
                uid = user.Id,
                lastCreateTokenAt = user.lastCreateTokenAt,
                salt = user.salt,
            };

            string token = SignInCryptographyHelper.EncodeToken(tok);
            try
            {
                Game.Scene.GetComponent<HttpComponent>().RemoveFromWhiteMap(token);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
