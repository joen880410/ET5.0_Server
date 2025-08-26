using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace ETModel
{
	[BsonIgnoreExtraElements]
	public class CloudStorageConfig : AConfigComponent
	{
		public string ImageRootPath => $"{ModeRootDir}/{ImageRootDir}";

		public string RideRecordRootPath => $"{ModeRootDir}/{RideRecordRootDir}";

		public string RunRecordRootPath => $"{ModeRootDir}/{RunRecordRootDir}";

        public string JsgRecordRootPath => $"{ModeRootDir}/{JsgRecordRootDir}";

        public string JsgGolfRecordRootPath => $"{ModeRootDir}/{JsgRecordRootDir}/golf/";

        public string ModeRootDir { get; set; }

		public string ImageRootDir { get; set; }

		public string RideRecordRootDir { get; set; }

		public string RunRecordRootDir { get; set; }

        public string JsgRecordRootDir { get; set; }

        public string Uri { get; set; }

		public string CloudStorageType { get; set; }

		public string BucketName { get; set; }

		public string ProjectId { get; set; }

		public CloudStorageBase GetCloudStorage()
        {
			var types = Game.EventSystem.GetTypes(typeof(CloudStorageAttribute));
			var type = types.FirstOrDefault(e => e.Name == CloudStorageType);

            if (type != null)
            {
				return (CloudStorageBase)Activator.CreateInstance(type, BucketName);
			}
			throw new Exception("cloud storage setting not found.");
		}
	}
}