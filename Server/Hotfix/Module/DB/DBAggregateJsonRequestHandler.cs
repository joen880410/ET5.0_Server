using System;
using System.Collections.Generic;
using ETModel;
using MongoDB.Bson;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBAggregateJsonRequestHandler : AMRpcHandler<DBAggregateJsonRequest, DBAggregateJsonResponse>
	{
		protected override void Run(Session session, DBAggregateJsonRequest message, Action<DBAggregateJsonResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		protected async ETVoid RunAsync(Session session, DBAggregateJsonRequest message, Action<DBAggregateJsonResponse> reply)
		{
            DBAggregateJsonResponse response = new DBAggregateJsonResponse();
			try
			{
				List<BsonDocument> bsons = await Game.Scene.GetComponent<DBComponent>().GetAggregateCommand(message.Json);
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
