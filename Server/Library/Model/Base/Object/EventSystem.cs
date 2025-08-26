using System;
using System.Collections.Generic;
using System.Reflection;

namespace ETModel
{
    public enum DLLType
    {
        Model,
        Hotfix,
        Editor,
        Library,
    }

    public sealed class EventSystem
    {
        private readonly Dictionary<long, Component> allComponents = new Dictionary<long, Component>();

        private readonly Dictionary<DLLType, Assembly> assemblies = new Dictionary<DLLType, Assembly>();
        private readonly UnOrderMultiMap<Type, Type> types = new UnOrderMultiMap<Type, Type>();

        private readonly Dictionary<string, List<IEventSystem>> allEvents = new Dictionary<string, List<IEventSystem>>();

        private readonly UnOrderMultiMap<Type, IAwakeSystem> awakeSystems = new UnOrderMultiMap<Type, IAwakeSystem>();

        private readonly UnOrderMultiMap<Type, IStartSystem> startSystems = new UnOrderMultiMap<Type, IStartSystem>();

        private readonly UnOrderMultiMap<Type, IDestroySystem> destroySystems = new UnOrderMultiMap<Type, IDestroySystem>();

        private readonly UnOrderMultiMap<Type, ILoadSystem> loadSystems = new UnOrderMultiMap<Type, ILoadSystem>();

        private readonly UnOrderMultiMap<Type, IUpdateSystem> updateSystems = new UnOrderMultiMap<Type, IUpdateSystem>();

        private readonly UnOrderMultiMap<Type, IFixedUpdateSystem> fixedUpdateSystems = new UnOrderMultiMap<Type, IFixedUpdateSystem>();

        private readonly UnOrderMultiMap<Type, ILateUpdateSystem> lateUpdateSystems = new UnOrderMultiMap<Type, ILateUpdateSystem>();

        private readonly UnOrderMultiMap<Type, IChangeSystem> changeSystems = new UnOrderMultiMap<Type, IChangeSystem>();

        private readonly UnOrderMultiMap<Type, IDeserializeSystem> deserializeSystems = new UnOrderMultiMap<Type, IDeserializeSystem>();


        private readonly TwoQueue<long> fixedUpdatesTwoQueue = new TwoQueue<long>();

        private readonly Queue<long> starts = new Queue<long>();

        private readonly TwoQueue<long> updateTwoQueue = new TwoQueue<long>();

        private readonly TwoQueue<long> loadTwoQueue = new TwoQueue<long>();

        private readonly TwoQueue<long> lateUpdateTwoQueue = new TwoQueue<long>();

        public void Add(DLLType dllType, Assembly assembly)
        {
            this.assemblies[dllType] = assembly;
            this.types.Clear();
            foreach (Assembly value in this.assemblies.Values)
            {
                foreach (Type type in value.GetTypes())
                {
                    object[] objects = type.GetCustomAttributes(typeof(BaseAttribute), false);
                    if (objects.Length == 0)
                    {
                        continue;
                    }
                    BaseAttribute baseAttribute = (BaseAttribute)objects[0];
                    this.types.Add(baseAttribute.AttributeType, type);
                }
            }

            this.awakeSystems.Clear();
            this.lateUpdateSystems.Clear();
            this.updateSystems.Clear();
            this.fixedUpdateSystems.Clear();
            this.startSystems.Clear();
            this.loadSystems.Clear();
            this.changeSystems.Clear();
            this.destroySystems.Clear();
            this.deserializeSystems.Clear();

            foreach (Type type in types[typeof(ObjectSystemAttribute)])
            {
                object[] attrs = type.GetCustomAttributes(typeof(ObjectSystemAttribute), false);

                if (attrs.Length == 0)
                {
                    continue;
                }

                object obj = Activator.CreateInstance(type);

                switch (obj)
                {
                    case IAwakeSystem objectSystem:
                        this.awakeSystems.Add(objectSystem.Type(), objectSystem);
                        break;
                    case IUpdateSystem updateSystem:
                        this.updateSystems.Add(updateSystem.Type(), updateSystem);
                        break;
                    case IFixedUpdateSystem fixedUpdateSystem:
                        this.fixedUpdateSystems.Add(fixedUpdateSystem.Type(), fixedUpdateSystem);
                        break;
                    case ILateUpdateSystem lateUpdateSystem:
                        this.lateUpdateSystems.Add(lateUpdateSystem.Type(), lateUpdateSystem);
                        break;
                    case IStartSystem startSystem:
                        this.startSystems.Add(startSystem.Type(), startSystem);
                        break;
                    case IDestroySystem destroySystem:
                        this.destroySystems.Add(destroySystem.Type(), destroySystem);
                        break;
                    case ILoadSystem loadSystem:
                        this.loadSystems.Add(loadSystem.Type(), loadSystem);
                        break;
                    case IChangeSystem changeSystem:
                        this.changeSystems.Add(changeSystem.Type(), changeSystem);
                        break;
                    case IDeserializeSystem deserializeSystem:
                        this.deserializeSystems.Add(deserializeSystem.Type(), deserializeSystem);
                        break;
                }
            }

            this.allEvents.Clear();
            var list = types[typeof(EventAttribute)];
            if (list != null)
            {
                foreach (Type type in list)
                {
                    object[] attrs = type.GetCustomAttributes(typeof(EventAttribute), false);

                    foreach (object attr in attrs)
                    {
                        EventAttribute aEventAttribute = (EventAttribute)attr;
                        IEventSystem obj = Activator.CreateInstance(type) as IEventSystem;
                        if (obj == null)
                        {
                            Log.Error($"{obj.GetType().Name} 没有继承IEventSystem");
                        }
                        this.RegisterEvent(aEventAttribute.EventId, obj);
                    }
                }
            }
            this.Load();
        }

        public void RegisterEvent(string eventId, IEventSystem e)
        {
            if (!this.allEvents.ContainsKey(eventId))
            {
                this.allEvents.Add(eventId, new List<IEventSystem>());
            }
            this.allEvents[eventId].Add(e);
        }

        public void UnregisterEvent(string eventId, IEventSystem e)
        {
            if (this.allEvents.ContainsKey(eventId))
            {
                this.allEvents[eventId].Remove(e);
            }
        }

        public Assembly Get(DLLType dllType)
        {
            return this.assemblies[dllType];
        }

        /// <summary>
        /// 根據參數取得所有有掛對應Attribute的type
        /// </summary>
        /// <param name="systemAttributeType">掛在最上面的Attribute</param>
        /// <returns></returns>
        /// 
        public List<Type> GetTypes(Type systemAttributeType)
        {
            if (!this.types.ContainsKey(systemAttributeType))
            {
                return new List<Type>();
            }
            return this.types[systemAttributeType];
        }

        /// <summary>
        /// 取得所有有掛Attribute的type
        /// </summary>
        /// <returns></returns>

        // 重用list
        private readonly List<Type> allTypes = new List<Type>();
        public List<Type> GetTypes()
        {
            allTypes.Clear();
            foreach (List<Type> types in this.types.GetDictionary().Values)
            {
                allTypes.AddRange(types);
            }
            return allTypes;
        }

        public void Add(Component component)
        {
            this.allComponents.Add(component.InstanceId, component);

            Type type = component.GetType();

            if (this.loadSystems.ContainsKey(type))
            {
                this.loadTwoQueue.Queue1.Enqueue(component.InstanceId);
            }

            if (this.updateSystems.ContainsKey(type))
            {
                this.updateTwoQueue.Queue1.Enqueue(component.InstanceId);
            }
            if (this.fixedUpdateSystems.ContainsKey(type))
            {
                this.fixedUpdatesTwoQueue.Queue1.Enqueue(component.InstanceId);
            }
            if (this.startSystems.ContainsKey(type))
            {
                this.starts.Enqueue(component.InstanceId);
            }

            if (this.lateUpdateSystems.ContainsKey(type))
            {
                this.lateUpdateTwoQueue.Queue1.Enqueue(component.InstanceId);
            }
        }

        public void Remove(long instanceId)
        {
            this.allComponents.Remove(instanceId);
        }

        public Component Get(long instanceId)
        {
            this.allComponents.TryGetValue(instanceId, out Component component);
            return component;
        }

        public void Deserialize(Component component)
        {
            List<IDeserializeSystem> iDeserializeSystems = this.deserializeSystems[component.GetType()];
            if (iDeserializeSystems == null)
            {
                return;
            }

            foreach (IDeserializeSystem deserializeSystem in iDeserializeSystems)
            {
                if (deserializeSystem == null)
                {
                    continue;
                }

                try
                {
                    deserializeSystem.Run(component);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Awake(Component component)
        {
            List<IAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
            if (iAwakeSystems == null)
            {
                return;
            }

            foreach (IAwakeSystem aAwakeSystem in iAwakeSystems)
            {
                if (aAwakeSystem == null)
                {
                    continue;
                }

                IAwake iAwake = aAwakeSystem as IAwake;
                if (iAwake == null)
                {
                    continue;
                }

                try
                {
                    iAwake.Run(component);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Awake<P1>(Component component, P1 p1)
        {
            List<IAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
            if (iAwakeSystems == null)
            {
                return;
            }

            foreach (IAwakeSystem aAwakeSystem in iAwakeSystems)
            {
                if (aAwakeSystem == null)
                {
                    continue;
                }

                IAwake<P1> iAwake = aAwakeSystem as IAwake<P1>;
                if (iAwake == null)
                {
                    continue;
                }

                try
                {
                    iAwake.Run(component, p1);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Awake<P1, P2>(Component component, P1 p1, P2 p2)
        {
            List<IAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
            if (iAwakeSystems == null)
            {
                return;
            }

            foreach (IAwakeSystem aAwakeSystem in iAwakeSystems)
            {
                if (aAwakeSystem == null)
                {
                    continue;
                }

                IAwake<P1, P2> iAwake = aAwakeSystem as IAwake<P1, P2>;
                if (iAwake == null)
                {
                    continue;
                }

                try
                {
                    iAwake.Run(component, p1, p2);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Awake<P1, P2, P3>(Component component, P1 p1, P2 p2, P3 p3)
        {
            List<IAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
            if (iAwakeSystems == null)
            {
                return;
            }

            foreach (IAwakeSystem aAwakeSystem in iAwakeSystems)
            {
                if (aAwakeSystem == null)
                {
                    continue;
                }

                IAwake<P1, P2, P3> iAwake = aAwakeSystem as IAwake<P1, P2, P3>;
                if (iAwake == null)
                {
                    continue;
                }

                try
                {
                    iAwake.Run(component, p1, p2, p3);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Change(Component component)
        {
            List<IChangeSystem> iChangeSystems = this.changeSystems[component.GetType()];
            if (iChangeSystems == null)
            {
                return;
            }

            foreach (IChangeSystem iChangeSystem in iChangeSystems)
            {
                if (iChangeSystem == null)
                {
                    continue;
                }

                try
                {
                    iChangeSystem.Run(component);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Load()
        {
            while (this.loadTwoQueue.Queue1.Count > 0)
            {
                long instanceId = this.loadTwoQueue.Queue1.Dequeue();
                if (!this.allComponents.TryGetValue(instanceId, out Component component))
                {
                    continue;
                }
                if (component.IsDisposed)
                {
                    continue;
                }

                List<ILoadSystem> iLoadSystems = this.loadSystems[component.GetType()];
                if (iLoadSystems == null)
                {
                    continue;
                }

                this.loadTwoQueue.Queue2.Enqueue(instanceId);

                foreach (ILoadSystem iLoadSystem in iLoadSystems)
                {
                    try
                    {
                        iLoadSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }

            loadTwoQueue.Swap();
        }

        private void Start()
        {
            while (this.starts.Count > 0)
            {
                long instanceId = this.starts.Dequeue();
                if (!this.allComponents.TryGetValue(instanceId, out Component component))
                {
                    continue;
                }

                List<IStartSystem> iStartSystems = this.startSystems[component.GetType()];
                if (iStartSystems == null)
                {
                    continue;
                }

                foreach (IStartSystem iStartSystem in iStartSystems)
                {
                    try
                    {
                        iStartSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }

        public void Destroy(Component component)
        {
            List<IDestroySystem> iDestroySystems = this.destroySystems[component.GetType()];
            if (iDestroySystems == null)
            {
                return;
            }

            foreach (IDestroySystem iDestroySystem in iDestroySystems)
            {
                if (iDestroySystem == null)
                {
                    continue;
                }

                try
                {
                    iDestroySystem.Run(component);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Update()
        {
            this.Start();

            while (this.updateTwoQueue.Queue1.Count > 0)
            {
                long instanceId = this.updateTwoQueue.Queue1.Dequeue();
                Component component;
                if (!this.allComponents.TryGetValue(instanceId, out component))
                {
                    continue;
                }
                if (component.IsDisposed)
                {
                    continue;
                }

                List<IUpdateSystem> iUpdateSystems = this.updateSystems[component.GetType()];
                if (iUpdateSystems == null)
                {
                    continue;
                }

                this.updateTwoQueue.Queue2.Enqueue(instanceId);

                foreach (IUpdateSystem iUpdateSystem in iUpdateSystems)
                {
                    try
                    {
                        iUpdateSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }

            updateTwoQueue.Swap();
        }

        public void FixedUpdate()
        {
            while (this.fixedUpdatesTwoQueue.Queue1.Count > 0)
            {
                long instanceId = this.fixedUpdatesTwoQueue.Queue1.Dequeue();
                Component component;
                if (!this.allComponents.TryGetValue(instanceId, out component))
                {
                    continue;
                }

                if (component.IsDisposed)
                {
                    continue;
                }

                List<IFixedUpdateSystem> iFixedUpdates = this.fixedUpdateSystems[component.GetType()];
                if (iFixedUpdates == null)
                {
                    continue;
                }

                this.fixedUpdatesTwoQueue.Queue2.Enqueue(instanceId);

                foreach (IFixedUpdateSystem iFixedUpdateSystem in iFixedUpdates)
                {
                    try
                    {
                        iFixedUpdateSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
            this.fixedUpdatesTwoQueue.Swap();
        }

        public void LateUpdate()
        {
            while (this.lateUpdateTwoQueue.Queue1.Count > 0)
            {
                long instanceId = this.lateUpdateTwoQueue.Queue1.Dequeue();
                Component component;
                if (!this.allComponents.TryGetValue(instanceId, out component))
                {
                    continue;
                }
                if (component.IsDisposed)
                {
                    continue;
                }

                List<ILateUpdateSystem> iLateUpdateSystems = this.lateUpdateSystems[component.GetType()];
                if (iLateUpdateSystems == null)
                {
                    continue;
                }

                this.lateUpdateTwoQueue.Queue2.Enqueue(instanceId);

                foreach (ILateUpdateSystem iLateUpdateSystem in iLateUpdateSystems)
                {
                    try
                    {
                        iLateUpdateSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }

            lateUpdateTwoQueue.Swap();
        }

        public void Run(string eventId)
        {
            if (!this.allEvents.TryGetValue(eventId, out List<IEventSystem> iEventSystem))
            {
                return;
            }
            foreach (IEventSystem eventSystem in iEventSystem)
            {
                try
                {
                    IEvent iEvent = eventSystem as IEvent;
                    if (iEvent == null)
                    {
                        continue;
                    }
                    iEvent.Handle();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Run<A>(string eventId, A a)
        {
            if (!this.allEvents.TryGetValue(eventId, out List<IEventSystem> iEventSystem))
            {
                return;
            }
            foreach (IEventSystem eventSystem in iEventSystem)
            {
                try
                {
                    IEvent<A> iEvent = eventSystem as IEvent<A>;
                    if (iEvent == null)
                    {
                        continue;
                    }
                    iEvent.Handle(a);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Run<A, B>(string eventId, A a, B b)
        {
            if (!this.allEvents.TryGetValue(eventId, out List<IEventSystem> iEventSystem))
            {
                return;
            }
            foreach (IEventSystem eventSystem in iEventSystem)
            {
                try
                {
                    IEvent<A, B> iEvent = eventSystem as IEvent<A, B>;
                    if (iEvent == null)
                    {
                        continue;
                    }
                    iEvent.Handle(a, b);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Run<A, B, C>(string eventId, A a, B b, C c)
        {
            if (!this.allEvents.TryGetValue(eventId, out List<IEventSystem> iEventSystem))
            {
                return;
            }
            foreach (IEventSystem eventSystem in iEventSystem)
            {
                try
                {
                    IEvent<A, B, C> iEvent = eventSystem as IEvent<A, B, C>;
                    if (iEvent == null)
                    {
                        continue;
                    }
                    iEvent.Handle(a, b, c);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void Run<A, B, C, D>(string eventId, A a, B b, C c, D d)
        {
            if (!this.allEvents.TryGetValue(eventId, out List<IEventSystem> iEventSystem))
            {
                return;
            }
            foreach (IEventSystem eventSystem in iEventSystem)
            {
                try
                {
                    IEvent<A, B, C, D> iEvent = eventSystem as IEvent<A, B, C, D>;
                    if (iEvent == null)
                    {
                        continue;
                    }
                    iEvent.Handle(a, b, c, d);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}