using System;
using System.Collections.Generic;
using System.Threading;

namespace ETModel
{
    public struct Timer
    {
        public long Id { get; set; }
        public long Time { get; set; }
        public ETTaskCompletionSource tcs;
    }

    [ObjectSystem]
    public class TimerComponentAwakeSystem : AwakeSystem<TimerComponent>
    {
        public override void Awake(TimerComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class TimerComponentUpdateSystem : UpdateSystem<TimerComponent>
    {
        public override void Update(TimerComponent self)
        {
            self.Update();
        }
    }

    public class TimerComponent : Component
    {
        private readonly Dictionary<long, Timer> timers = new Dictionary<long, Timer>();

        /// <summary>
        /// key: time, value: timer id
        /// </summary>
        private readonly MultiMap<long, long> timeId = new MultiMap<long, long>();

        private readonly Queue<long> timeOutTime = new Queue<long>();

        private readonly Queue<long> timeOutTimerIds = new Queue<long>();

        // 记录最小时间，不用每次都去MultiMap取第一个值
        private long minTime;

        public float deltaTime { private set; get; }

        public float time { private set; get; }

        private long initialTicks { set; get; }

        private long lastTicks { set; get; }

        private ETCancellationTokenSource etcs;

        public void Awake()
        {
            initialTicks = System.DateTime.Now.Ticks;
            lastTicks = System.DateTime.Now.Ticks;
            EvaluateDeltaTime();
        }

        public void Update()
        {
            EvaluateDeltaTime();

            if (this.timeId.Count == 0)
            {
                return;
            }

            long timeNow = TimeHelper.ClientNowMilliSeconds();

            if (timeNow < this.minTime)
            {
                return;
            }

            foreach (KeyValuePair<long, List<long>> kv in this.timeId.GetDictionary())
            {
                long k = kv.Key;
                if (k > timeNow)
                {
                    minTime = k;
                    break;
                }
                this.timeOutTime.Enqueue(k);
            }

            while (this.timeOutTime.Count > 0)
            {
                long time = this.timeOutTime.Dequeue();
                foreach (long timerId in this.timeId[time])
                {
                    this.timeOutTimerIds.Enqueue(timerId);
                }
                this.timeId.Remove(time);
            }

            while (this.timeOutTimerIds.Count > 0)
            {
                long timerId = this.timeOutTimerIds.Dequeue();
                Timer timer;
                if (!this.timers.TryGetValue(timerId, out timer))
                {
                    continue;
                }
                this.timers.Remove(timerId);
                timer.tcs.SetResult();
            }
        }

        private void EvaluateDeltaTime()
        {
            var ticks = DateTime.Now.Ticks;
            deltaTime = (ticks - lastTicks) / (float)TimeSpan.TicksPerSecond;
            time = (ticks - initialTicks) / (float)TimeSpan.TicksPerSecond;
            lastTicks = ticks;
        }

        private void Remove(long id)
        {
            this.timers.Remove(id);
        }

        public ETTask WaitTillAsync(long tillTime, CancellationToken cancellationToken)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = tillTime, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            cancellationToken.Register(() =>
            {
                tcs.TrySetResult();
                this.Remove(timer.Id);
            });
            return tcs.Task;
        }

        public ETTask WaitTillAsync(long tillTime)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = tillTime, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            return tcs.Task;
        }

        public ETTask WaitAsync(long time, CancellationToken cancellationToken)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = TimeHelper.ClientNowMilliSeconds() + time, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            cancellationToken.Register(() =>
            {
                tcs.TrySetResult();
                this.Remove(timer.Id);
            });
            return tcs.Task;
        }

        public async ETTask WaitFrameAsync(CancellationToken cancellationToken)
        {
            await WaitForSecondAsync(1f, cancellationToken);
        }

        /// <summary>
        /// 等待指定(秒)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public ETTask WaitForSecondAsync(float time)
        {
            return WaitForMilliSecondAsync((long)System.Math.Round(time * 1000));
        }

        /// <summary>
        /// 等待指定(毫秒)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
		public ETTask WaitForMilliSecondAsync(long time)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = TimeHelper.ClientNowMilliSeconds() + time, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            return tcs.Task;
        }
        /// <summary>
        /// 等待指定(秒),附加取消條件
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public ETTask WaitForSecondAsync(float time, CancellationToken cancellationToken)
        {
            return WaitForMilliSecondAsync((long)System.Math.Round(time * 1000), cancellationToken);
        }
        /// <summary>
        /// 等待指定(毫秒),附加取消條件
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public ETTask WaitForMilliSecondAsync(long time, CancellationToken cancellationToken)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Timer timer = new Timer { Id = IdGenerater.GenerateId(), Time = TimeHelper.ClientNowMilliSeconds() + time, tcs = tcs };
            this.timers[timer.Id] = timer;
            this.timeId.Add(timer.Time, timer.Id);
            if (timer.Time < this.minTime)
            {
                this.minTime = timer.Time;
            }
            cancellationToken.Register(() =>
            {//這邊TrySetResult先註解調，否則猜測會造成while迴圈瘋狂跑導致上面物件增生記憶體炸掉，之後要回來修改
                tcs.TrySetResult();
                this.Remove(timer.Id);
            });
            return tcs.Task;
        }

        /// <summary>
        /// 會每倒數指定秒數後回傳當前倒數計時的時間
        /// </summary>
        /// <param name="fromTime">開始倒數的時間</param>
        /// <param name="toTime">結束倒數的時間</param>
        /// <param name="second">倒數的秒數</param>
        /// <param name="callback">註冊委派，當執行時間後將會呼叫該委派</param>
        /// <returns></returns>
        public ETTask WaitForAssignSecondAsync(int fromTime, int toTime = 0, int second = 1, Action<int> callback = null)
        {
            return TimerAsync(fromTime, toTime, second, callback);
        }
        private async ETTask TimerAsync(int fromTime, int toTime = 0, int second = 1, Action<int> callback = null)
        {
            if (etcs != null)
            {
                etcs.Cancel();
                etcs.Dispose();
            }
            etcs = new ETCancellationTokenSource();

            var RefreshSecond = fromTime;
            while (RefreshSecond >= toTime && !etcs.Token.IsCancellationRequested)
            {
                callback(RefreshSecond);
                await Game.Scene.GetComponent<TimerComponent>().WaitForSecondAsync(second, etcs.Token);
                RefreshSecond -= second;
            }
        }
    }
}