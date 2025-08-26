using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    public class PingComponent : Component
    {
        public readonly Dictionary<long, long> _sessionTimes = new Dictionary<long, long>();

        public Action<long> onDisconnected = null;
        public const long OnlineTimeLimit = 5000;

        public void AddSession(long id)
        {
            _sessionTimes.Add(id, TimeHelper.ClientNowMilliSeconds());
        }
        public bool RemoveSession(long id)
        {
            if (_sessionTimes.ContainsKey(id))
            {
                onDisconnected?.Invoke(id);
            }
            return _sessionTimes.Remove(id);
        }

        public void UpdateSession(long id)
        {
            if (_sessionTimes.ContainsKey(id)) 
                _sessionTimes[id] = TimeHelper.ClientNowMilliSeconds();
        }

        public void UpsertSession(long id)
        {
            _sessionTimes[id] = TimeHelper.ClientNowMilliSeconds();
            Console.WriteLine($"id:{id}--updateSession:{DateHelper.TimestampMillisecondToDateTimeLocal(TimeHelper.ClientNowMilliSeconds())}");
        }
    }
}