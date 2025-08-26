using System.Collections.Generic;

namespace ETModel
{
	public class PlayerComponent : Component
	{
        /// <summary>
        /// Player快取同步組件(擁有者)
        /// </summary>
        public RedisEventSolverComponent MemorySync { get; set; }

        public Player GetByUid(long uid)
        {
            return MemorySync.Get<Player>(uid);
        }
    }
}