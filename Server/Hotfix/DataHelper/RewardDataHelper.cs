using ETHotfix.Share;
using ETModel;
using Google.Protobuf.Collections;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using RewardType = ETModel.RewardCode.RewardType;

namespace ETHotfix
{
    public static class RewardDataHelper
    {
        private const int INVALID = -1;
        private static DBProxyComponent dbProxy
        {
            get
            {
                return Game.Scene.GetComponent<DBProxyComponent>();
            }
        }
        private static RewardCodeComponent rewardCodeComponent
        {
            get
            {
                return Game.Scene.GetComponent<RewardCodeComponent>();
            }
        }

        
        public static string ReleaseKeyName(this RewardCode rewardcode)
        {
            return $"{rewardcode.Id}_Release";
        }
        public static string ExpiredKeyName(this RewardCode rewardcode)
        {
            return $"{rewardcode.Id}_Expired";
        }

        public static async ETTask<List<RewardCode>> FindAllRewardCode()
        {
            var list = await dbProxy.Query<RewardCode>(entity => true);
            return list.OfType<RewardCode>().ToList();
        }

        private static async ETTask<RewardCode> FindOneRewardCode(string rewardCode)
        {
            var list = await dbProxy.Query<RewardCode>(entity => entity.rewardcode == rewardCode);
            return (RewardCode)list.FirstOrDefault();
        }
        public static async ETTask<RewardCode> CreateRewardCode(string code, long expireAt, int count, RewardType rewardType)
        {
            var rewardId = RewardCodeHelper.GetRewardId(code);
            RewardCode rewardCode = ComponentFactory.CreateWithId<RewardCode>(IdGenerater.GenerateId());
            rewardCode.rewardType = (int)rewardType;
            rewardCode.rewardcode = code;
            rewardCode.rewardId = rewardId;
            rewardCode.createAt = TimeHelper.NowTimeMillisecond();
            rewardCode.expireAt = expireAt;
            rewardCode.count = count;
            await dbProxy.Save(rewardCode);
            rewardCodeComponent.CreateCron(rewardCode);
            return rewardCode;
        }
        public static async ETTask<RewardCodeRecord> CreateRewardCodeRecord(long uid, long rewardId)
        {
            RewardCodeRecord rewardCodeRecord = ComponentFactory.CreateWithId<RewardCodeRecord>(IdGenerater.GenerateId());
            rewardCodeRecord.uid = uid;
            rewardCodeRecord.rewardcodes = new RepeatedField<long>() { rewardId };
            rewardCodeRecord.createAt = TimeHelper.NowTimeMillisecond();
            await dbProxy.Save(rewardCodeRecord);
            return rewardCodeRecord;
        }
        public static async ETTask<(int errorCode, int rewardId)> UpsertUseRewardCodeRecord(long uid, string rewardCodeStr, DBLog.LogType logType)
        {
            var rewardCode = await FindOneRewardCode(rewardCodeStr);
            if (rewardCode == null)
            {
                return (ErrorCode.ERR_RewardCodeDoesntExist, INVALID);
            }
            var rewardCodeRecord = (await dbProxy.Query<RewardCodeRecord>(e => e.uid == uid)).OfType<RewardCodeRecord>().FirstOrDefault();
            if (rewardCodeRecord == null)
            {
                rewardCodeRecord = await CreateRewardCodeRecord(uid, rewardCode.rewardId);
            }
            else
            {
                if (rewardCode.rewardType == (int)RewardType.OneUserOneTime)
                {
                    if (rewardCodeRecord.rewardcodes.Any(e => e == rewardCode.rewardId))
                    {
                        return (ErrorCode.ERR_RewardCodeAreadlyUse, rewardCode.rewardId);
                    }
                }
                rewardCodeRecord.rewardcodes.Add(rewardCode.rewardId);
                rewardCodeRecord.updateAt = TimeHelper.NowTimeMillisecond();
            }

            var log = new BsonDocument
            {
                ["uid"] = uid,
                ["rewardId"] = rewardCode.rewardId,
                ["rewardCode"] = rewardCode.rewardcode,
                ["updateAt"] = rewardCodeRecord.updateAt,
            };

            if (log.Count() > 0)
            {
                await dbProxy.Save(rewardCodeRecord);
                await dbProxy.SaveLog(rewardCodeRecord.uid, logType, log);
            }
            if (rewardCode.rewardType == (int)RewardType.OnlyOneUse || rewardCode.count == 0)
            {
                await rewardCodeComponent.DeleteRewardCode(rewardCode.Id);
            }
            else if (rewardCode.count != INVALID)
            {
                rewardCode.count--;
                await dbProxy.Save(rewardCode);
            }
            return (ErrorCode.ERR_Success, rewardCode.rewardId);
        }

        public static async ETTask DeleteOneRewardCode(long id)
        {
            await dbProxy.DeleteJson<RewardCode>(e => e.Id == id);
        }
    }
}
