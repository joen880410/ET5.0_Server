using System;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class L2M_MapUnitCreateHandler : AMRpcHandler<L2M_MapUnitCreate, M2L_MapUnitCreate>
    {
        protected override void Run(Session session, L2M_MapUnitCreate message, Action<M2L_MapUnitCreate> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        protected async ETTask RunAsync(Session session, L2M_MapUnitCreate message, Action<M2L_MapUnitCreate> reply)
        {
            M2L_MapUnitCreate response = new M2L_MapUnitCreate();
            try
            {
                MapUnitComponent mapUnitComponent = Game.Scene.GetComponent<MapUnitComponent>();
                RoomComponent roomComponent = Game.Scene.GetComponent<RoomComponent>();
                MapUnit mapUnit = mapUnitComponent.GetByUid(message.Uid);
                if (mapUnit != null)
                {
                    mapUnitComponent.Remove(mapUnit.Id);
                }
                // 建立MapUnit
                mapUnit = ComponentFactory.CreateWithId<MapUnit, MapUnitType>(IdGenerater.GenerateId(), MapUnitType.Hero);
                mapUnit.Uid = message.Uid;
                await mapUnit.AddComponent<MailBoxComponent>().AddLocation();
                mapUnit.AddComponent<MapUnitGateComponent, long>(message.GateSessionId);

                var deviceComponent = Game.Scene.GetComponent<DeviceComponent>();
                if (deviceComponent == null)
                {
                    response.Error = ErrorCode.ERR_DeviceComponentNull;
                    reply(response);
                    return;
                }

                MapUnitInfo mapUnitInfo = BsonSerializer.Deserialize<MapUnitInfo>(message.MapUnitInfo);
                mapUnit.SetInfo(mapUnitInfo);
                mapUnitComponent.Add(mapUnit);
                await mapUnit.EnterRoom(mapUnitInfo.RoomId);
                // 設定運動類型
                await roomComponent.Update(mapUnit.Room);
                response.MapUnitInfo = mapUnit.Info.ToJson();
                // 回傳MapUnitId給進入者
                response.MapUnitId = mapUnit.Id;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}