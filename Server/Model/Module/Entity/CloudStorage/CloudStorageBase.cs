using System;
using System.IO;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public abstract class CloudStorageBase : IDisposable
	{
        public string bucketName { protected set; get; }

        public CloudStorageBase(string bucketName)
        {
            this.bucketName = bucketName;
        }

        /// <summary>
        /// 上傳並取代原本的物件
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="stream"></param>
        /// <param name="storageOptions"></param>
        /// <returns></returns>
        public abstract ETTask<Google.Apis.Storage.v1.Data.Object> UploadObject(string objectName, Stream stream, StorageOptions storageOptions);

        public abstract ETTask<Google.Apis.Storage.v1.Data.Object> GetObject(string objectName);

        public abstract ETTask DeleteObject(string objectName);

        public virtual void Dispose() { }
    }
}