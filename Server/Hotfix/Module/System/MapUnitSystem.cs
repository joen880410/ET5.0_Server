using ETHotfix.Share;
using ETModel;
using System;

namespace ETHotfix
{
    [ObjectSystem]
    public class MapUnitAwakeSystem : AwakeSystem<MapUnit, MapUnitType>
    {
        public override void Awake(MapUnit self, MapUnitType a)
        {
            self.Awake(a);
        }
    }

    public static class MapUnitSystem
    {
        #region Room

        public static async ETTask<bool> EnterRoom(this MapUnit self, long roomId)
        {
            Room room = Game.Scene.GetComponent<RoomComponent>().Get(roomId);
            if (room == null)
                return false;
            self.Room = room;
            self.RoomId = room.Id;
            if (room.type != (int)RoomUtility.RoomType.Roaming)
            {
                self.StartRideTimeUtcTick = -1;
            }
            await room.AddMapUnit(self);
            return true;
        }

        public static async ETTask LeaveRoom(this MapUnit self)
        {
            if (self.Room != null)
            {
                await self.Room.RemoveMapUnitByUid(self.Uid);
                self.Room = null;
                self.RoomId = -1;
            }
        }
        
        #endregion

    }
}