using System;
using System.Collections.Generic;
using ETModel;
using Google.Protobuf.Collections;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Lobby)]
    public class C2L_SyncPlayerStateHandler : AMActorLocationRpcHandler<Player, C2L_SyncPlayerState, L2C_SyncPlayerState>
    {
        protected override async ETTask Run(Player player, C2L_SyncPlayerState message, Action<L2C_SyncPlayerState> reply)
        {
            await RunAsync(player, message, reply);
        }

        private async ETTask RunAsync(Player player, C2L_SyncPlayerState message, Action<L2C_SyncPlayerState> reply)
        {
            L2C_SyncPlayerState response = new L2C_SyncPlayerState();
            try
            {
                var lobbyComponent = Game.Scene.GetComponent<LobbyComponent>();
                // 取得自身資料
                User user = await UserDataHelper.FindOneUser((player?.uid).GetValueOrDefault());
                if (user == null)
                {
                    response.Error = ErrorCode.ERR_AccountDoesntExist;
                    reply(response);
                    return;
                }
                response.Error = ErrorCode.ERR_SyncPlayerStateError;
                switch (message.StateData.Type)
                {
                    case PlayerStateData.Types.StateType.Start:
                        // 砍掉MapUnit
                        await lobbyComponent.DestroyMapUnit(player.mapUnitId);
                        response.Error = ErrorCode.ERR_Success;
                        break;
                    case PlayerStateData.Types.StateType.Lobby:
                        {
                            Room room = lobbyComponent.GetRoom(player.roomID);
                            if (room == null || room.state == (int)RoomState.End)
                            {
                                await lobbyComponent.DestroyMapUnit(player.mapUnitId);
                            }
                            // TODO 		
                            // Team = 2;  Map = 3; 問要不要回來
                            response.Type = L2C_SyncPlayerState.Types.OptionType.Nothing;
                            response.Error = ErrorCode.ERR_Success;
                        }
                        break;
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
