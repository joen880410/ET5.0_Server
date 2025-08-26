using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using MongoDB.Driver;
using Google.Cloud.Storage.V1;

namespace ETModel
{
    [CloudStorage]
    public class GoogleCloudStorage : CloudStorageBase
	{
        /// <summary>
        /// Google Cloud Platform project ID.
        /// </summary>
        public const string projectId = "jplay-245602";

        private StorageClient storageClient;

        private Dictionary<string, string> metaData = new Dictionary<string, string>
        {
            // 不進行快取
            { "max-age", "0" }
        };

        public GoogleCloudStorage(string bucketName) : base(bucketName)
        {
            const string credentialFileName = "google_storage_key.json";

            // 讀取憑證文件並產生憑證物件
            GoogleCredential googleCredential = null;
            string[] paths = new string[]
            {
                Path.Combine("..", "Config", "Key", credentialFileName),
                Path.Combine("..", "..", "Config", "Key", credentialFileName)
            };
            string path = paths.FirstOrDefault(e => File.Exists(e));
            if (string.IsNullOrEmpty(path))
            {
                Log.Error($"GoogleCredential's google_storage_key.json doesnt exist on the path!");
                return;
            }
            else
            {
                googleCredential = GoogleCredential.FromFile(path);
            }

            // Instantiates a client.
            storageClient = StorageClient.Create(googleCredential);

            try
            {
                var bucket = storageClient.GetBucket(bucketName);
                if (bucket == null)
                {
                    // Creates the new bucket.
                    bucket = storageClient.CreateBucket(projectId, bucketName, new CreateBucketOptions
                    {
                        PredefinedAcl = PredefinedBucketAcl.PublicRead,
                        PredefinedDefaultObjectAcl = PredefinedObjectAcl.PublicRead,
                        Projection = Projection.Full,
                    });

                    var msg = $"Bucket {bucket.Name} created.";
                    Console.WriteLine(msg);
                    Log.Info(msg);
                }
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                // The bucket already exists.  That's fine.
                Console.WriteLine(e.Error.Message);
                Log.Info(e.Error.Message);
            }
        }

        public override async ETTask<Google.Apis.Storage.v1.Data.Object> UploadObject(string objectName, Stream stream, StorageOptions storageOptions)
        {
            try
            {
                var obj = new Google.Apis.Storage.v1.Data.Object();
                obj.Bucket = bucketName;
                obj.Name = objectName;
                obj.ContentType = storageOptions.contentType;
                obj.CacheControl = "no-cache";
                obj.Metadata = metaData;
                obj.RetentionExpirationTime = storageOptions.retentionExpirationTime;

                var result = await storageClient.UploadObjectAsync(obj, stream);
                return result;
            }
            catch(Exception e)
            {
                Log.Error(e);
            }
            return null;
        }

        public override async ETTask<Google.Apis.Storage.v1.Data.Object> GetObject(string objectName)
        {
            try
            {
                var result = await storageClient.GetObjectAsync(bucketName, objectName);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return null;
        }

        public override async ETTask DeleteObject(string objectName)
        {
            try
            {
                await storageClient.DeleteObjectAsync(bucketName, objectName);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public override void Dispose()
        {
            storageClient.Dispose();
        }
    }
}