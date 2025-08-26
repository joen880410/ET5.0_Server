using System.Net;
using ETModel;
using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace ETHotfix
{
    [HttpHandler(AppType.DB, "/select/coin")]
    public class C2H_SelectUserCoinHandler : AHttpHandler
    {
        [Post(ApiType.Jmarket)]
        public async ETTask<HttpUtility.HttpResult> SelectUserCoinHandle(HttpListenerRequest req)
        {
            await ETTask.CompletedTask;
            try
            {
                var response = new DBQueryJsonResponse();
                var dbComponent = Game.Scene.GetComponent<DBComponent>();
                var match = new BsonDocument
                {
                    { "$match", new BsonDocument
                        {
                            {
                                "coin", new BsonDocument
                                {
                                    { "$gte", 0 }
                                }
                            }
                        }
                    }
                };

                var group = new BsonDocument
                {
                    { "$group", new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "name", new BsonDocument
                                {
                                    { "$first", "$name" }
                                }
                            },
                            { "coin", new BsonDocument
                                {
                                    { "$first", "$coin" }
                                }
                            },
                        }
                    }
                };

                var command = new BsonDocument
                {
                    {"aggregate", nameof(User) },
                    {"pipeline",new BsonArray{ match, group }},
                    {"cursor", new BsonDocument{ { "batchSize", 1000 } } },
                };
                var result = await dbComponent.RunAggregateCommandAsync(command);
                return Ok(msg: result.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.RelaxedExtendedJson }));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return Error(msg: $"Message: {e.Message}\r\nStackTrace: {e.StackTrace}");
            }
        }

    }
}
