using ETHotfix.Share;
using ETModel;
using System;

namespace ETHotfix
{
    [ObjectSystem]
    public class PlayerAwakeSystem : AwakeSystem<Player>
    {
        public override void Awake(Player self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class PlayerUpdateSystem : UpdateSystem<Player>
    {
        public override void Update(Player self)
        {
            if (self.isOnDisconnectingStage)
            {
                CheckOnline(self);
            }
        }

        private void CheckOnline(Player self)
        {
            if (!self.isOnline && TimeHelper.ClientNowMilliSeconds() > self.disconnectTime)
            {
                NetworkHelper.DisconnectPlayer(self);
                Console.WriteLine($"{DateTime.Now} {self.uid} DisconnectPlayer");
            }
            if (!self.isOnline && TimeHelper.ClientNowMilliSeconds() > self.mapUnitDisconnectTime && self.InRoom())
            {
                NetworkHelper.DisconnectMapUnit(self);
                Console.WriteLine($"{DateTime.Now} {self.uid} DisconnectMapUnit");
            }
        }
    }

    public static class PlayerSystem
    {
        /// <summary>
        /// 玩家網路超時時間，超過就無法直接重連
        /// </summary>
        private const int ConnectTimeout = 60 * 60 * 1000 * 4;
        private const int MapUnitTimeout = 60 * 60 * 1000;

        #region Online/Offline

        public static void SetOnline(this Player self, bool isOnline)
        {
            if (!isOnline && self.isOnline != isOnline)
            {
                self.disconnectTime = TimeHelper.ClientNowMilliSeconds() + ConnectTimeout;
                if (self.InRoom())
                {
                    self.mapUnitDisconnectTime = TimeHelper.ClientNowMilliSeconds() + MapUnitTimeout;
                }
                self.isOnDisconnectingStage = !isOnline;
            }
            self.isOnline = isOnline;
            Console.WriteLine($"{DateTime.Now} {self.uid} isOnline:{isOnline}");
        }

        #endregion

    }
}