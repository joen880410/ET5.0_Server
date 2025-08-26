using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.DB)]
	public class DBQueryCountCommandRequestHandler : AMRpcHandler<DBQueryCountCommandRequest, DBQueryCountCommandResponse>
	{
		protected override void Run(Session session, DBQueryCountCommandRequest message, Action<DBQueryCountCommandResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, DBQueryCountCommandRequest message, Action<DBQueryCountCommandResponse> reply)
		{
			DBQueryCountCommandResponse response = new DBQueryCountCommandResponse();
			try
			{
				await ETTask.CompletedTask;
				//long count = await Game.Scene.GetComponent<DBComponent>().GetCountByCommand(message.CommandJson);
				//response.Count = count;

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}