using ETHotfix;
using Google.Protobuf.Collections;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETModel
{
    public enum RoomState
    {
        Ready,
        Start,
        Run,
        End,
    }

#if !DBVIEWGENERATOR
    public sealed partial class Room : Entity
    {
        [BsonIgnore]
        [CacheIgnore]
        public RoomState State
        {
            get
            {
                return (RoomState)state;
            }
            set
            {
                state = (int)value;
            }
        }

        [BsonIgnore]
        [CacheIgnore]
        public RoomType Type
        {
            get
            {
                return (RoomType)type;
            }
            set
            {
                type = (int)value;
            }
        }

        [BsonIgnore]
        [CacheIgnore]
        public int RoomBattlePlayType
        {
            get
            {
                return this.roomBattlePlayType;
            }
            set
            {
                this.roomBattlePlayType = value;
            }
        }

        [BsonIgnore]
        [CacheIgnore]
        public int MemberCount
        {
            get
            {
                return this.MapUnitList.Count;
            }
        }

        [BsonIgnore]
        [CacheIgnore]
        public List<MapUnit> Players { get => this.MapUnitList.Where(e => e.MapUnitType == MapUnitType.Hero).ToList(); }

        [BsonIgnore]
        [CacheIgnore]
        public List<MapUnit> Npcs { get => this.MapUnitList.Where(e => e.MapUnitType == MapUnitType.Npc).ToList(); }

        [BsonIgnore]
        [CacheIgnore]
        public int PlayerCount { get => this.MapUnitList.Count(e => e.MapUnitType == MapUnitType.Hero); }

        [BsonIgnore]
        [CacheIgnore]
        public int NPCCount { get => this.MapUnitList.Count(e => e.MapUnitType == MapUnitType.Npc); }

        [BsonIgnore]
        public readonly Dictionary<long, MapUnit> UidDict = new Dictionary<long, MapUnit>();

        [BsonIgnore]
        public readonly Dictionary<long, MapUnit> IdDict = new Dictionary<long, MapUnit>();

        [BsonIgnore]
        public readonly List<MapUnit> MapUnitList = new List<MapUnit>();

        public void Awake(RoomType roomType)
        {
            Type = roomType;
            RoomBattlePlayType = 0;
            State = RoomState.Start;
            info = null;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            MapUnitList.Clear();
            IdDict.Clear();
            UidDict.Clear();
            base.Dispose();
        }

        public void SwitchState(RoomState state)
        {
            State = state;
        }

        /// <summary>
        /// MapUintId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MapUnit GetMapUnitById(long id)
        {
            this.IdDict.TryGetValue(id, out MapUnit mapUnit);
            return mapUnit;
        }

        /// <summary>
        /// PlayerUid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public MapUnit GetMapUnitByUid(long uid)
        {
            this.UidDict.TryGetValue(uid, out MapUnit mapUnit);
            return mapUnit;
        }

        public List<MapUnit> GetAll()
        {
            return this.MapUnitList;
        }

        public void SetData(RoomInfo roomInfo)
        {
            info = roomInfo;
            info.RoomId = Id;
        }
        public RepeatedField<long> GetAllUid()
        {
            var UserList = new RepeatedField<long>();
            foreach (var user in this.IdDict)
            {
                if (user.Value.MapUnitType == MapUnitType.Npc)
                    continue;
                UserList.Add(user.Value.Uid);
            }
            return UserList;
        }
    }
#endif
}