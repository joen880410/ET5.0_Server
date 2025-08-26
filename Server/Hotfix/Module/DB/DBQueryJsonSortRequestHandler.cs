using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.DB)]
    public class DBQueryJsonSortRequestHandler : AMRpcHandler<DBQueryJsonSortRequest, DBQueryJsonSortResponse>
	{
		protected override void Run(Session session, DBQueryJsonSortRequest message, Action<DBQueryJsonSortResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		protected async ETVoid RunAsync(Session session, DBQueryJsonSortRequest message, Action<DBQueryJsonSortResponse> reply)
		{
			DBQueryJsonSortResponse response = new DBQueryJsonSortResponse();
			try
			{
				List<ComponentWithId> components = await Game.Scene.GetComponent<DBComponent>().GetJsonSort(message.CollectionName, message.Json, message.SortJson, message.Skip, message.Limit);
				response.Components = components;

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}
