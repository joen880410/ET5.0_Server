using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    /// <summary>
    /// Config组件会扫描所有的有ConfigAttribute标签的配置,加载进来
    /// </summary>
    public class ConfigComponent : Component
    {
        public Dictionary<Type, ACategory> AllConfig = new Dictionary<Type, ACategory>();
        public Dictionary<string, Type> AllConfigType = new Dictionary<string, Type>();

        public bool isLoadDB { get; set; } = false;

        public IConfig Get(Type type, long id)
        {
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                throw new Exception($"ConfigComponent not found key: {type.FullName}");
            }

            return configCategory.TryGet(id);
        }
        public T Get<T>(long id)
        {
            Type type = typeof(T);
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                throw new Exception($"ConfigComponent not found key: {type.FullName}");
            }

            return (T)configCategory.TryGet(id);
        }

        public IConfig TryGet(Type type, long id)
        {
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                return null;
            }
            return configCategory.TryGet(id);
        }

        public T TryGet<T>(long id)
        {
            Type type = typeof(T);
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                return default(T);
            }
            return (T)configCategory.TryGet(id);
        }

        public IConfig[] GetAll(Type type)
        {
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                throw new Exception($"ConfigComponent not found key: {type.FullName}");
            }
            return configCategory.GetAll();
        }

        public List<T> GetAll<T>()
        {
            Type type = typeof(T);
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                throw new Exception($"ConfigComponent not found key: {type.FullName}");
            }
            return configCategory.GetAll().OfType<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(Func<T, bool> predicate)
        {
            Type type = typeof(T);
            if (!AllConfig.TryGetValue(type, out ACategory configCategory))
            {
                throw new Exception($"ConfigComponent not found key: {type.FullName}");
            }
            return GetAll<T>().Where(predicate);
        }
    }
}