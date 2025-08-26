using System;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Lobby)]
    public class C2L_DeleteAccountHandler : AMActorLocationRpcHandler<Player, C2L_DeleteAccount, L2C_DeleteAccount>
    {
        protected override async ETTask Run(Player player, C2L_DeleteAccount message, Action<L2C_DeleteAccount> reply)
        {
            await ETTask.CompletedTask;
            RunAsync(player, message, reply).Coroutine();
        }

        private async ETVoid RunAsync(Player player, C2L_DeleteAccount message, Action<L2C_DeleteAccount> reply)
        {
            L2C_DeleteAccount response = new L2C_DeleteAccount();
            try
            {
                await UserDataHelper.DeleteAccount(player.uid);
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
