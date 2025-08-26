using System.Collections.Generic;
using System.Linq;
using ETModel;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETHotfix
{
    [ObjectSystem]
    public class RoomEntityAwakeSystem : AwakeSystem<Room, RoomType>
    {
        public override void Awake(Room self, RoomType a)
        {
            self.Awake(a);
        }
    }

    public static class RoomSystem
    {
        public static async ETTask AddMapUnit(this Room self, MapUnit mapUnit)
        {
            if (self.UidDict.ContainsKey(mapUnit.Uid))
            {
                return;
            }

            self.UidDict.Add(mapUnit.Uid, mapUnit);
            self.IdDict.Add(mapUnit.Id, mapUnit);
            self.MapUnitList.Add(mapUnit);
            var roomComponent = Game.Scene.GetComponent<RoomComponent>();
            
            // 同步房間資訊
            await roomComponent.Update(self);
        }

        public static async ETTask RemoveMapUnitByUid(this Room self, long uid)
        {
            RoomComponent roomComponent = Game.Scene.GetComponent<RoomComponent>();

            if (!self.UidDict.TryGetValue(uid, out MapUnit mapUnit))
            {
                return;
            }
            self.MapUnitList.Remove(mapUnit);
            self.IdDict.Remove(mapUnit.Id);
            self.UidDict.Remove(uid);

            // 同步房間資訊
            await roomComponent.Update(self);
        }
    }
}