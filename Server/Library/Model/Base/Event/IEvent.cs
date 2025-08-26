using System;

namespace ETModel
{
    public interface IEventSystem
    {

    }
    public interface IEvent
    {
        void Handle();
    }

    public interface IEvent<A>
    {
        void Handle(A a);
    }

    public interface IEvent<A, B>
    {
        void Handle(A a, B b);
    }

    public interface IEvent<A, B, C>
    {
        void Handle(A a, B b, C c);
    }

    public interface IEvent<A, B, C, D>
    {
        void Handle(A a, B b, C c, D d);
    }

    public abstract class AEvent : IEventSystem, IEvent
    {
        public void Handle()
        {
            this.Run();
        }

        public abstract void Run();
    }

    public abstract class AEvent<A> : IEventSystem, IEvent<A>
    {
        public void Handle(A a)
        {
            this.Run(a);
        }

        public abstract void Run(A a);
    }

    public abstract class AEvent<A, B> : IEventSystem, IEvent<A, B>
    {
        public void Handle(A a, B b)
        {
            this.Run(a, b);
        }
        public abstract void Run(A a, B b);
    }

    public abstract class AEvent<A, B, C> : IEventSystem, IEvent<A, B, C>
    {

        public void Handle(A a, B b, C c)
        {
            this.Run(a, b, c);
        }
        public abstract void Run(A a, B b, C c);
    }

    public abstract class AEvent<A, B, C, D> : IEventSystem, IEvent<A, B, C, D>
    {
        public void Handle(A a, B b, C c, D d)
        {
            this.Run(a, b, c, d);
        }
        public abstract void Run(A a, B b, C c, D d);
    }
}