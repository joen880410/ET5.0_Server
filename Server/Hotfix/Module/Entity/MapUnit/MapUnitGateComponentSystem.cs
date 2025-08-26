using ETModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace ETHotfix
{
    [ObjectSystem]
    public class MapUnitGateComponentAwakeSystem : AwakeSystem<MapUnitGateComponent, long>
    {
        public override void Awake(MapUnitGateComponent self, long a)
        {
            self.Awake(a);
        }
    }

    public static class MapUnitGateComponentSystem
    {
        public static void Disconnect(this MapUnitGateComponent self)
        {
            MapUnit mapUnit = self.GetParent<MapUnit>();
            Console.WriteLine($"{DateTime.Now} Disconnect MapUnit:{mapUnit.Uid}");
            self.IsDisconnect = true;
            Game.Scene.GetComponent<MapUnitComponent>().Remove(mapUnit.Id);
        }

        public static void Reconnect(this MapUnitGateComponent self)
        {
            MapUnit mapUnit = self.GetParent<MapUnit>();
            Console.WriteLine($"{DateTime.Now} Reconnect MapUnit:{mapUnit}");
            self.IsDisconnect = false;
        }
    }
}