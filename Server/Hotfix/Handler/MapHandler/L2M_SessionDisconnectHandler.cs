using System;
using System.Collections.Generic;
using ETModel;
using RoomType = ETModel.Share.RoomUtility.RoomType;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class L2M_SessionDisconnectHandler : AMActorLocationHandler<MapUnit, L2M_SessionDisconnect>
    {
        protected override void Run(MapUnit mapUnit, L2M_SessionDisconnect message)
        {
            RunAsync(mapUnit, message);
        }

        private void RunAsync(MapUnit mapUnit, L2M_SessionDisconnect message)
        {
            try
            {
                Room room = mapUnit.Room;
                //公開賽在排行榜
                if (room != null && !room.IsDisposed)
                {
                    // 刪除MapUnit
                    M2C_MapUnitDestroy m2C_MapUnitDestroy = new M2C_MapUnitDestroy();
                    m2C_MapUnitDestroy.Uid = mapUnit.Uid;

                    // 製作廣播列表
                    List<MapUnit> broadcastMapUnits = new List<MapUnit>();
                    broadcastMapUnits.AddRange(room.GetAll());
                    for (int i = 0; i < broadcastMapUnits.Count; i++)
                    {
                        // 過濾自己
                        if (broadcastMapUnits[i].Uid == mapUnit.Uid)
                        {
                            broadcastMapUnits.RemoveAt(i);
                            break;
                        }
                    }
                    MapMessageHelper.BroadcastTarget(m2C_MapUnitDestroy, broadcastMapUnits);
                }

                // 中斷指定玩家與Map的連接
                mapUnit.GetComponent<MapUnitGateComponent>().Disconnect();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
