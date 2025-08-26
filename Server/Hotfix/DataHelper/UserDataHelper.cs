using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using ETHotfix.Share;
using ETModel;
using Google.Protobuf.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using RoomSubType = ETHotfix.Share.RoomUtility.RoomSubType;

namespace ETHotfix
{
    //TODO：要改用新的DB組件
    public static class UserDataHelper
    {
        public const int USER_HOBBY_BYTE_LIMIT = 64;

        public const int USER_SELFINTRODUCTION_BYTE_LIMIT = 256;

        public const int USER_RECORDNOTE_BYTE_LIMIT = 256;

        public const int USER_POST_BYTE_LIMIT = 256;

        public const int USER_COMMENT_BYTE_LIMIT = 128;

        public const int USER_CLUB_NAME_LIMIT = 64;

        public const int USER_CLUB_INTRODUCTION_LIMIT = 256;

        public const int USER_CLUB_ANNOUNCEMENT_LIMIT = 128;

        public const int USER_CHANNEL_NAME_LIMIT = 64;

        public const int USER_CHANNEL_INTRODUCTION_LIMIT = 256;

        public const int PLAN_NAME_LIMIT = 256;

        public const int PAGE_DEFAULT_LIMIT = 10;


        public class QueryMileageResult
        {
            public long Uid { set; get; }
            public long Mileage { set; get; }
        }

        private static CloudStorageComponent cloudStorageComponent
        {
            get
            {
                return Game.Scene.GetComponent<CloudStorageComponent>();
            }
        }

        private static StartConfigComponent startConfigComponent
        {
            get
            {
                return Game.Scene.GetComponent<StartConfigComponent>();
            }
        }

        private static CloudStorageConfig cloudStorageConfig
        {
            get
            {
                return startConfigComponent.StartConfig.GetComponent<CloudStorageConfig>();
            }
        }

        private static DBProxyComponent dbProxy
        {
            get
            {
                return Game.Scene.GetComponent<DBProxyComponent>();
            }
        }

        private static DBComponent db => Game.Scene.GetComponent<DBComponent>();

        private readonly static BsonDocument log = new BsonDocument();

        /// <summary>
        /// 查詢區間新增使用者
        /// </summary>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindIntervalUserCteate(long timeFrom, long timeTo)
        {
            var length = await dbProxy.QueryCount<User>(entity => entity.createAt >= timeFrom && entity.createAt <= timeTo);
            var result = await OtherHelper.BatchRun(async (skip, limit) =>
            {
                return await dbProxy.Query<User>(entity => entity.createAt >= timeFrom && entity.createAt <= timeTo, skip, limit);
            }, (int)length);
            return result.OfType<User>().ToList();
        }


        /// <summary>
        /// 用帳戶ID查詢一位使用者
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static async ETTask<User> FindOneUser(long uid)
        {
            var list = await dbProxy.Query<User>(entity => entity.Id == uid);
            if (list.Any())
            {
                return (User)list.First(); 
            }
            return null;
        }
        public static async ETTask<User> FindOneUser(long uid, List<Expression<Func<User, object>>> projections)
        {
            var list = await dbProxy.Query(entity => entity.Id == uid, projections: projections);
            if (list.Any())
            {
                return list.First();
            }
            return null;
        }
        public static async ETTask<User> FindOneUser(long uid, Expression<Func<User, object>> projection)
        {
            var list = await dbProxy.Query(entity => entity.Id == uid, projection: projection);
            if (list.Any())
            {
                return list.First();
            }
            return null;
        }
        public static async ETTask<User> FindOneUserName(string name)
        {
            var list = await dbProxy.Query<User>(entity => entity.name == name);
            if (list.Any())
            {
                return (User)list[0];
            }
            return null;
        }
        public static async ETTask ClearOneUserFirebaseToken(long uid)
        {
            var user = await dbProxy.Query<User>(uid);
            user.firebaseDeviceToken = string.Empty;
            await dbProxy.Save(user);
        }
        /// 查詢所有使用者
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindAllUser()
        {
            long length = await dbProxy.QueryCount<User>(entity => true);
            return await FindAllUser((int)length, (e) => true);
        }
        public static async ETTask<List<User>> FindAllUser(Expression<Func<User, bool>> exp)
        {
            long length = await dbProxy.QueryCount(exp);
            return await FindAllUser((int)length, exp);
        }

        private static async ETTask<List<User>> FindAllUser(int length, Expression<Func<User, bool>> exp)
        {
            var result = await OtherHelper.BatchRun(async (skip, limit) =>
            {
                return await dbProxy.Query(exp, skip, limit);
            }, (int)length);
            return result.OfType<User>().ToList();
        }

        public static async ETTask<List<long>> FindAllUserId()
        {

            var command = new BsonDocument
                {
                    { "find", nameof(User) },
                    { "projection", new BsonDocument
                        {
                            { "_id", 1 },
                        }
                    },
                };

            return (await dbProxy.QueryCommand(command.ToJson())).Select(e => e.ToBsonDocument()["_id"].AsInt64).ToList();
        }

        /// <summary>
        /// 取得活動積分前20名玩家
        /// </summary>
        /// <param name="Sortexp"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindAllUserPeriodScore(Expression<Func<User, object>> Sortexp)
        {
            var users = await dbProxy.QueryDescendingSort(e => true, Sortexp, 0, 20);
            return users.OfType<User>().ToList();
        }
        /// <summary>
        /// 用帳戶名稱查詢一位第三方使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        public static async ETTask<ThirdPartyUser> FindOneThirdPartyUser(string userId, string party)
        {
            var list = await dbProxy.Query<ThirdPartyUser>(entity => entity.userId == userId && entity.party == party);
            if (list.Count != 0)
            {
                return (ThirdPartyUser)list[0];
            }
            return null;
        }
        /// <summary>
        /// 用帳戶名稱查詢一位第三方使用者
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        public static async ETTask<ThirdPartyUser> FindOneThirdPartyUser(long uid, string party)
        {
            var list = await dbProxy.Query<ThirdPartyUser>(entity => entity.uid == uid && entity.party == party);
            if (list.Count != 0)
            {
                return (ThirdPartyUser)list[0];
            }
            return null;
        }
        /// <summary>
        /// 用UID查詢所有第三方使用者
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        public static async ETTask<List<ThirdPartyUser>> FindAllThirdPartyUser(long uid)
        {
            var list = await dbProxy.Query<ThirdPartyUser>(entity => entity.uid == uid);
            return list.OfType<ThirdPartyUser>().ToList();
        }
        /// <summary>
        /// 刪除一位第三方使用者
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static async ETTask DeleteThirdPartyUser(long uid)
        {
            await dbProxy.DeleteJson<ThirdPartyUser>(e => e.uid == uid);
        }
        /// <summary>
        /// 新增一位使用者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async ETTask SignUserUp(User user)
        {
            await ETTask.WaitAll(new ETTask[]
            {
                dbProxy.Save(user),
                dbProxy.SaveLog(user.Id, DBLog.LogType.SignUserUp, user),
            }, null);
        }

        /// <summary>
        /// 更新或插入一位使用者
        /// </summary>
        /// <param name="user"></param>
        /// <param name="logType"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static async ETTask UpsertUser(User user, DBLog.LogType logType = DBLog.LogType.Unknown, BsonDocument log = null)
        {
            await dbProxy.Save(user);
            if (log != null)
            {
                if (log.Any())
                {
                    await dbProxy.SaveLog(user.Id, logType, log);
                }
            }
        }

        public static async ETTask UpsertUserLog(long uid, DBLog.LogType logType, BsonDocument log)
        {
            if (log.Any())
            {
                await dbProxy.SaveLog(uid, logType, log);
            }
        }
        public static async ETTask UpsertUserNoLog(User user)
        {
            await dbProxy.Save(user);
        }
        /// <summary>
        /// 更新或插入一位第三方使用者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async ETTask UpsertThirdPartyUser(ThirdPartyUser user)
        {
            await dbProxy.Save(user);
            await dbProxy.SaveLog(user.Id, DBLog.LogType.BindThirdPartyUser, user);
        }
        /// <summary>
        /// 更新或插入一位第三方刪除的使用者
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async ETTask UpsertThirdPartyUserDeleted(ThirdPartyUserDeleted user)
        {
            await dbProxy.Save(user);
        }

        /// <summary>
        /// 根據條件、筆數跟偏移，尋找使用者們
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindUsers(Expression<Func<User, bool>> predicate, long skip, long limit = 100)
        {
            List<ComponentWithId> list = await dbProxy.Query(predicate, skip, limit);
            return list.OfType<User>().ToList();
        }

        public static async ETTask<List<User>> FindUsersSort(Expression<Func<User, bool>> predicate, long skip, long limit = 10)
        {
            List<ComponentWithId> list = await dbProxy.Query(predicate, skip * limit, limit);
            return list.OfType<User>().ToList();
        }

        public static async ETTask<List<User>> FindUsers(Expression<Func<User, bool>> predicate, Expression<Func<User, object>> projection)
        {
            return await dbProxy.Query(predicate, projection);
        }

        public static async ETTask<List<User>> FindUsers(BsonDocument predicate, BsonDocument projection)
        {
            return (await dbProxy.Query<User>(predicate, projection)).Select(e => BsonSerializer.Deserialize<User>(e)).ToList();
        }

        public static async ETTask<List<User>> FindUsers(Expression<Func<User, bool>> predicate, List<Expression<Func<User, object>>> projections)
        {
            return await dbProxy.Query(predicate, projections);
        }

        /// <summary>
        /// 查詢複數使用者資料
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindUsers(long[] uids)
        {
            var list = await dbProxy.Query<User>(entity => uids.Contains(entity.Id));
            return list.OfType<User>().ToList();
        }

        /// <summary>
        /// 查詢複數使用者資料
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        public static async ETTask<List<User>> FindUsers(List<long> uids)
        {
            var list = await dbProxy.Query<User>(entity => uids.Contains(entity.Id));
            return list.OfType<User>().ToList();
        }

        /// <summary>
        /// 使用帳號密碼註冊//Manager
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask<int> SignUpGameManager(string account, string password, string email, User.Identity identity = User.Identity.Super)
        {
            ThirdPartyUser thirdPartyUser = await FindOneThirdPartyUser(account, ThirdPartyUser.Tag.Account.ToString());
            if (thirdPartyUser != null)
            {
                return ErrorCode.ERR_SignUpFailed;
            }
            else
            {
                var user = AuthenticationHelper.CreateNewUser(41, identity);
                string hashPassword = CryptographyHelper.MD5Encoding(password, user.salt);
                user.email = email;
                user.hashPassword = hashPassword;
                await SignUserUp(user);

                thirdPartyUser = ComponentFactory.CreateWithId<ThirdPartyUser>(IdGenerater.GenerateId());
                thirdPartyUser.uid = user.Id;
                thirdPartyUser.party = ThirdPartyUser.Tag.Account.ToString();
                thirdPartyUser.userId = account;
                thirdPartyUser.name = "";
                thirdPartyUser.gender = "";
                thirdPartyUser.location = "";
                thirdPartyUser.email = email;
                thirdPartyUser.birthday = "";
                thirdPartyUser.createAt = user.createAt;
                await UpsertThirdPartyUser(thirdPartyUser);
                user.Dispose();
                thirdPartyUser.Dispose();

                return ErrorCode.ERR_Success;
            }
        }

        /// <summary>
        /// 註銷使用者
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask<int> DeleteAccount(long uid)
        {
            var thirdPartyUsers = await FindAllThirdPartyUser(uid);
            if (thirdPartyUsers.Count == 0)
            {
                return ErrorCode.ERR_AccountDoesntExist;
            }
            else
            {
                User user = await FindOneUser(uid);
                log.Clear();
                log["createAt"] = TimeHelper.NowTimeMillisecond();
                log["uid"] = user.Id;

                user.userState = (int)User.State.unUse;

                var thirdPartyData = ComponentFactory.CreateWithId<ThirdPartyUserDeleted>(IdGenerater.GenerateId());
                var docs = new BsonArray();
                for (int i = 0; i < thirdPartyUsers.Count; i++)
                {
                    docs.Add(thirdPartyUsers[i].ToBsonDocument());
                }
                thirdPartyData.docs = docs;
                thirdPartyData.createAt = TimeHelper.NowTimeMillisecond();
                await UpsertUser(user, DBLog.LogType.UserDelete, log);
                await UpsertThirdPartyUserDeleted(thirdPartyData);
                await DeleteThirdPartyUser(uid);
                user.Dispose();
                thirdPartyData.Dispose();
                return ErrorCode.ERR_Success;
            }
        }

        public static async ETTask LogSharingOnSocial(long uid, SocialEnum socialType)
        {
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)DBLog.LogType.ShareOnSocial;
            dBLog.document = new BsonDocument
            {
                { "socialType", (int)socialType }, // 使用的社群
            };
            dBLog.createAt = TimeHelper.NowTimeMillisecond();
            await dbProxy.Save(dBLog);
        }

        /// <summary>
        /// 更新異動多少J幣(都會記一個log)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="amountCoin">異動金額</param>
        /// <returns></returns>
        public static async ETTask AmountUserCoin(this User user, long amountCoin, string reason = "")
        {
            var log = new BsonDocument
            {
                [nameof(User.name)] = user.name,
                ["AmountCoin"] = amountCoin,
                ["InitialBalance"] = user.coin,
            };

            long coin = user.coin;
            coin += amountCoin;
            if (coin < 0L)
            {
                coin = 0L;
            }

            if (user.coin != coin)
            {
                user.coin = coin;
                log["FinalBalance"] = user.coin;
                if (!string.IsNullOrEmpty(reason))
                {
                    log["Reason"] = reason;
                }
                await UpsertUser(user, DBLog.LogType.UpdateUserCoin, log);
            }
        }

        /// <summary>
        /// 更新獲得多少J幣在Lobby上
        /// </summary>
        public static async ETTask GainCoinToUserOnLobby(this User user, long amountCoin)
        {
            // 更新獲得多少J幣
            await user.AmountUserCoin(amountCoin);
        }

        /// <summary>
        /// 更新獲得多少J幣
        /// </summary>
        public static async ETTask GainCoinToUserOnMap(this User user, long amountCoin)
        {
            // 更新獲得多少J幣
            await user.AmountUserCoin(amountCoin);
        }
    }
}
