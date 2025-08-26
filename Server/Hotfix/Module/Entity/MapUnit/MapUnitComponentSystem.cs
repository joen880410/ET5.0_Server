using ETModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace ETHotfix
{
    [ObjectSystem]
    public class MapUnitComponentDestorySystem : DestroySystem<MapUnitComponent>
    {
        public override void Destroy(MapUnitComponent self)
        {
            var all_MapUnits = self.GetAll();
            for (int i = 0; all_MapUnits.Length > i; i++)
            {
                var unit = all_MapUnits[i];
                self.Remove(unit.Uid);
            }
            self.Destroy();
        }
    }

    public static class MapUnitComponentSystem
    {
        public static async void Remove(this MapUnitComponent self, long id)
        {
            var mapUnit = self.Get(id);
            await mapUnit.LeaveRoom();
            self.RemoveUnit(mapUnit);
            mapUnit.Dispose();
        }
        public static void Remove(this MapUnitComponent self, List<long> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                var id = ids[i];
                self.Remove(id);
            }
        }
    }
}