using System;
using MongoDB.Bson;

namespace ETModel
{
    [ObjectSystem]
    public class DBUploadFileTaskAwakeSystem : AwakeSystem<DBUploadFileTask, ETTaskCompletionSource<ObjectId>>
    {
        public override void Awake(DBUploadFileTask self, ETTaskCompletionSource<ObjectId> tcs)
        {
            self.Tcs = tcs;
        }
    }
    public class DBUploadFileTask : DBTask
    {
        public string FileName { get; set; }
        public byte[] Source { get; set; }
        public BsonDocument Meta { get; set; }
        public ETTaskCompletionSource<ObjectId> Tcs { get; set; }
        public override async ETTask Run()
        {
            var dbComponent = Game.Scene.GetComponent<DBComponent>();
            try
            {
                var id = await dbComponent.UploadFromBytesAsync(FileName, Source, Meta);
                this.Tcs.SetResult(id);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"上傳数据库异常! {FileName}", e));
            }
        }
    }
}
