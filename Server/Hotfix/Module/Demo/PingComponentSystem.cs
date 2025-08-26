using ETModel;
using System;
using System.Linq;

namespace ETHotfix
{
    [ObjectSystem]
    public class PingComponentAwakeSystem : AwakeSystem<PingComponent, long, long>
    {
        public override async void Awake(PingComponent self, long waitTime, long overtime)
        {
            self.onDisconnected = NetworkHelper.OnHeartBeatFailed;
            var timerComponent = Game.Scene.GetComponent<TimerComponent>();
            while (true)
            {
                try
                {
                    if (self._sessionTimes.Count > 0)
                        Console.WriteLine("在線人數 ：" + self._sessionTimes.Count.ToString());


                    await timerComponent.WaitForMilliSecondAsync(waitTime);

                    // 检查所有Session，如果有时间超过指定的间隔就执行action
                    foreach (var session in self._sessionTimes.ToArray())
                    {
                        if ((TimeHelper.ClientNowMilliSeconds() - session.Value) > overtime)
                        {
                            self.RemoveSession(session.Key);
                        }
                        await self.ChangeStatusSession(session.Key, TimeHelper.ClientNowMilliSeconds() - session.Value > PingComponent.OnlineTimeLimit * 2);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }
    }
    public static class PlayerComponentExpansion
    {
        public static async ETTask ChangeStatusSession(this PingComponent self, long id, bool isOnline)
        {
            var netOuterComponent = Game.Scene.GetComponent<NetOuterComponent>();
            var session = netOuterComponent.Get(id);
            if (session != null)
            {
                var sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
                if (sessionPlayerComponent != null)
                {
                    if (sessionPlayerComponent.Player.isOnline == !isOnline)
                    {
                        return;
                    }
                    await sessionPlayerComponent.ChangePlayerOnlineStatus(!isOnline);
                }
            }

        }
    }

}