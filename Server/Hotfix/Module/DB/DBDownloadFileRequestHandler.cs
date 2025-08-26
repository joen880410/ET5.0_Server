using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBDownloadFileRequestHandler : AMRpcHandler<DBDownloadFileRequest, DBDownloadFileResponse>
	{
		protected override void Run(Session session, DBDownloadFileRequest message, Action<DBDownloadFileResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		protected async ETVoid RunAsync(Session session, DBDownloadFileRequest message, Action<DBDownloadFileResponse> reply)
		{
			DBDownloadFileResponse response = new DBDownloadFileResponse();
			try
			{
				response.Source = await Game.Scene.GetComponent<DBComponent>().DownloadFile(message.Id);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}
