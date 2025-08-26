using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ETModel
{
    [ObjectSystem]
    public class DBQueryJsonViewTaskAwakeSystem : AwakeSystem<DBQueryJsonViewTask, string, ETTaskCompletionSource<List<BsonDocument>>>
    {
        public override void Awake(DBQueryJsonViewTask self, string json, ETTaskCompletionSource<List<BsonDocument>> tcs)
        {
            self.Json = json;
            self.Tcs = tcs;
        }
    }
    public class DBQueryJsonViewTask : DBTask
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
                List<BsonDocument> cursor = (await dbComponent.RunFindCommandAsync(doc)).ToBsonDocumentList();
                this.Tcs.SetResult(cursor);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"查询数据库异常! {this.Json}", e));
            }
        }
    }
}
