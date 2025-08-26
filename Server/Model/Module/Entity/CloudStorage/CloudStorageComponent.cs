using System;
using System.IO;

namespace ETModel
{
    [ObjectSystem]
    public class CloudStorageComponentAwakeSystem : AwakeSystem<CloudStorageComponent>
    {
        public override void Awake(CloudStorageComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class CloudStorageComponentDestroySystem : DestroySystem<CloudStorageComponent>
    {
        public override void Destroy(CloudStorageComponent self)
        {
            self.Destroy();
        }
    }

    public class StorageOptions
    {
        public string contentType;
        public DateTime? retentionExpirationTime;
    }

    public class CloudStorageComponent : Component
    {
        private CloudStorageBase cloudStorage;

        private CloudStorageConfig cloudStorageConfig;

        public string Uri => cloudStorageConfig.Uri;

        public void Awake()
        {
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();
            cloudStorageConfig = startConfigComponent.HttpConfig.GetComponent<CloudStorageConfig>();
            cloudStorage = cloudStorageConfig.GetCloudStorage();
        }

        public ETTask<Google.Apis.Storage.v1.Data.Object> GetObject(string objectName) 
        {
            return cloudStorage.GetObject(objectName);
        }

        /// <summary>
        /// 上傳並取代原本的物件
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="stream"></param>
        /// <param name="storageOptions"></param>
        /// <returns></returns>
        public ETTask<Google.Apis.Storage.v1.Data.Object> UploadObject(string objectName, Stream stream, StorageOptions storageOptions)
        {
            return cloudStorage.UploadObject(objectName, stream, storageOptions);
        }

        public void UpdateObject()
        {

        }

        /// <summary>
        /// 刪除物件
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public ETTask DeleteObject(string objectName)
        {
            return cloudStorage.DeleteObject(objectName);
        }

        public void Destroy()
        {
            cloudStorage.Dispose();
        }
    }
}