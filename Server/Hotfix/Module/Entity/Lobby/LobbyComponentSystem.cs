using System.Collections.Generic;
using ETModel;
using System;
using Google.Protobuf.Collections;
using MongoDB.Bson.Serialization;
using RoomType = ETModel.Share.RoomUtility.RoomType;
using ETHotfix.Share;
using MongoDB.Bson;

namespace ETHotfix
{
    [ObjectSystem]
    public class LobbyComponentStartSystem : StartSystem<LobbyComponent>
    {
        public override void Start(LobbyComponent self)
        {
            self.Start();
        }
    }

    public static class LobbyComponentSystem
    {
        public static async ETTask<Room> CreateRoom(this LobbyComponent self, int mapAppId, RoomInfo roomInfo)
        {
            // 等候房間創建完成，並且也同步Room到了Lobby
            Session mapSession = SessionHelper.GetMapSession(mapAppId);
            M2L_TeamCreate m2L_TeamCreate = (M2L_TeamCreate)await mapSession.Call(new L2M_TeamCreate 
            {
                Info = roomInfo.ToJson(),
            });

            if(m2L_TeamCreate.Error != ErrorCode.ERR_Success)
            {
                return null;
            }

            Room room = BsonSerializer.Deserialize<Room>(m2L_TeamCreate.Json);
            CacheExHelper.WriteInCache(room, out room);

            return room;
        }
        

        public static async ETTask<bool> DestroyRoom(this LobbyComponent self, long roomId)
        {
            int mapAppId = IdGenerater.GetAppId(roomId);
            Session mapSession = SessionHelper.GetMapSession(mapAppId);
            M2L_DestroyRoom m2L_DestroyRoom = (M2L_DestroyRoom)await mapSession.Call(new L2M_DestroyRoom
            {
                RoomId = roomId,
            });
            if (m2L_DestroyRoom.Error != ErrorCode.ERR_Success)
            {
                return false;
            }
            return true;
        }

        public static async ETTask<bool> BroadcastTeamModifyMember(this LobbyComponent self, long uid, long roomId)
        {
            L2M_TeamModifyMember l2M_TeamModifyMember = new L2M_TeamModifyMember
            {
                Uid = uid,
                RoomId = roomId,
            };
            int mapAppId = IdGenerater.GetAppId(roomId);
            Session mapSession = SessionHelper.GetMapSession(mapAppId);
            M2L_TeamModifyMember m2L_TeamModifyMember = (M2L_TeamModifyMember)await mapSession.Call(l2M_TeamModifyMember);
            if (m2L_TeamModifyMember.Error != ErrorCode.ERR_Success)
            {
                return false;
            }
            return true;
        }

        public static async ETTask<bool> BroadcastTeamLose(this LobbyComponent self, long roomId)
        {
            L2M_TeamLose l2M_TeamLose = new L2M_TeamLose
            {
                RoomId = roomId,
            };
            int mapAppId = IdGenerater.GetAppId(roomId);
            Session mapSession = SessionHelper.GetMapSession(mapAppId);
            M2L_TeamLose m2L_TeamLose = (M2L_TeamLose)await mapSession.Call(l2M_TeamLose);
            if (m2L_TeamLose.Error != ErrorCode.ERR_Success)
            {
                return false;
            }
            return true;
        }

        public static async ETTask<bool> DestroyMapUnit(this LobbyComponent self, long mapUnitId)
        {
            if(mapUnitId == 0)
            {
                return false;
            }
            L2M_DestroyMapUnit l2M_DestroyMapUnit = new L2M_DestroyMapUnit
            {
                mapUnitId = mapUnitId,
            };
            int mapAppId = IdGenerater.GetAppId(mapUnitId);
            Session mapSession = SessionHelper.GetMapSession(mapAppId);
            M2L_DestroyMapUnit m2L_TeamLose = (M2L_DestroyMapUnit)await mapSession.Call(l2M_DestroyMapUnit);
            if (m2L_TeamLose.Error != ErrorCode.ERR_Success)
            {
                return false;
            }
            return true;
        }

        public static async ETTask<int> LeaveRoom(this LobbyComponent self, long roomId, long uid)
        {
            Room room = self.GetRoom(roomId);
            if(room == null)
            {
                return ErrorCode.ERR_RoomIdNotFound;
            }

            var roomType = room.Type;

            switch (roomType)
            {
                case RoomType.Roaming:
                    return await self.LeaveRoom(roomId, uid);
                default:
                    return ErrorCode.ERR_RoomIdNotFound;
            }
        }

        public static int GetTeamRoomCount(this LobbyComponent self)
        {
            return self.TeamList.Count;
        }

        public static async ETTask CreateAI(this LobbyComponent self, long roomId)
        {
            var mapAppId = IdGenerater.GetAppId(roomId);
            var mapSession = SessionHelper.GetMapSession(mapAppId);
            var response = (M2L_AICreate)await mapSession.Call(new L2M_AICreate()
            {
                RoomId = roomId
            });

            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Trace($"Room Id:{roomId} Error:{response.Error}");
            }
        }
    }
}