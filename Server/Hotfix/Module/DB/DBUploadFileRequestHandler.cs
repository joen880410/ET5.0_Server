using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBUploadFileRequestHandler : AMRpcHandler<DBUploadFileRequest, DBUploadFileResponse>
	{
		protected override void Run(Session session, DBUploadFileRequest message, Action<DBUploadFileResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		protected async ETVoid RunAsync(Session session, DBUploadFileRequest message, Action<DBUploadFileResponse> reply)
		{
			DBUploadFileResponse response = new DBUploadFileResponse();
			try
			{
				response.Id = await Game.Scene.GetComponent<DBComponent>().UploadFile(message.FileName, message.Source, message.Meta);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}
