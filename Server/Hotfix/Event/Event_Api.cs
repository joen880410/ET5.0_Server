using ETModel;
using System;

namespace ETHotfix
{
    [Event("11111111")]
    [Console(AppType.DB, CommandPattern.CreateApi, ConsoleService.Self)]
    
    public class Event_CreateApi : AConsoleHandler<string, string>
    {
        public override async ETTask<ConsoleResult> Execute(string type, string ApiName)
        {
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            if (Enum.TryParse(type, out ApiType apiType))
            {
                return ConsoleResult.NotSupported(msg: "ApiType no convert ");
            }
            var result = await APIDataHelper.FindOneAPIAuthorization(apiType, ApiName);
            if (result != null)
            {
                return ConsoleResult.Error(msg: "this apiName is Exist");
            }
            else
            {
                result = await APIDataHelper.SignUpAPIAuthorization(apiType, ApiName);
                return ConsoleResult.Ok(msg: $"SignUp API Authorization Successful :{result.HashKey}");
            }
        }
    }

}