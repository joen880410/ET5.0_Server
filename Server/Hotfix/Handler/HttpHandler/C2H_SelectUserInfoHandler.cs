using System.Net;
using ETModel;
using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace ETHotfix
{
    [HttpHandler(AppType.DB, "/select/info")]
    public class C2H_SelectUserInfoHandler : AHttpHandler
    {
        [Get(ApiType.Jmarket)]
        public async ETTask<HttpUtility.HttpResult> SelectUserInfoHandle(HttpListenerRequest req, string uidStr)
        {
            await ETTask.CompletedTask;
            try
            {
                var response = new DBQueryJsonResponse();
                var dbComponent = Game.Scene.GetComponent<DBComponent>();
                if (!long.TryParse(uidStr, out var uid))
                {
                    return Error(msg: $"Message: uidStr ParseError");
                }

                var match = new BsonDocument
                {
                    { "$match", new BsonDocument
                        {
                            {
                                "_id", new BsonDocument
                                {
                                    { "$eq", uid }
                                }
                            }
                        }
                    }
                };

                var otherMatch = new BsonDocument
                {
                    { "$match", new BsonDocument
                        {
                            {
                                "uid", new BsonDocument
                                {
                                    { "$eq", uid }
                                }
                            }
                        }
                    }
                };

                #region 設定 pipeline (group / project)
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
                            { "sex", new BsonDocument
                                {
                                    { "$first", "$sex" }
                                }
                            },
                            { "location", new BsonDocument
                                {
                                    { "$first", "$location" }
                                }
                            },
                            { "height", new BsonDocument
                                {
                                    { "$first", "$height" }
                                }
                            },
                            { "weight", new BsonDocument
                                {
                                    { "$first", "$weight" }
                                }
                            },
                            { "birthday", new BsonDocument
                                {
                                    { "$first", "$birthday" }
                                }
                            },
                            { "createAt", new BsonDocument
                                {
                                    { "$first", "$createAt" }
                                }
                            },
                            { "lastOnlineAt", new BsonDocument
                                {
                                    { "$first", "$lastOnlineAt" }
                                }
                            },
                            { "coin", new BsonDocument
                                {
                                    { "$first", "$coin" }
                                }
                            },
                            { "playerRideTotalInfo", new BsonDocument
                                {
                                    { "$first", "$playerRideTotalInfo" }
                                }
                            },
                            { "playerRunTotalInfo", new BsonDocument
                                {
                                    { "$first", "$playerRunTotalInfo" }
                                }
                            },
                            { "language", new BsonDocument
                                {
                                    { "$first", "$language" }
                                }
                            },
                            { "userLeaderCapacity", new BsonDocument
                                {
                                    { "$first", "$userLeaderCapacity" }
                                }
                            },
                            { "userTaskCapacity", new BsonDocument
                                {
                                   { "$first", "$userTaskCapacity" }
                                }
                            }
                        }
                    }
                };
                var charProject = new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "_id", 0 },
                            { "type", 1 },
                            { "charaType", 1 },
                            { "modelSkinId", 1 },
                            { "bicycleSkinId", 1 },
                            { "halmetSkinId", 1 },
                            { "effectSkinId", 1 },
                            { "decorationSkinId", 1 },
                            { "clothSkinId", 1 },
                            { "pantsSkinId", 1 },
                            { "shoesSkinId", 1 },
                            { "petSkinId", 1 },
                            { "suitSkinId", 1 }
                        }
                    }
                };
                var equipProject = new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "_id", 0 },
                            { "configType", 1 },
                            { "configId", 1 },
                            { "count", 1 }
                        }
                    }
                };
                var sceneProject = new BsonDocument
                {
                    { "$project", new BsonDocument
                        {
                            { "_id", 0 },
                            { "sceneId", 1 },
                            { "sceneType", 1 }
                        }
                    }
                };
                #endregion

                #region 設定 command
                var userCommand = new BsonDocument
                {
                    {"aggregate", nameof(User) },
                    {"pipeline",new BsonArray{ match, group}},
                    {"cursor", new BsonDocument{ { "batchSize", 1000 } } },
                };
                #endregion

                var result = await dbComponent.RunAggregateCommandAsync(userCommand);
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
