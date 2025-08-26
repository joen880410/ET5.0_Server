using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class ScheduleComponentAwakeSystem : AwakeSystem<ScheduleComponent>
    {
        public override void Awake(ScheduleComponent self)
        {
            self.Awake();
            self.LoadAllSchedule();
        }
    }
    [ObjectSystem]
    public class ScheduleComponentDestroySystem : DestroySystem<ScheduleComponent>
    {
        public override void Destroy(ScheduleComponent self)
        {
            self.Destroy();
        }
    }

    public static class ScheduleComponentHelper
    {
        public static  void LoadOneSchedule(this ScheduleComponent self, long configId)
        {
            var config = GetConfig(configId);
            self.ReloadSchedule(config);
        }
        public static  void LoadAllSchedule(this ScheduleComponent self)
        {
            var configs = GetConfigs();
            self.ReloadAllSchedule(configs);
        }
        public static void SetNextTime(this ScheduleComponent self, string name)
        {
            self.SetScheduleNextTime(name);
        }
        public static async ETTask RunOneSchedule(this ScheduleComponent self, string name)
        {
            await self.RunSchedule(name);
            return;
        }
        private static List<ScheduleSetting> GetConfigs()
        {
            var configComponent = Game.Scene.GetComponent<ConfigComponent>();
            var configs = configComponent.GetAll<ScheduleSetting>();
            return configs;
        }
        private static ScheduleSetting GetConfig(long configId)
        {
            var configComponent = Game.Scene.GetComponent<ConfigComponent>();
            return configComponent.Get<ScheduleSetting>(configId);
        }
    }
}
