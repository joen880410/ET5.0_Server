using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MongoDB.Bson;
using ETModel;
using Google.Protobuf.Collections;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETHotfix
{
    [ObjectSystem]
    public class RoomComponentStartSystem : StartSystem<RoomComponent>
    {
        public override void Start(RoomComponent self)
        {
            self.Start();
        }
    }

    public static class RoomComponentSystem
    {
        public static async ETTask<Room> CreateRoom(this RoomComponent self, RoomInfo roomInfo)
        {
            Room room = self.Get(roomInfo.RoomId);
            if (room != null)
            {
                return room;
            }

            room = ComponentFactory.CreateWithId<Room, RoomType>(IdGenerater.GenerateId(), RoomType.Roaming);
            room.SetData(roomInfo);
            await self.MemorySync.Create(room);
            return room;

        }


        public static async ETTask DestroyRoom(this RoomComponent self, long id)
        {
            Room room = self.Get(id);

            if (room == null)
                return;
            await self.Delete(room);
        }

        public static async ETTask<Room> Update(this RoomComponent self, Room room)
        {
            if (room == null)
            {
                Log.Error("room is null");
                return null;
            }
            return await self.MemorySync.Update(room);
        }
        public static async ETTask<bool> Delete(this RoomComponent self, Room room)
        {
            return await self.MemorySync.Delete<Room>(room.Id);
        }
    }
}