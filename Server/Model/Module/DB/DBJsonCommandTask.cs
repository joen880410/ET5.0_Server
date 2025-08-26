using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using StackExchange.Redis;
using static System.Collections.Specialized.BitVector32;

namespace ETModel
{
    [ObjectSystem]
    public class DBJsonTaskAwakeSystem : AwakeSystem<DBJsonCommandTask, string, ETTaskCompletionSource<List<BsonDocument>>>
    {
        public override void Awake(DBJsonCommandTask self, string json, ETTaskCompletionSource<List<BsonDocument>> tcs)
        {
            self.Json = json;
            self.Tcs = tcs;
        }
    }
    public class DBJsonCommandTask : DBTask
    {
        public string Json { get; set; }

        public ETTaskCompletionSource<List<BsonDocument>> Tcs { get; set; }

        public override async ETTask Run()
        {
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            try
            {
                // 执行查询数据库任务
                if (!BsonDocument.TryParse(Json, out var doc))
                {
                    throw new Exception("Invalid bson document string format");
                }

                var result = await dbComponent.RunFindCommandAsync(doc);
                List<BsonDocument> cursor = result.ToBsonDocumentList();
                this.Tcs.SetResult(cursor);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"查询数据库异常! {this.Json}", e));
            }
        }
    }
}
