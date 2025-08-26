using System.Net;

namespace ETModel
{
	public interface IHttpHandler
	{
		void Handle(HttpListenerContext context);
	}

	public abstract class AHttpHandler : IHttpHandler
	{
		public virtual void Handle(HttpListenerContext context)
		{
		}
		public virtual HttpUtility.HttpResult Ok(string msg = "", object data = null)
		{
			return new HttpUtility.HttpResult
			{
				code = ErrorCode.ERR_Success,
				msg = msg,
				status = true,
				data = data
			};
		}

		public virtual HttpUtility.HttpResult Error(int errorCode = ErrorCode.ERR_HttpError, string msg = "")
		{
			return new HttpUtility.HttpResult
			{
				code = errorCode,
				msg = msg,
				status = false
			};
		}
	}
}