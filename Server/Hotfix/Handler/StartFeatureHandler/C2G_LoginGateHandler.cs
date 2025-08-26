using System;
using System.Net;
using ETModel;
using MongoDB.Bson.Serialization;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
    {
        protected override async void Run(Session session, C2G_LoginGate message, Action<G2C_LoginGate> reply)
        {
            G2C_LoginGate response = new G2C_LoginGate();
            StartConfigComponent startComponent = Game.Scene.GetComponent<StartConfigComponent>();
            GateSessionKeyComponent gateSessionKeyComponent = Game.Scene.GetComponent<GateSessionKeyComponent>();
            try
            {
                long uid = gateSessionKeyComponent.Get(message.Key);
                if (uid <= 0)
                {
                    response.Error = ErrorCode.ERR_ConnectGateKeyError;
                    response.Message = "Gate key驗證失敗!";
                    reply(response);
                    return;
                }
                // 登入成功，刪除Gate的Key
                gateSessionKeyComponent.Remove(message.Key);
                int lobbyAppId = 0;
                var player = CacheHelper.GetFromCache<Player>(uid);

                if (player != null && player.lobbyAppId != 0)
                {
                    lobbyAppId = player.lobbyAppId;
                    // 為了防止舊的Session觸發Dispose會影響到斷線後的重連的SessionPlayerComponent.gateSessionActorId
                    //player.gateSessionActorId = session.InstanceId;
                    //await CacheExHelper.UpdateInCache(player);
                }
                else
                {
                    lobbyAppId = SessionHelper.GetLobbyIdRandomly();
                }
                // 隨機連接到Lobby伺服器，並創建PlayerUnit實體
                G2L_LobbyUnitCreate g2L_LobbyUnitCreate = new G2L_LobbyUnitCreate
                {
                    Uid = uid,
                    GateSessionId = session.InstanceId,
                    GateAppId = (int)IdGenerater.AppId,
                    LobbyAppId = lobbyAppId
                };

                // 等候LobbyUnit單元創建完成，並且也同步Player到了Gate
                Session lobbySession = SessionHelper.GetLobbySession(g2L_LobbyUnitCreate.LobbyAppId);
                L2G_LobbyUnitCreate l2G_LobbyUnitCreate = (L2G_LobbyUnitCreate)await lobbySession.Call(g2L_LobbyUnitCreate);
                if (l2G_LobbyUnitCreate.Error != ErrorCode.ERR_Success)
                {
                    response.Error = l2G_LobbyUnitCreate.Error;
                    reply(response);
                    return;
                }

                player = BsonSerializer.Deserialize<Player>(l2G_LobbyUnitCreate.Json);
                CacheExHelper.WriteInCache(player, out player);
                var sessionComponent = session.GetComponent<SessionPlayerComponent>();
                if (sessionComponent == null)
                {
                    session.AddComponent<SessionPlayerComponent, long>(player.gateSessionActorId).Player = player;
                }
                else
                {
                    sessionComponent.Player = player;
                }
                var mailBoxComponent = session.GetComponent<MailBoxComponent>();
                if (mailBoxComponent == null)
                {
                    session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);
                }

                response.PlayerId = player.Id;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}