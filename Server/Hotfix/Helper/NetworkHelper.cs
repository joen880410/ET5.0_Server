using ETModel;
using System;

namespace ETHotfix
{
    public static class NetworkHelper
    {
        public static class DisconnectInfo
        {
            public const int Network_Delay = 0;
            public const int Network_Break = 1;
            public const int Server_Maintain = 2;
            public const int Multiple_Login = 3;
            public const int Version_Invalid = 4;
        }

        public static void OnHeartBeatFailed(long sessionId)
        {
            Game.Scene.GetComponent<NetOuterComponent>().Remove(sessionId);
            Game.Scene.GetComponent<NetInnerComponent>().Remove(sessionId);

            Log.Info($"{DateTime.UtcNow}:SessionID:{sessionId} has died");
        }

        public static void DisconnectSession(long uid, int disconnectInfo)
        {
            G2C_ForceDisconnect message = new G2C_ForceDisconnect()
            {
                DisconnectInfo = disconnectInfo
            };
            GateMessageHelper.BroadcastTarget(message, uid);

            Log.Info($"SendForceDisconnect> uid:{uid}, disconnectInfo:{disconnectInfo}");
        }

        public static async void DisconnectPlayer(Player player)
        {
            if (player == null)
                return;

            // 向MapServer發送斷線訊息
            if (player.InRoom())
            {
                ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(player.mapUnitId);
                actorLocationSender.Send(new L2M_SessionDisconnect());
            }
            player.isOnDisconnectingStage = false;
            //刪除 Player
            await Game.Scene.GetComponent<PlayerComponent>().Remove(player.uid);
            //清除HTTP的授權
            SessionHelper.GetHttpSession().Send(new L2H_SessionDisconnect()
            {
                Uid = player.uid
            });
            SessionHelper.GetDBHttpSession().Send(new L2D_SessionDisconnect()
            {
                Uid = player.uid
            });
            Log.Info($"DisconnectPlayer> uid:{player.uid}");
        }
        public static async void DisconnectMapUnit(Player player)
        {
            if (player == null)
                return;

            // 向MapServer發送斷線訊息
            if (player.InRoom())
            {
                ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(player.mapUnitId);
                actorLocationSender.Send(new L2M_SessionDisconnect());

                // Player移除mapUnitId
                player.LeaveRoom();
            }

            // 更新Player
            await Game.Scene.GetComponent<PlayerComponent>().Update(player);
            G2C_ForceDisconnect message = new G2C_ForceDisconnect()
            {
                DisconnectInfo = DisconnectInfo.Network_Delay
            };
            GateMessageHelper.BroadcastTarget(message, player.uid);
            Log.Info($"DisconnectMapUnit> uid:{player.uid}");
        }
        public static StartConfig GetRandomMap()
        {
            var mapConfigs = StartConfigComponent.Instance.MapConfigs;
            var val = RandomHelper.RandomNumber(0, mapConfigs.Count);
            return mapConfigs[val];
        }
    }
}
