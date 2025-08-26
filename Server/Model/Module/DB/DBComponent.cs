using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using System;
using CSharpx;

namespace ETModel
{
    [ObjectSystem]
    public class DBComponentAwakeSystem : AwakeSystem<DBComponent>
    {
        public override void Awake(DBComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class DBComponentDestroySystem : DestroySystem<DBComponent>
    {
        public override void Destroy(DBComponent self)
        {
            self.Destroy();
        }
    }

    /// <summary>
    /// 用来缓存数据
    /// </summary>
    public class DBComponent : Component
    {
        public MongoClient mongoClient { private set; get; }

        public IMongoDatabase database { private set; get; }
        public GridFSBucket bucket { private set; get; }

        public const int taskCount = 32;
        public List<DBTaskQueue> tasks { private set; get; } = new List<DBTaskQueue>(taskCount);

        public void Awake()
        {
            DBConfig config = StartConfigComponent.Instance.StartConfig.GetComponent<DBConfig>();
            string connectionString = config.ConnectionString;
            var mongoSetting = MongoClientSettings.FromConnectionString(connectionString);
            mongoSetting.ReadPreference = ReadPreference.PrimaryPreferred;
            mongoSetting.WriteConcern = WriteConcern.WMajority;
            mongoSetting.WaitQueueSize = 1000;
            mongoClient = new MongoClient(mongoSetting);
            this.database = this.mongoClient.GetDatabase(config.DBName);
            this.bucket = new GridFSBucket(this.database);

            for (int i = 0; i < taskCount; ++i)
            {
                DBTaskQueue taskQueue = ComponentFactory.Create<DBTaskQueue>();
                this.tasks.Add(taskQueue);
            }

            BuildDBSchema();
        }

        public void Destroy()
        {
        }

        public IMongoCollection<ComponentWithId> GetCollection(string name)
        {
            return this.database.GetCollection<ComponentWithId>(name);
        }

        public bool GetCollectionExists(string name)
        {
            var collectionNames = this.database.ListCollectionNames().ToEnumerable().Select(e => e.ToLower()).ToArray();
            return collectionNames.Contains(name.ToLower());
        }

        public ETTask Add(ComponentWithId component, string collectionName = "")
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = component.GetType().Name;
            }

            DBSaveTask task =
                    ComponentFactory.CreateWithId<DBSaveTask, ComponentWithId, string, ETTaskCompletionSource>(component.Id, component,
                        collectionName, tcs);
            this.tasks[(int)((ulong)task.Id % taskCount)].Add(task);

            return tcs.Task;
        }

        public ETTask AddBatch(List<ComponentWithId> components, string collectionName)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            DBSaveBatchTask task =
                    ComponentFactory.Create<DBSaveBatchTask, List<ComponentWithId>, string, ETTaskCompletionSource>(components, collectionName, tcs);
            this.tasks[(int)((ulong)task.Id % taskCount)].Add(task);
            return tcs.Task;
        }

        public ETTask<ComponentWithId> Get(string collectionName, long id)
        {
            ETTaskCompletionSource<ComponentWithId> tcs = new ETTaskCompletionSource<ComponentWithId>();
            DBQueryTask dbQueryTask =
                    ComponentFactory.CreateWithId<DBQueryTask, string, ETTaskCompletionSource<ComponentWithId>>(id, collectionName, tcs);
            this.tasks[(int)((ulong)id % taskCount)].Add(dbQueryTask);

            return tcs.Task;
        }

        public ETTask<List<ComponentWithId>> GetBatch(string collectionName, List<long> idList)
        {
            ETTaskCompletionSource<List<ComponentWithId>> tcs = new ETTaskCompletionSource<List<ComponentWithId>>();
            DBQueryBatchTask dbQueryBatchTask =
                    ComponentFactory.Create<DBQueryBatchTask, List<long>, string, ETTaskCompletionSource<List<ComponentWithId>>>(idList,
                        collectionName, tcs);
            this.tasks[(int)((ulong)dbQueryBatchTask.Id % taskCount)].Add(dbQueryBatchTask);

            return tcs.Task;
        }

        public ETTask<List<ComponentWithId>> GetJson(string collectionName, string json)
        {
            ETTaskCompletionSource<List<ComponentWithId>> tcs = new ETTaskCompletionSource<List<ComponentWithId>>();

            DBQueryJsonTask dbQueryJsonTask =
                    ComponentFactory.Create<DBQueryJsonTask, string, string, ETTaskCompletionSource<List<ComponentWithId>>>(collectionName, json, tcs);
            this.tasks[(int)((ulong)dbQueryJsonTask.Id % taskCount)].Add(dbQueryJsonTask);

            return tcs.Task;
        }

        public ETTask<List<ComponentWithId>> GetJson(string collectionName, string json, long skip, long limit)
        {
            ETTaskCompletionSource<List<ComponentWithId>> tcs = new ETTaskCompletionSource<List<ComponentWithId>>();
            PipelineStageDefinition<ComponentWithId, ComponentWithId> _match = PipelineStageDefinitionBuilder.Match<ComponentWithId>(json);
            PipelineStageDefinition<ComponentWithId, ComponentWithId> _skip = PipelineStageDefinitionBuilder.Skip<ComponentWithId>((int)skip);
            PipelineStageDefinition<ComponentWithId, ComponentWithId> _limit = PipelineStageDefinitionBuilder.Limit<ComponentWithId>((int)limit);
            PipelineDefinition<ComponentWithId, ComponentWithId> pipeline = new PipelineStagePipelineDefinition<ComponentWithId, ComponentWithId>(
                new PipelineStageDefinition<ComponentWithId, ComponentWithId>[] { _match, _skip, _limit });
            DBQueryPipelineTask dbQueryPipelineTask =
                    ComponentFactory.Create<DBQueryPipelineTask, string, PipelineDefinition<ComponentWithId, ComponentWithId>,
                                ETTaskCompletionSource<List<ComponentWithId>>>(collectionName, pipeline, tcs);
            this.tasks[(int)((ulong)dbQueryPipelineTask.Id % taskCount)].Add(dbQueryPipelineTask);
            return tcs.Task;
        }

        public ETTask<List<ComponentWithId>> GetJsonSort(string collectionName, string json, string sortJson, int skip, int limit)
        {
            ETTaskCompletionSource<List<ComponentWithId>> tcs = new ETTaskCompletionSource<List<ComponentWithId>>();

            DBQueryJsonSortTask dbQueryJsonSortTask =
                    ComponentFactory.Create<DBQueryJsonSortTask, string, ETTaskCompletionSource<List<ComponentWithId>>>(collectionName, tcs);
            dbQueryJsonSortTask.Json = json;
            dbQueryJsonSortTask.SortJson = sortJson;
            dbQueryJsonSortTask.Skip = skip;
            dbQueryJsonSortTask.Limit = limit;
            this.tasks[(int)((ulong)dbQueryJsonSortTask.Id % taskCount)].Add(dbQueryJsonSortTask);

            return tcs.Task;
        }

        public ETTask<List<BsonDocument>> GetJsonCommand(string json)
        {
            ETTaskCompletionSource<List<BsonDocument>> tcs = new ETTaskCompletionSource<List<BsonDocument>>();

            DBJsonCommandTask dBCommandTask =
                ComponentFactory.Create<DBJsonCommandTask, string, ETTaskCompletionSource<List<BsonDocument>>>(json, tcs);
            this.tasks[(int)((ulong)dBCommandTask.Id % taskCount)].Add(dBCommandTask);
            return tcs.Task;
        }

        public ETTask<List<BsonDocument>> GetAggregateCommand(string json)
        {
            ETTaskCompletionSource<List<BsonDocument>> tcs = new ETTaskCompletionSource<List<BsonDocument>>();

            DBAggregateCommandTask dBCommandTask =
                ComponentFactory.Create<DBAggregateCommandTask, string, ETTaskCompletionSource<List<BsonDocument>>>(json, tcs);
            this.tasks[(int)((ulong)dBCommandTask.Id % taskCount)].Add(dBCommandTask);
            return tcs.Task;
        }

        public ETTask<long> GetCountByJson(string collectionName, string json)
        {
            ETTaskCompletionSource<long> tcs = new ETTaskCompletionSource<long>();
            DBQueryCountTask dbQueryCountTask =
                    ComponentFactory.Create<DBQueryCountTask, string, string, ETTaskCompletionSource<long>>(collectionName, json, tcs);
            this.tasks[(int)((ulong)dbQueryCountTask.Id % taskCount)].Add(dbQueryCountTask);
            return tcs.Task;
        }

        public ETTask DeleteJson(string collectionName, string json)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            DBDeleteJsonTask dbDeleteJsonTask =
                    ComponentFactory.Create<DBDeleteJsonTask, string, string, ETTaskCompletionSource>(collectionName, json, tcs);
            this.tasks[(int)((ulong)dbDeleteJsonTask.Id % taskCount)].Add(dbDeleteJsonTask);
            return tcs.Task;
        }

        public ETTask<ObjectId> UploadFile(string fileName, byte[] source, BsonDocument meta)
        {
            ETTaskCompletionSource<ObjectId> tcs = new ETTaskCompletionSource<ObjectId>();
            DBUploadFileTask dBUploadFileTask =
                ComponentFactory.Create<DBUploadFileTask, ETTaskCompletionSource<ObjectId>>(tcs);
            dBUploadFileTask.FileName = fileName;
            dBUploadFileTask.Source = source;
            dBUploadFileTask.Meta = meta;
            this.tasks[(int)((ulong)dBUploadFileTask.Id % taskCount)].Add(dBUploadFileTask);
            return tcs.Task;
        }

        public ETTask<byte[]> DownloadFile(ObjectId objId)
        {
            ETTaskCompletionSource<byte[]> tcs = new ETTaskCompletionSource<byte[]>();
            DBDownloadFileTask dBDownloadFileTask = 
                ComponentFactory.Create<DBDownloadFileTask, ObjectId, ETTaskCompletionSource<byte[]>>(objId, tcs);
            this.tasks[(int)((ulong)dBDownloadFileTask.Id % taskCount)].Add(dBDownloadFileTask);
            return tcs.Task;
        }

        /// <summary>
        /// 建立資料庫結構
        /// </summary>
        private void BuildDBSchema()
        {
            var collectionNames = this.database.ListCollectionNames().ToEnumerable().Select(e => e.ToLower()).ToArray();
            foreach (var pair in DBHelper.GetDBSchemaIndicesIter())
            {
                DBSchemaAttribute dbArre = pair.Key.GetCustomAttribute<DBSchemaAttribute>();
                if (dbArre == null)
                {
                    continue;
                }

                // TODO:表的名稱要統一，這邊先用開頭大寫的規則
                var collectionName = pair.Key.Name;
                if (!collectionNames.Contains(collectionName.ToLower()))
                {
                    // 建立表
                    this.database.CreateCollection(collectionName);
                }
                else
                {
                    if (!dbArre.isNeedToAlter)
                    {
                        continue;
                    }
                }

                var collection = database.GetCollection<ComponentWithId>(collectionName);
                var oldIndexs = collection.Indexes.List().ToList();
                //排除ID索引
                oldIndexs.RemoveAt(0);
                var indexInfoDict = oldIndexs.Aggregate(new Dictionary<BsonDocument, string>(), (dic, list) =>
                  {
                      dic[list["key"].AsBsonDocument] = list["name"].AsString;
                      return dic;
                  });
                var oldIndexsBsonDocument = oldIndexs.Select(e => e["key"].AsBsonDocument).ToList();
                // 取得索引
                foreach (var index in pair.Value)
                {
                    var options = new CreateIndexOptions() { Unique = index.Value.isUnique };
                    var indexDoc = new BsonDocument();
                    foreach (var column in index.Value.dBIndexOrders)
                    {
                        indexDoc[column.columnName] = (int)column.order;
                    }

                    if (!indexInfoDict.TryGetValue(indexDoc, out var existingIndexName))
                    {
                        IndexKeysDefinition<ComponentWithId> keyCode = indexDoc;
                        var codeIndexModel = new CreateIndexModel<ComponentWithId>(keyCode, options);

                        if (existingIndexName != null)
                        {
                            collection.Indexes.DropOne(existingIndexName);
                        }

                        var newIndexName = collection.Indexes.CreateOne(codeIndexModel);
                        indexInfoDict[indexDoc] = newIndexName;
                    }
                    oldIndexsBsonDocument.Remove(indexDoc);
                }

                foreach (var oldIndexBsonDoc in oldIndexsBsonDocument)
                {
                    if (indexInfoDict.TryGetValue(oldIndexBsonDoc, out var indexName))
                    {
                        collection.Indexes.DropOne(indexName);
                    }
                }
            }
        }

        /// <summary>
        /// 該插入有處理批次的問題
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async ETTask RunInsertCommandAsync<T>(List<BsonDocument> doc, int batchSize = 10000)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                var array = new BsonArray(batchSize);
                var tasks = new List<Task<BsonDocument>>();
                for (int i = 0; i < doc.Count; i++)
                {
                    array.Add(doc[i].ToBsonDocument());
                    if ((i + 1) % batchSize == 0 || i + 1 == doc.Count)
                    {
                        var command = new BsonDocument
                    {
                        { "insert", typeof (T).Name },
                        { "documents", array },
                        { "ordered", false },
                        { "writeConcern", new BsonDocument { { "w", "majority" }, { "wtimeout", 5000 } } }
                    };
                        var result = await database.RunCommandAsync<BsonDocument>(session, command);
                        Console.WriteLine($"{DateTime.Now} collection:{typeof(T).Name}:{result.ToJson()}");
                        array.Clear();
                    }
                }

            }
        }

        /// <summary>
        /// 該插入有處理批次的問題
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async ETTask RunUpdateCommandAsync<T>(List<BsonDocument> doc, int batchSize = 10000)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                var array = new BsonArray(batchSize);
                for (int i = 0; i < doc.Count; i++)
                {
                    array.Add(doc[i].ToBsonDocument());
                    if ((i + 1) % batchSize == 0 || i + 1 == doc.Count)
                    {
                        var command = new BsonDocument
                        {
                            { "update", typeof(T).Name },
                            { "updates", new BsonArray(doc) },
                            { "ordered", true },
                            { "writeConcern", new BsonDocument
                                    {
                                        { "w", "majority" },
                                        { "wtimeout", 5000 }
                                    }
                            }
                        };
                        var result = await database.RunCommandAsync<BsonDocument>(session, command);
                        Console.WriteLine($"{DateTime.Now} collection:{typeof(T).Name}:{result.ToJson()}");
                        array.Clear();
                    }
                }
            }

        }

        /// <summary>
        /// 該查詢有處理批次的問題
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<BsonArray> RunFindCommandAsync(BsonDocument command, int batchSize = 1000)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                var result = await database.RunCommandAsync<BsonDocument>(session, command);
                BsonArray data = result["cursor"]["firstBatch"].AsBsonArray;
                return await FindNextBatch(data, session, result["cursor"]["id"].AsInt64, command["find"].AsString, batchSize);
            }
        }

        BsonDocument command = new BsonDocument();

        private async Task<BsonArray> FindNextBatch(BsonArray data, IClientSessionHandle session, long cursorId, string findCollection,
        int batchSize = 1000)
        {
            command.Clear();
            if (cursorId == 0L)
            {
                return data;
            }

            command.Add("getMore", cursorId);
            command.Add("collection", findCollection);
            command.Add("batchSize", batchSize);
            BsonDocument result = await database.RunCommandAsync<BsonDocument>(session, command);
            BsonArray _data = result["cursor"]["nextBatch"].AsBsonArray;
            data.AddRange(_data);
            return await FindNextBatch(data, session, result["cursor"]["id"].AsInt64, findCollection, batchSize);
        }

        /// <summary>
        /// 該查詢有處理批次的問題
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<List<BsonDocument>> RunAggregateCommandAsync(BsonDocument command, int batchSize = 1000)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                var result = await database.RunCommandAsync<BsonDocument>(session, command);
                BsonArray data = (result["cursor"]["firstBatch"].AsBsonArray);
                return (await AggregateNextBatch(data, session, result["cursor"]["id"].AsInt64, command["aggregate"].AsString, batchSize)).ToBsonDocumentList();
            }
        }
        /// <summary>
        /// 查詢有處理批次的問題 (要合併到其他collection的情形)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public async Task<BsonArray> RunAggregateCommandAsync_ToBsonArray(BsonDocument command, int batchSize = 1000)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                var result = await database.RunCommandAsync<BsonDocument>(session, command);
                return result["cursor"]["firstBatch"].AsBsonArray;
            }
        }

        private async Task<BsonArray> AggregateNextBatch(BsonArray data, IClientSessionHandle session, long cursorId, string findCollection,
        int batchSize = 1000)
        {
            if (cursorId == 0L)
            {
                return data;
            }

            BsonDocument command = new BsonDocument { { "getMore", cursorId }, { "collection", findCollection }, { "batchSize", batchSize }, };
            BsonDocument result = await database.RunCommandAsync<BsonDocument>(session, command);
            BsonArray _data = (result["cursor"]["nextBatch"].AsBsonArray);
            data.AddRange(_data);
            return await FindNextBatch(data, session, result["cursor"]["id"].AsInt64, findCollection, batchSize);
        }

        /// <summary>
        /// 查詢筆數
        /// </summary>
        /// <typeparam name="T">collectionName</typeparam>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        public async ETTask<int> RunCountCommandAsync<T>(BsonDocument filter)
        {

            var command = new BsonDocument
                {
                    { "aggregate",  typeof(T).Name },
                    {  "pipeline", new BsonArray
                        {
                        new BsonDocument( "$match", filter),
                        new BsonDocument( "$count", "total")
                        }
                    },
                    { "cursor",  new BsonDocument() },
                };
            var result = await database.RunCommandAsync<BsonDocument>(command);
            var bson = result["cursor"]["firstBatch"].AsBsonArray.FirstOrDefault()?.AsBsonDocument;
            return bson?["total"]?.AsInt32 ?? 0;
        }

        public async Task<ObjectId> UploadFromBytesAsync(string fileName, byte[] source, BsonDocument meta)
        {
            return await bucket.UploadFromBytesAsync(fileName, source, new GridFSUploadOptions
            {
                Metadata = meta // 額外資訊
            });
        }

        public async Task<byte[]> DownloadAsBytesAsync(ObjectId id)
        {
            return await bucket.DownloadAsBytesAsync(id);
        }

        public async Task<byte[]> DownloadAsBytesByNameAsync(string fileName, int version)
        {
            return await bucket.DownloadAsBytesByNameAsync(fileName, new GridFSDownloadByNameOptions
            {
                Revision = version // 同名文件第幾個版本
            });
        }

        public async Task<List<GridFSFileInfo>> FindFilesAsync(string fileName, int skip, int limit, int batchSize = 1000)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(e => e.Filename, fileName);
            var sort = Builders<GridFSFileInfo>.Sort.Descending(e => e.UploadDateTime);
            var options = new GridFSFindOptions { Skip = skip, Limit = limit, BatchSize = batchSize, Sort = sort };

            var fileInfos = new List<GridFSFileInfo>();
            using (var cursor = await bucket.FindAsync(filter, options))
            {
                fileInfos = cursor.ToList();
            }

            return fileInfos;
        }

        public async Task<GridFSFileInfo> FindFileAsync(string fileName)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(e => e.Filename, fileName);
            var sort = Builders<GridFSFileInfo>.Sort.Descending(e => e.UploadDateTime);
            var options = new GridFSFindOptions { Limit = 1, Sort = sort };

            GridFSFileInfo fileInfo;
            using (var cursor = await bucket.FindAsync(filter, options))
            {
                fileInfo = cursor.ToList().FirstOrDefault();
            }

            return fileInfo;
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await bucket.DeleteAsync(id);
        }
    }
}