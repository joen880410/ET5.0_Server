using System;
using System.Collections.Generic;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBQueryCommandRequestHandler : AMRpcHandler<DBQueryCommandRequest, DBQueryCommandResponse>
	{
		protected override void Run(Session session, DBQueryCommandRequest message, Action<DBQueryCommandResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		protected async ETVoid RunAsync(Session session, DBQueryCommandRequest message, Action<DBQueryCommandResponse> reply)
		{
            DBQueryCommandResponse response = new DBQueryCommandResponse();
			try
			{
				List<BsonDocument> bsons = await Game.Scene.GetComponent<DBComponent>().GetJsonCommand(message.Json);
				response.BsonDocuments = bsons;

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}
