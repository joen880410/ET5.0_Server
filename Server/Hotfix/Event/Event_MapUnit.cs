using ETModel;
using System;
using MongoDB.Bson;

namespace ETHotfix
{
    [Console(AppType.Map,CommandPattern.ShowMapUnit,ConsoleService.Self)]
    public class Event_ShowMapUnit : AConsoleHandler<long>
    {
        public override async ETTask<ConsoleResult> Execute(long mapUnitId)
        {
            await ETTask.CompletedTask;

            var mapUnitComponent = Game.Scene.GetComponent<MapUnitComponent>();
            if (mapUnitComponent == null)
            {
                return ConsoleResult.Error(msg:"MapUnitComponent is null");
            }
            var mapUnit = mapUnitComponent.Get(mapUnitId);
            if (mapUnit == null)
            {
                return ConsoleResult.Error(msg: "mapUnit is null");
            }
            return ConsoleResult.Ok(msg: $"MapUnitId[{mapUnitId}]-> Info:{mapUnit.Info.ToJson()}, \n StartTime:{mapUnit.StartRideTimeUtcTick} EndTime:{mapUnit.EndRideTimeUtcTick}");
        }
    }
    
}