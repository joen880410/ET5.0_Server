using ETModel;
using MongoDB.Bson;
using System;

namespace ETHotfix
{
    [Console(AppType.AllServer, CommandPattern.ReloadConfig, ConsoleService.All)]
    public class ConfigRoload : AConsoleHandler
    {
        public override async ETTask<ConsoleResult> Execute()
        {
            await ETTask.CompletedTask;

            Game.Scene.GetComponent<ConfigComponent>().isLoadDB = false;
            await Game.Scene.GetComponent<ConfigComponent>().LoadToDB();
            Game.EventSystem.Run(EventIdType.SyncAllRoamingRoom);
            return ConsoleResult.Ok();
        }
    }


    [Console(AppType.AllServer, CommandPattern.Reload, ConsoleService.All)]
    public class Roload : AConsoleHandler
    {
        public override async ETTask<ConsoleResult> Execute()
        {
            try
            {
                var a = DateTime.UtcNow.ToJson();
                var aaa = JsonHelper.FromJson(typeof(DateTime), a);
            }
            catch (Exception e)
            {

            }
            return ConsoleResult.Ok();
        }
    }


}
