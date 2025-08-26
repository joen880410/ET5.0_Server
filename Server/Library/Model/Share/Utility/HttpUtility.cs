namespace ETModel
{
    public static class HttpUtility
    {
        #region Gallery
        public class UploadImageResult
        {
            public string fileName;

            public int errorCode;

            public string paramsName;

            public long imageId;
        }
        #endregion
        #region Config
        public class UploadConfigResult
        {
            public string fileName;

            public int errorCode;

            public string Context;
        }
        #endregion

        public class HttpResult
        {
            public int code;
            public bool status;
            public string msg = "";
            [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
            public object data;
        }
    }
}