using ETModel;
using System;
using MongoDB.Bson;

namespace ETHotfix
{
    [Console(AppType.DB | AppType.Realm | AppType.Gate | AppType.Lobby | AppType.Map, CommandPattern.ShowPlayer, ConsoleService.Self)]
    public class Event_ShowPlayer : AConsoleHandler<long>
    {
        public override async ETTask<ConsoleResult> Execute(long uid)
        {
            var cacheProxyComponent = Game.Scene.GetComponent<CacheProxyComponent>();
            if (cacheProxyComponent == null)
            {
                Console.WriteLine("cacheProxyComponent is null");
                return ConsoleResult.NotSupported(msg: "cacheProxyComponent is null");
            }
            var proxy = cacheProxyComponent.GetMemorySyncSolver<Player>();
            if (proxy == null)
            {
                Console.WriteLine("cache proxy is null");
                return ConsoleResult.NotSupported(msg: "cache proxy is null");
            }
            var player = proxy.Get<Player>(uid);
            if (player == null)
            {
                Console.WriteLine("player is null");
                return ConsoleResult.NotSupported(msg: "player is null");
            }
            Console.WriteLine($"Player[{uid}]: {player.ToJson()}");
            await ETTask.CompletedTask;
            return ConsoleResult.Ok(msg: $"Player[{uid}]: {player.ToJson()}",info:uid.ToString());
        }
    }
}