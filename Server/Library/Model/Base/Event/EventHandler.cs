using System;
using System.Collections.Generic;

namespace ETModel
{

    public class EventHandler : IEventSystem, IEvent
    {
        private Action _action;
        public EventHandler(Action action)
        {
            this._action = action;
        }
        public void Handle()
        {
            _action.Invoke();
        }
    }

    public class EventHandler<A> : IEventSystem, IEvent<A>
    {
        private readonly Action<A> _action;
        public EventHandler(Action<A> action)
        {
            this._action = action;
        }
        public void Handle(A a)
        {
            _action.Invoke(a);
        }
    }

    public class EventHandler<A, B> : Component, IEventSystem, IEvent<A, B>
    {
        private readonly Action<A, B> _action;
        public EventHandler(Action<A, B> action)
        {
            this._action = action;
        }
        public void Handle(A a, B b)
        {
            _action.Invoke(a, b);
        }
    }

    public class EventHandler<A, B, C> : Component, IEventSystem, IEvent<A, B, C>
    {
        private readonly Action<A, B, C> _action;
        public EventHandler(Action<A, B, C> action)
        {
            this._action = action;
        }
        public void Handle(A a, B b, C c)
        {
            _action.Invoke(a, b, c);
        }
    }
    public class EventHandler<A, B, C, D> : Component, IEventSystem, IEvent<A, B, C, D>
    {
        private readonly Action<A, B, C, D> _action;
        public EventHandler(Action<A, B, C, D> action)
        {
            this._action = action;
        }
        public void Handle(A a, B b, C c, D d)
        {
            _action.Invoke(a, b, c, d);
        }
    }
}
