using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class SessionPlayerComponentAwakeSystem : AwakeSystem<SessionPlayerComponent, long>
    {
        public override void Awake(SessionPlayerComponent self, long gateSessionActorId)
        {
            self.gateSessionActorId = gateSessionActorId;
        }
    }

    [ObjectSystem]
    public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
    {
        public override async void Destroy(SessionPlayerComponent self)
        {
            if (!self.isAlive)
                return;
            Session lobbySession = SessionHelper.GetSession(self.Player.lobbyAppId);
            G2L_LobbyUnitUpdate g2L_LobbyUnitUpdate = new G2L_LobbyUnitUpdate();
            g2L_LobbyUnitUpdate.Uid = self.Player.uid;
            g2L_LobbyUnitUpdate.IsOnline = false;
            await lobbySession.Call(g2L_LobbyUnitUpdate);

            // 不是靠PingComponent中斷的需手動清除
            Game.Scene.GetComponent<PingComponent>().RemoveSession(self.gateSessionActorId);
        }
    }
    public static class SessionPlayerComponentExpansion
    {
        public static async ETTask ChangePlayerOnlineStatus(this SessionPlayerComponent self ,bool isOnline)
        {
            if (!self.isAlive)
                return;
            Session lobbySession = SessionHelper.GetSession(self.Player.lobbyAppId);
            G2L_LobbyUnitUpdate g2L_LobbyUnitUpdate = new G2L_LobbyUnitUpdate();
            g2L_LobbyUnitUpdate.Uid = self.Player.uid;
            g2L_LobbyUnitUpdate.IsOnline = isOnline;
            await lobbySession.Call(g2L_LobbyUnitUpdate);

        }
    }
}