using CronNET.Impl;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ETModel
{
    public class ScheduleComponent : Component
    {
        public static readonly CronDaemon cron_daemon = new CronDaemon();

        private Dictionary<string, ScheduleBase> scheduleDict = new Dictionary<string, ScheduleBase>();

        public void Awake()
        {
            if (scheduleDict == null)
            {
                scheduleDict = new Dictionary<string, ScheduleBase>();
            }

            scheduleDict.Clear();

            foreach (Type type in Game.EventSystem.GetTypes(typeof(ScheduleAttribute)))
            {
                var attrs = type.GetCustomAttributes(typeof(ScheduleAttribute), false);
                var ScheduleHandlerAttribute = (ScheduleAttribute)attrs[0];

                var obj = Activator.CreateInstance(type);
                if (obj == null)
                    throw new Exception($"To Register Schdule failed! Reason: AllSchedule not Find: {ScheduleHandlerAttribute.name}");

                var scheduleBase = (ScheduleBase)obj;

                scheduleDict.Add(ScheduleHandlerAttribute.name.ToLower(), scheduleBase);
            }
        }
        public void Destroy()
        {
            scheduleDict.Clear();
            scheduleDict = null;
        }
        public void SetScheduleNextTime(string name)
        {
            name = name.ToLower();
            if (!scheduleDict.TryGetValue(name, out var scheduleFunc))
            {
                Log.Warning($"ScheduleComponent scheduleDict not found func name: {name}");
                return;
            }

            var nextDate = GetScheduleNextTime(scheduleFunc.RepeatRate);
            scheduleDict[name].NextTime = nextDate.ToString();
        }
        public async ETTask RunSchedule(string name)
        {
            name = name.ToLower();
            if (!scheduleDict.TryGetValue(name, out var scheduleFunc))
            {
                Log.Warning($"ScheduleComponent scheduleDict not found func name: {name}");
                return;
            }
            await scheduleFunc.Start();
        }
        public ScheduleBase ShowSchedule(string name)
        {
            name = name.ToLower();
            if (!scheduleDict.TryGetValue(name, out var scheduleFunc))
            {
                Log.Warning($"ScheduleComponent scheduleDict not found func name: {name}");
                return null;
            }
            return scheduleFunc;

        }
        public void ReloadSchedule(ScheduleSetting config)
        {
            if (config == null)
            {
                return;
            }

            var funcName = config._Func.ToLower();
            var repeatRate = config._CronNet;
            if (!scheduleDict.TryGetValue(funcName, out var scheduleFunc))
            {
                Log.Warning($"ScheduleComponent scheduleDict not found func name: {funcName} config id: {config.Id}");
                return;
            }

            var nextDate = GetScheduleNextTime(repeatRate);
            scheduleDict[funcName].RepeatRate = repeatRate;
            scheduleDict[funcName].NextTime = nextDate.ToString();

            cron_daemon.Remove(funcName);
            cron_daemon.Add(new CronJob(() =>
            {
                return ScheduleFunc(scheduleFunc.Start);
            }, funcName, repeatRate));
        }
        public void ReloadAllSchedule(List<ScheduleSetting> configs)
        {
            if (configs.Count <= 0)
            {
                return;
            }

            cron_daemon.Clear();

            for (int i = 0; i < configs.Count; i++)
            {
                var config = configs[i];
                var funcName = config._Func.ToLower();
                var repeatRate = config._CronNet;
                if (!scheduleDict.TryGetValue(funcName, out var scheduleFunc))
                {
                    Log.Warning($"ScheduleComponent scheduleDict not found func name: {funcName} config id: {config.Id}");
                    return;
                }
                var nextDate = GetScheduleNextTime(repeatRate);
                scheduleDict[funcName].RepeatRate = repeatRate;
                scheduleDict[funcName].NextTime = nextDate.ToString();
                scheduleDict[funcName].scheduleComponent = this;
                cron_daemon.Add(new CronJob(() =>
                {
                    return ScheduleFunc(scheduleFunc.Start);
                }, funcName, repeatRate));
            }
        }
        /// <summary>
        /// 取得下次更新時間
        /// </summary>
        private DateTime GetScheduleNextTime(string repeatRate)
        {
            var schedule = CrontabSchedule.Parse(repeatRate);
            var nowDate = DateHelper.TimestampMillisecondToDateTimeUTC(TimeHelper.NowTimeMillisecond());
            Log.Info("-------排程間隔 : " + repeatRate + "當前時間:" + nowDate + " 下次執行時間" + schedule.GetNextOccurrence(nowDate));
            return schedule.GetNextOccurrence(nowDate);
        }
        private Task ScheduleFunc(Func<Task> func)
        {
            SendOrPostCallback callback = o => func?.Invoke();
            OneThreadSynchronizationContext.Instance.Post(callback, null);
            return Task.CompletedTask;
        }
    }
}