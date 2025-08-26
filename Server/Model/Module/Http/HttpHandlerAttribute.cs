using System;

namespace ETModel
{
    public class HttpHandlerAttribute : BaseAttribute
    {
        public AppType AppType { get; }

        public string Path { get; }

        public HttpHandlerAttribute(AppType appType, string path)
        {
            this.AppType = appType;
            this.Path = path;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class GetAttribute : Attribute
    {
        public ApiType apiType { get; }
        public GetAttribute(ApiType type)
        {
            apiType = type;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PostAttribute : Attribute
    {
        public ApiType apiType { get; }
        public bool IsAuthorized { get; }

        public PostAttribute(ApiType type, bool isAuth = true)
        {
            this.IsAuthorized = isAuth;
            this.apiType = type;
        }

    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PutAttribute : Attribute
    {
        public ApiType apiType { get; }
        public bool IsAuthorized { get; }

        public PutAttribute(ApiType type, bool isAuth = true)
        {
            this.IsAuthorized = isAuth;
            this.apiType = type;
        }

    }
}