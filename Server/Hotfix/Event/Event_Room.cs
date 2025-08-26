using ETModel;
using System;
using MongoDB.Bson;

namespace ETHotfix
{
    [Console(AppType.Map, CommandPattern.ShowRoom, ConsoleService.Any)]
    public class Event_Room : AConsoleHandler<long>
    {
        public override async ETTask<ConsoleResult> Execute(long roomId)
        {
            var roomComponent = Game.Scene.GetComponent<RoomComponent>();
            if (roomComponent == null)
            {
                Log.Error("roomComponent is null");
                return ConsoleResult.Error(msg: "roomComponent is null");
            }
            var room = roomComponent.Get(roomId);
            if (room == null)
            {
                Console.WriteLine("room is null");
                return ConsoleResult.Error(msg: "room is null");
            }
            Console.WriteLine($"Room[{roomId}]: {room.ToJson()}");
            await ETTask.CompletedTask;
            return ConsoleResult.Ok(msg: $"Room[{roomId}]: {room.ToJson()}",info:roomId.ToString());
        }
    }
}