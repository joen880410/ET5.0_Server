using ETModel;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Text;

namespace ETHotfix
{

    [Console(AppType.DB, CommandPattern.AttachSuperUser, ConsoleService.Self)]
    public class Event_AttachUser : AConsoleHandler<string, string, string, long>
    {
        public override async ETTask<ConsoleResult> Execute(string account, string password, string email, long uid)
        {
            BsonDocument log = new BsonDocument();
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            ThirdPartyUser thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(account, ThirdPartyUser.Tag.Account.ToString());
            if (thirdPartyUser == null)
            {
                User user = await UserDataHelper.FindOneUser(uid);
                if (user == null)
                    return ConsoleResult.Error(msg: "Uid not find");
                var salt = CryptographyHelper.GenerateRandomId();
                var hashPassword = CryptographyHelper.MD5Encoding(password, salt);
                user.salt = salt;
                user.hashPassword = hashPassword;
                user.email = email;
                user.identity = (int)User.Identity.Super;

                log["salt"] = user.salt;
                log["hashPassword"] = user.hashPassword;
                log["email"] = user.email;
                log["identity"] = user.identity;
                await UserDataHelper.UpsertUser(user, DBLog.LogType.AttachAccount, log);
                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Account.ToString();
                thirdPartyUser.userId = account;
                thirdPartyUser.createAt = TimeHelper.NowTimeMillisecond();
                await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);
                thirdPartyUser.Dispose();
                user.Dispose();
                return ConsoleResult.Ok(msg: "to create all users is finished!");
            }
            else
                return ConsoleResult.Error(msg: "User have already attach Account");
        }
    }
    [Console(AppType.DB, CommandPattern.ShowSuperUser, ConsoleService.Self)]
    public class Event_ShowSuperUser : AConsoleHandler
    {
        public override async ETTask<ConsoleResult> Execute()
        {
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }
            var proxy = Game.Scene.GetComponent<DBProxyComponent>();
            var results = (await proxy.Query<User>(entity => entity.identity == (int)User.Identity.Super)).OfType<User>().ToList();
            for (int i = 0; i < results.Count; i++)
            {
                var ThirdPartyResults = await UserDataHelper.FindOneThirdPartyUser(results[i].Id, ThirdPartyUser.Tag.Account.ToString());
                Console.WriteLine(string.Format("{0},account:{1},email:{2},Uid:{3}", i + 1, ThirdPartyResults.userId, results[i].email, results[i].Id));
            }
            Console.WriteLine($"total:{results.Count}");
            return ConsoleResult.Ok();


        }
    }
    [Console(AppType.DB, CommandPattern.ChangeSuperUserPasswd, ConsoleService.Self)]
    public class Event_ChangeSuperUserPasswd : AConsoleHandler<string, string>
    {
        public override async ETTask<ConsoleResult> Execute(string account, string password)
        {
            long now = TimeHelper.NowTimeMillisecond();
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }
            BsonDocument log = new BsonDocument();
            // 更新user登入資訊
            var thirdPartyUser = await UserDataHelper.FindOneThirdPartyUser(account, ThirdPartyUser.Tag.Account.ToString());
            if (thirdPartyUser == null)
                return ConsoleResult.Error(msg: "not find account");
            var user = await UserDataHelper.FindOneUser(thirdPartyUser.uid);
            thirdPartyUser.userId = account;

            var salt = CryptographyHelper.GenerateRandomId();
            var hashPassword = CryptographyHelper.MD5Encoding(password, salt);
            user.salt = salt;
            user.hashPassword = hashPassword;
            user.lastCreateTokenAt = now;
            log["changePasswdTime"] = now;  // 更改密碼時間
            log["changeType"] = "console"; // 更改方式
            log["account"] = account; // 更改方式

            await UserDataHelper.UpsertUser(user, DBLog.LogType.UpdateUserPassword, log);
            await UserDataHelper.UpsertThirdPartyUser(thirdPartyUser);
            return ConsoleResult.Ok(msg: "to create all users is finished!");
        }
    }

}