using System.Collections.Generic;
using ETModel;
using System.Linq;
using ETHotfix.Share;
using System;
using MongoDB.Bson;

namespace ETHotfix
{
    [Schedule(SchedulePattern.RemakeTaskPerDay)]
    public class Schedule_RemakeTaskPerDay : ScheduleBase
    {
        private const long ScheduleId = 3L;
        public override async ETTask Run()
        {
            Log.Info($"Schedule_RemakeTaskPerDay--Time{DateTime.Now}");
            scheduleComponent.SetNextTime(SchedulePattern.RemakeTaskPerDay);

            NetInnerComponent netInnerComponent = Game.Scene.GetComponent<NetInnerComponent>();
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            List<StartConfig> startConfigs = startConfigComponent.LobbyConfigs;
            ETTask<IResponse>[] tasks = new ETTask<IResponse>[startConfigs.Count];
            StartConfig startConfig = null;
            for (int i = 0; i < startConfigs.Count; i++)
            {
                startConfig = startConfigs[i];
                InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
                Session serverSession = netInnerComponent.Get(innerConfig.IPEndPoint);
               
            }
        }
    }
}
