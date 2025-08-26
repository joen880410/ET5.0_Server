using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CronNET.Impl
{
    public class CronDaemon : ICronDaemon
    {
        private readonly System.Timers.Timer _timer;
        private readonly List<ICronJob> _cronJobs;
        private CancellationToken _cancellationToken;
        public event EventHandler<string> JobExecuting;
        public event EventHandler<string> JobExecuted;

        public CronDaemon()
        {
            _cronJobs = new List<ICronJob>();
            //60秒檢查一次 排程目前定最小到分鐘 30秒一次會觸發兩次
            _timer = new System.Timers.Timer(1000 * 1);
            _timer.Elapsed += TimerElapsed;
            _timer.Enabled = true;
        }
        public void Add(ICronJob job)
        {
            job.JobExecuted += Job_JobExecuted;
            job.JobExecuting += Job_JobExecuting;
            _cronJobs.Add(job);
        }
        public void Update(ICronJob job)
        {
            Remove(job.Name);
            Add(job);
        }
        private void Job_JobExecuting(object sender, string name)
        {
            JobExecuting?.Invoke(sender, name);
        }

        private void Job_JobExecuted(object sender, string name)
        {
            JobExecuted?.Invoke(sender, name);
        }

        public void Start(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _cancellationToken.Register(Stop);

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime.Second == 0)
            {
                Parallel.ForEach(_cronJobs, job => job.ExecuteAsync(DateTime.Now, _cancellationToken));
            }
        }

        public void Remove(string name)
        {
            var job = _cronJobs.FirstOrDefault(x => x.Name == name);

            if (job != null)
            {
                _cronJobs.Remove(job);
            }
        }

        public void Remove(ICronJob job)
        {
            _cronJobs.Remove(job);
        }
        public ICronJob Get(string name)
        {
            return _cronJobs.FirstOrDefault(x => x.Name == name);
        }
        public void Clear()
        {
            _cronJobs.Clear();
        }
        public Task RunAsync(Func<Task> func, CancellationToken cancellationToken, string name)
        {
            JobExecuting?.Invoke(this, name);

            return Task.Run(func, cancellationToken).ContinueWith(x => JobExecuted?.Invoke(this, name));
        }
    }
}
