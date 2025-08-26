using System;

namespace ETModel
{
    public interface IFixedUpdateSystem
    {
        Type Type();
        void Run(object o);
    }
    public interface IFixedUpdate
    {
        void Run(object o);
    }

    public abstract class FixedUpdateSystem<T> : IFixedUpdateSystem, IFixedUpdate
    {
        public void Run(object o)
        {
            this.FixedUpdate((T)o);
        }

        public Type Type()
        {
            return typeof(T);
        }

        public Type SystemType()
        {
            return typeof(IFixedUpdateSystem);
        }

        public abstract void FixedUpdate(T self);
    }
}