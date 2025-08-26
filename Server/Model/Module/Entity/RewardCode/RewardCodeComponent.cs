using System.Collections.Generic;
using CronNET.Impl;

namespace ETModel
{
    public class RewardCodeComponent : Component
    {
        public readonly Dictionary<long, RewardCode> AllRewardCodeDict = new Dictionary<long, RewardCode>();
        public readonly CronDaemon AllCronDict = new CronDaemon();
    }
}
