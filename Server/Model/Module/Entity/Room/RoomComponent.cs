using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETModel
{
    public class RoomComponent : Component
    {
        /// <summary>
        /// 房間同步控制器(擁有者)
        /// </summary>
        public RedisEventSolverComponent MemorySync { get; set; }
        public void Start()
        {
            var proxy = Game.Scene.GetComponent<CacheProxyComponent>();
            MemorySync = proxy.GetMemorySyncSolver<Room>();
            // 因為內部會取RoomComponent但Awake時還沒產生好，所以寫在Start
            Game.EventSystem.Run(EventIdType.SyncAllRoamingRoom);
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
            // 非擁有者請勿操作Dispose
            MemorySync.Dispose();
        }


        public Room Get(long id)
        {
            return MemorySync.Get<Room>(id);
        }

    }
}