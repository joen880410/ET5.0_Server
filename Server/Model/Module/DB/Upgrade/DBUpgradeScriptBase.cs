using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ETModel
{
    /// <summary>
    /// 執行腳本使用的類別
    /// 請參考MongoDB Command的網址
    /// https://docs.mongodb.com/manual/reference/command/
    /// </summary>
	public abstract class DBUpgradeScriptBase
	{
        protected const int batchSize = 1000;

        public string failedReason { protected set; get; } = string.Empty;

		public abstract int step { get; }

		public virtual string scriptName => GetType().Name.ToLower();

        protected bool isChecked { private set; get; } = false;

		public DBComponent db => Game.Scene.GetComponent<DBComponent>();

        public async ETTask<bool> IsValid()
        {
            if (!isChecked) 
            {
                return false;
            }
            try
            {
                return await _IsValid();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return false;
            }
        }

        protected abstract ETTask<bool> _IsValid();

        public async ETTask Run()
        {
            try
            {
                await _Run();
                isChecked = true;
            }
            catch(Exception e)
            {
                isChecked = false;
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
            }
        }

        protected abstract ETTask _Run();

        /// <summary>
        /// 該查詢有處理批次的問題
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<List<BsonDocument>> RunFindCommandAsync(BsonDocument command) 
        {
            return (await db.RunFindCommandAsync(command, batchSize)).ToBsonDocumentList();
        }

        /// <summary>
        /// 新增升級腳本DBLog，提供Market版本資料轉換紀錄
        /// </summary>
        protected async ETTask UpsertLog(DBLog.LogType logType, List<BsonDocument> logs)
        {
            if (logs.Count <= 0)
            {
                return;
            }

            var list = new List<BsonDocument>();
            for (int i = 0; i < logs.Count; i++)
            {
                var log = logs[i];
                var uid = log["uid"].AsInt64;
                log.Remove("uid");

                list.Add(new BsonDocument
                {
                    ["_id"] = IdGenerater.GenerateId(),
                    ["_t"] = "DBLog",
                    ["C"] = new BsonArray(),
                    ["uid"] = uid,
                    ["logType"] = (int)logType,
                    ["document"] = log,
                    ["createAt"] = TimeHelper.NowTimeMillisecond()
                });
            }

            var command = new BsonDocument
            {
                { "insert", "DBLog" },
                { "documents", new BsonArray(list) },
                { "ordered", false },
                { "writeConcern", new BsonDocument
                    {
                        { "w", "majority" },
                        { "wtimeout", 5000 }
                    }
                }
            };

            var result = await db.database.RunCommandAsync<BsonDocument>(command);
            Console.WriteLine($"Log:{result.ToJson()}");
        }
    }
}
