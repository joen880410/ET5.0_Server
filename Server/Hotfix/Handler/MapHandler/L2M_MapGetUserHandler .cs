using System;
using System.Linq;
using ETHotfix.Share;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class L2M_MapGetUserHandler : AMRpcHandler<L2M_GetUserList, M2L_GetUserList>
    {
        protected override void Run(Session session, L2M_GetUserList message, Action<M2L_GetUserList> reply)
        {
            RunAsync(session, message, reply);
        }

        protected  void RunAsync(Session session, L2M_GetUserList message, Action<M2L_GetUserList> reply)
        {
            M2L_GetUserList response = new M2L_GetUserList();
            try
            {
                var Room = Game.Scene.GetComponent<RoomComponent>().Get(message.RoomId);
                if (Room == null)
                {
                    response.Error = ErrorCode.ERR_RoomIdNotFound;
                    reply(response);
                    return;
                }
                response.UserList = Room.GetAllUid().ToList();
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}