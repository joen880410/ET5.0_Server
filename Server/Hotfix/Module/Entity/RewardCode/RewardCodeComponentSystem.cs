using CronNET.Impl;
using ETModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETHotfix
{
    [ObjectSystem]
    public class RewardCodeComponentAwakeSystem : AwakeSystem<RewardCodeComponent>
    {
        public override async void Awake(RewardCodeComponent self)
        {
            await self.Load();
        }
    }

    [ObjectSystem]
    public class RewardCodeComponentDestroySystem : DestroySystem<RewardCodeComponent>
    {
        public override void Destroy(RewardCodeComponent self)
        {
            self.Destroy();
        }
    }

    public static class RewardCodeComponentEX 
    {
        public static async ETTask Load(this RewardCodeComponent self)
        {
            var rewardcodes = await RewardDataHelper.FindAllRewardCode();
            if (rewardcodes.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < rewardcodes.Count; i++)
            {
                var rewardcode = rewardcodes[i];
                if (rewardcode.expireAt < TimeHelper.NowTimeMillisecond())
                {
                    await RewardDataHelper.DeleteOneRewardCode(rewardcode.Id);
                    continue;
                }
                self.CreateCron(rewardcodes[i]);
            }
        }

        public static void Destroy(this RewardCodeComponent self)
        {
            self.AllRewardCodeDict.Clear();
            self.AllCronDict.Clear();
        }

        public static async Task DeleteRewardCode(this RewardCodeComponent self, long id)
        {
            await RewardDataHelper.DeleteOneRewardCode(id);
            self.DeleteCron(id);
        }

        #region CronNet
        public static void CreateCron(this RewardCodeComponent self, RewardCode rewardcode)
        {
            if (!self.AllRewardCodeDict.ContainsKey(rewardcode.Id))
            {
                self.AllRewardCodeDict.Add(rewardcode.Id, rewardcode);
            }

            var expiredUTCDateTime = DateHelper.TimestampMillisecondToDateTimeUTC(rewardcode.expireAt);
            var expiredCron = $"{expiredUTCDateTime.Minute} {expiredUTCDateTime.Hour} {expiredUTCDateTime.Day} {expiredUTCDateTime.Month} {(int)expiredUTCDateTime.DayOfWeek}";
            var expiredKey = rewardcode.ExpiredKeyName();
            self.AllCronDict.Add(new CronJob(() =>
            {
                return RewardcodeFunc(async () =>
                {
                    await self.DeleteRewardCode(rewardcode.Id);
                });
            }, expiredKey, expiredCron));
        }

        public static void DeleteCron(this RewardCodeComponent self, long id)
        {
            var rewardCode = self.AllRewardCodeDict[id];
            var releaseKey = rewardCode.ReleaseKeyName();
            self.AllCronDict.Remove(releaseKey);
            self.AllRewardCodeDict.Remove(id);
        }
        private static Task RewardcodeFunc(Func<Task> func)
        {
            SendOrPostCallback callback = o => func?.Invoke();
            OneThreadSynchronizationContext.Instance.Post(callback, null);
            return Task.CompletedTask;
        }
        #endregion
    }

}
