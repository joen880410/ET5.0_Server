using System;

namespace ETModel
{
    public class MapUnitGateComponent : Component, ISerializeToEntity
    {
        /// <summary>
        /// 玩家同步控制器(非擁有者)
        /// </summary>
        public RedisEventSolverComponent MemorySync { get; set; }
        public const long NeedDisconnectMilliseconds = 10000;
        public long GateSessionActorId;

        public bool IsDisconnect { get; set; }

        public void Awake(long gateSessionId)
        {
            MemorySync = Game.Scene.GetComponent<CacheProxyComponent>().GetMemorySyncSolver<Player>();
            MemorySync.onUpdate += OnPlayerUpdated;

            this.IsDisconnect = false;
            this.GateSessionActorId = gateSessionId;
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();
            IsDisconnect = false;
            MemorySync.onUpdate -= OnPlayerUpdated;
            MemorySync = null;
        }

        private void OnPlayerUpdated(long id)
        {
            MapUnit parent = GetParent<MapUnit>();
            Player player = MemorySync.Get<Player>(id);
            if (player != null && parent.Uid == player.uid)
            {
                this.GateSessionActorId = player.gateSessionActorId;
            }
        }
    }
}