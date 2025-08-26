using System;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Lobby)]
    public class C2L_LinkHandler : AMActorLocationRpcHandler<Player, C2L_Link, L2C_Link>
    {
        protected override async ETTask Run(Player player, C2L_Link message, Action<L2C_Link> reply)
        {
            await ETTask.CompletedTask;
            RunAsync(player, message, reply).Coroutine();
        }
        
        public async ETVoid RunAsync(Player player, C2L_Link request, Action<L2C_Link> reply)
        {
            L2C_Link response = new L2C_Link();
            try
            {
                if (request.Info == null)
                {
                    response.Error = ErrorCode.ERR_AuthenticationIsNull;
                }
                else
                {
                }
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
