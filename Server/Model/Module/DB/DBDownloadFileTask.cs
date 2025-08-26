using System;
using MongoDB.Bson;

namespace ETModel
{
    [ObjectSystem]
    public class DBDownloadFileTaskAwakeSystem : AwakeSystem<DBDownloadFileTask, ObjectId, ETTaskCompletionSource<byte[]>>
    {
        public override void Awake(DBDownloadFileTask self, ObjectId objId, ETTaskCompletionSource<byte[]> tcs)
        {
            self.ObjId = objId;
            self.Tcs = tcs;
        }
    }
    public class DBDownloadFileTask : DBTask
    {
        public ObjectId ObjId { get; set; }
        public ETTaskCompletionSource<byte[]> Tcs { get; set; }
        public override async ETTask Run()
        {
            var dbComponent = Game.Scene.GetComponent<DBComponent>();
            try
            {
                var id = await dbComponent.DownloadAsBytesAsync(ObjId);
                this.Tcs.SetResult(id);
            }
            catch (Exception e)
            {
                this.Tcs.SetException(new Exception($"下載数据库异常! {ObjId}", e));
            }
        }
    }
}
