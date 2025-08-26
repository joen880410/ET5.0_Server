using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [ObjectSystem]
    public class LockEventAwakeSystem : AwakeSystem<LockEvent, string>
    {
        public override void Awake(LockEvent self, string key)
        {
            self.Awake(key);
        }
    }

    [ObjectSystem]
    public class LockEventDestroySystem : DestroySystem<LockEvent>
    {
        public override void Destroy(LockEvent self)
        {
            self.Destroy();
        }
    }

    public static class LockEventSystem
    {
        public static void Awake(this LockEvent self, string key)
        {
            self.key = key;
        }

        public static async ETTask<LockEvent> Wait(this LockEvent self, long timeout = 60000)
        {
            var cacheProxyComponent = Game.Scene.GetComponent<CacheProxyComponent>();
            self.Id = IdGenerater.GenerateId();
            // 模擬自旋鎖
            while (!await cacheProxyComponent.LockEvent(self.key, self.Id.ToString(), timeout))
            {
                await Game.Scene.GetComponent<TimerComponent>().WaitForSecondAsync(1f);
            }

            return self;
        }

        public static void Destroy(this LockEvent self)
        {
            var cacheProxyComponent = Game.Scene.GetComponent<CacheProxyComponent>();
            cacheProxyComponent.UnlockEvent(self.key, self.Id.ToString()).Coroutine();

            //Game.Scene.GetComponent<LocationProxyComponent>().UnlockEvent(self.Id, self.key).Coroutine();
        }
    }
}