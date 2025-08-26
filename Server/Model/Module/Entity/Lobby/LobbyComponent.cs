using System.Collections.Generic;
using System.Linq;
using ETHotfix;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETModel
{
    public class LobbyComponent : Component
    {
        /// <summary>
        /// 房間同步控制器(非擁有者)
        /// </summary>
        public RedisEventSolverComponent RoomMemorySync { get; set; }

        // Roaming
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Room> RoamingSettingDict = new Dictionary<long, Room>();

        [BsonElement]
        public List<Room> RoamingList => RoomMemorySync.Data.Values.OfType<Room>().Where(e => e.Type == RoomType.Roaming).ToList();

        // Team
        [BsonElement]
        public List<Room> TeamList => RoomMemorySync.Data.Values.OfType<Room>().Where(e => e.Type == RoomType.Team).ToList();
        
        // EventTeam
        [BsonElement]
        public List<Room> EventTeamList => RoomMemorySync.Data.Values.OfType<Room>().Where(e => e.Type == RoomType.EventTeam).ToList();

        public void Start()
        {
            var proxy = Game.Scene.GetComponent<CacheProxyComponent>();
            RoomMemorySync = proxy.GetMemorySyncSolver<Room>();
            //RoomMemorySync.onCreate += OnCreate;
            //RoomMemorySync.onWillDelete += OnWillDelete;
            //RoomMemorySync.onRefresh += ((value) =>
            //{
            //    var rooms = RoomMemorySync.Data.Values.OfType<Room>().ToList();
            //    foreach (var room in rooms)
            //    {
            //        OnCreate(room.info.RoomId);
            //    }
            //});
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            //RoomMemorySync.onCreate -= OnCreate;
            //RoomMemorySync.onWillDelete -= OnWillDelete;
            RoomMemorySync = null;
        }

        public void OnCreate(long id)
        {
            Room room = RoomMemorySync.Get<Room>(id);
            Room first = null;
            // 房間創建同步完成
            if (room != null)
            {
                switch (room.Type)
                {
                    case RoomType.Roaming:
                        first = OtherHelper.Search(RoamingList, r => r.Id == id);
                        if (first == null)
                        {
                            RoamingList.Add(room);
                        }
                        break;
                    case RoomType.Team:
                        first = OtherHelper.Search(TeamList, r => r.Id == id);
                        if (first == null)
                        {
                            TeamList.Add(room);
                        }
                        break;
                    case RoomType.EventTeam:
                        first = OtherHelper.Search(this.EventTeamList, r => r.Id == id);
                        if (first == null)
                        {
                            this.EventTeamList.Add(room);
                        }
                        break;
                }
            }
            else
            {
                // GG跑到這邊表示有問題
                Log.Error($"Room[{id}] is missing!");
            }
        }
        public void OnRefresh(long id)
        {
            RoamingList.Clear();
            TeamList.Clear();
            Room room = RoomMemorySync.Get<Room>(id);
            Room first = null;
            // 房間創建同步完成
            if (room != null)
            {
                switch (room.Type)
                {
                    case RoomType.Roaming:
                        first = OtherHelper.Search(RoamingList, r => r.Id == id);
                        if (first == null)
                        {
                            RoamingList.Add(room);
                        }
                        break;
                    case RoomType.Team:
                        first = OtherHelper.Search(TeamList, r => r.Id == id);
                        if (first == null)
                        {
                            TeamList.Add(room);
                        }
                        break;
                }
            }
            else
            {
                // GG跑到這邊表示有問題
                Log.Error($"Room[{id}] is missing!");
            }
        }
        public void OnWillDelete(long id)
        {
            // 刪除前的物件
            Room room = RoomMemorySync.Get<Room>(id);
            if (room == null)
            {
                // GG跑到這邊表示有問題
                Log.Error($"Room[{id}] has removed!");
            }
            else
            {
                switch (room.Type)
                {
                    case RoomType.Roaming:
                        room = OtherHelper.Search(RoamingList, r => r.Id == id);
                        if (room != null)
                        {
                            RoamingList.Remove(room);
                        }
                        break;
                    case RoomType.Team:
                        room = OtherHelper.Search(TeamList, r => r.Id == id);
                        if (room != null)
                        {
                            TeamList.Remove(room);
                        }
                        break;
                    case RoomType.EventTeam:
                        room = OtherHelper.Search(this.EventTeamList, r => r.Id == id);
                        if (room != null)
                        {
                            this.EventTeamList.Remove(room);
                        }
                        break;
                }
            }
        }

        public Room GetRoom(long id)
        {
            return RoomMemorySync.Get<Room>(id);
        }

        public List<Room> GetAllByType(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.Roaming:
                    return RoamingList;
                case RoomType.Team:
                    return TeamList;
                case RoomType.EventTeam:
                    return this.EventTeamList;
            }
            return null;
        }

        public Room GetRoamingBySettingId(long settingId)
        {
            RoamingSettingDict.TryGetValue(settingId, out var room);
            return room;
        }

        public int GetAllRoomCount()
        {
            return RoamingList.Count + TeamList.Count;
        }
    }
}