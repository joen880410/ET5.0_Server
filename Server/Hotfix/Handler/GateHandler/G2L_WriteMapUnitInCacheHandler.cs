using System;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class G2L_WriteMapUnitInCacheHandler : AMRpcHandler<L2G_WriteMapUnitInCache, G2L_WriteMapUnitInCache>
    {
        protected override void Run(Session session, L2G_WriteMapUnitInCache message, Action<G2L_WriteMapUnitInCache> reply)
        {
            RunAsync(session, message, reply).Coroutine();
        }

        protected async ETTask RunAsync(Session session, L2G_WriteMapUnitInCache message, Action<G2L_WriteMapUnitInCache> reply)
        {
            G2L_WriteMapUnitInCache response = new G2L_WriteMapUnitInCache();
            try
            {
                var player = BsonSerializer.Deserialize<Player>(message.Json);
                CacheExHelper.WriteInCache(player, out player);
                await ETTask.CompletedTask;
                reply(response);

            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}