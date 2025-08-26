using ETHotfix.Share;
using ETModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ETHotfix
{

    [Console(AppType.DB, CommandPattern.CreateUserRandomly, ConsoleService.Self)]
    public class Event_CreateUser : AConsoleHandler<int>
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random random = new Random();

        public override async ETTask<ConsoleResult> Execute(int count)
        {
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            if (count <= 0)
            {
                return ConsoleResult.Error(msg: "expect count > 0 but <= 0 that is invalid!");
            }
            Queue<string> emails = new Queue<string>(count);
            List<string> existed = new List<string>();
            string prefix = string.Empty;
            for (int i = 0; i < count; i++)
            {
                do
                {
                    prefix = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                } while (emails.Contains(prefix));
                emails.Enqueue(prefix);
            }
            ETTask[] taskAll = new ETTask[count];
            for (int j = 0; j < count; j++)
            {
                prefix = emails.Dequeue();
                taskAll[j] = AuthenticationHelper.AuthenticationByBot(prefix);
            }
            await ETTask.WaitAll(taskAll);

            return ConsoleResult.Ok(msg: "to create all users is finished!");
        }
    }

    [Console(AppType.DB, CommandPattern.CreateUserRandomlyCommand, ConsoleService.Self)]
    public class Event_CreateUserCommand : AConsoleHandler<int>
    {
        public override async ETTask<ConsoleResult> Execute(int count)
        {
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            if (count <= 0)
            {
                return ConsoleResult.Error(msg: "expect count > 0 but <= 0 that is invalid!");
            }
            Queue<string> emails = new Queue<string>(count);
            List<string> existed = new List<string>();
            string prefix = string.Empty;
            for (int i = 0; i < count; i++)
            {

                prefix = Guid.NewGuid().ToString("N");
                emails.Enqueue(prefix);
            }
            var userArray = new BsonArray();
            var thirdPartyUserArray = new BsonArray();
            for (int j = 0; j < count; j++)
            {
                prefix = emails.Dequeue();
                var user = AuthenticationHelper.CreateNewUser(10, User.Identity.TestPlayer); ;
                var thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Guest.ToString();
                thirdPartyUser.userId = prefix;
                thirdPartyUser.name = "";
                thirdPartyUser.gender = "";
                thirdPartyUser.location = "";
                thirdPartyUser.email = "";
                thirdPartyUser.birthday = "";
                thirdPartyUser.createAt = user.createAt;
                userArray.Add(user.ToBsonDocument());
                thirdPartyUserArray.Add(thirdPartyUser.ToBsonDocument());
                Console.WriteLine($"to create user {j}:{user.name} is successful!");
                if ((j + 1) % 1000 == 0)
                {
                    var command = new BsonDocument
                    {
                            { "insert", "User" },
                            { "documents", userArray }
                    };
                    var command2 = new BsonDocument
                    {
                        { "insert", "ThirdPartyUser" }, { "documents", thirdPartyUserArray }
                    };
                    await Game.Scene.GetComponent<DBComponent>().database.RunCommandAsync<BsonDocument>(command);
                    await Game.Scene.GetComponent<DBComponent>().database.RunCommandAsync<BsonDocument>(command2);
                    userArray.Clear();
                    thirdPartyUserArray.Clear();
                }
            }
            return ConsoleResult.Ok(msg: "to create all users is finished!");
        }
    }

    [Console(AppType.DB, CommandPattern.DeleteAllTestUser, ConsoleService.Self)]
    public class Event_DeleteAllTestUser : AConsoleHandler
    {
        public override async ETTask<ConsoleResult> Execute()
        {
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            var proxy = Game.Scene.GetComponent<DBProxyComponent>();
            var results = await proxy.Query<User>(entity => entity.identity == (int)User.Identity.TestPlayer);
            var userIds = results.Select(e => e.Id);
            await proxy.DeleteJson<User>(entity => userIds.Contains(entity.Id));
            return ConsoleResult.Ok(msg: "to delete all test user is successful!");


        }
    }
    [Console(AppType.DB, CommandPattern.DeleteUser, ConsoleService.Self)]
    public class Event_DeleteUser : AConsoleHandler<long>
    {
        public override async ETTask<ConsoleResult> Execute(long uid)
        {
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            await UserDataHelper.DeleteAccount(uid);
            return ConsoleResult.Ok(msg: $"to delete user : {uid} is successful!");


        }
    }
    [Console(AppType.DB, CommandPattern.CreateManagerUser, ConsoleService.Self)]
    public class Event_CreateManagerUser : AConsoleHandler<string, string, string>
    {
        public override async ETTask<ConsoleResult> Execute(string account, string password, string Email)
        {
            var db = Game.Scene.GetComponent<DBComponent>();
            if (db == null)
            {
                return ConsoleResult.NotSupported(msg: "To run the command only AppType == DB");
            }

            var result = await UserDataHelper.SignUpGameManager(account, password, Email);
            if (result == ErrorCode.ERR_Success)
            {
                return ConsoleResult.Ok(msg: "to create all users is finished!");
            }
            return ConsoleResult.Error();
        }
    }
}