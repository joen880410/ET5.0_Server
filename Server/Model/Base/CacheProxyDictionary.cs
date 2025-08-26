using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    [Obsolete]
    public sealed class CacheProxyDictionary<T, K> : Dictionary<T, K>, IDisposable where T : struct
                                                                                   where K : Entity
    {
        public RedisEventSolverComponent MemorySync { get; private set; }

        public void Set()
        {
            this.MemorySync = Game.Scene.GetComponent<CacheProxyComponent>()?.GetMemorySyncSolver<K>();
        }

        public K GetCacheByMemoryId(long id)
        {
            return this.MemorySync?.Get<K>(id);
        }

        public List<K> GetAllCacheByAppId(int appId)
        {
            return this.Select(e => e.Value).Where(e => IdGenerater.GetAppId(e.Id) == appId).ToList();
        }

        public List<K> GetAllCache()
        {
            return this.MemorySync?.GetAll<K>() ?? new List<K>();
        }

        public List<long> GetAllCacheId()
        {
            return this.GetAllCache().Select(e => e.Id).ToList();
        }

        public K GetCacheByKey(T t)
        {
            if (!this.TryGetValue(t, out var val))
            {
                return default;
            }

            return val;
        }

        public void AddCache(T t, K k)
        {
            if (!this.ContainKey(t))
            {
                this.Add(t, k);
            }
        }

        public void UpdateCache(T t, K k)
        {
            if (this.ContainKey(t))
            {
                this[t] = k;
            }
            else
            {
                this.AddCache(t, k);
            }
        }

        public void RemoveCache(T t)
        {
            if (this.ContainKey(t))
            {
                this.Remove(t);
            }
        }

        public bool ContainKey(T t) => this?.ContainsKey(t) ?? false;

        public void Dispose()
        {
            foreach (var pair in this)
            {
                pair.Value.Dispose();
            }
            this.MemorySync?.Dispose();
            this.Clear();
        }
    }
}