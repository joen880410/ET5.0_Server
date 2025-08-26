using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ETHotfix
{
    [ObjectSystem]
    public class DbProxyComponentSystem : AwakeSystem<DBProxyComponent>
    {
        public override void Awake(DBProxyComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 用来与数据库操作代理
    /// </summary>
    public static class DBProxyComponentEx
    {
        private readonly static BsonDocument Doc = new BsonDocument();
        public static void Awake(this DBProxyComponent self)
        {
            StartConfig dbStartConfig = StartConfigComponent.Instance.DBConfig;
            self.dbAddress = dbStartConfig.GetComponent<InnerConfig>().IPEndPoint;
        }

        public static async ETTask Save(this DBProxyComponent self, ComponentWithId component)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            await session.Call(new DBSaveRequest { Component = component });
        }

        public static async ETTask SaveBatch(this DBProxyComponent self, List<ComponentWithId> components)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            await session.Call(new DBSaveBatchRequest { Components = components });
        }

        public static async ETTask Save(this DBProxyComponent self, ComponentWithId component, ETCancellationTokenSource cts)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            await session.Call(new DBSaveRequest { Component = component }, cts);
        }

        public static async ETTask SaveLog(this DBProxyComponent self, ComponentWithId component)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            await session.Call(new DBSaveRequest { Component = component, CollectionName = "DBLog" });
        }

        public static async ETTask SaveRecordLog(this DBProxyComponent self, ComponentWithId component)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            await session.Call(new DBSaveRequest { Component = component, CollectionName = "RecordLog" });
        }

        public static async ETTask SaveLog(this DBProxyComponent self, long uid, DBLog.LogType logType, ComponentWithId record)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)logType;
            BsonDocument doc = null;
            BsonDocument.TryParse(record.ToJson(), out doc);
            dBLog.document = doc;
            dBLog.createAt = TimeHelper.NowTimeMillisecond();
            await self.SaveLog(dBLog);
            dBLog.Dispose();
        }

        public static async ETTask SaveLog(this DBProxyComponent self, long uid, DBLog.LogType logType, BsonDocument record)
        {
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)logType;
            dBLog.document = record;
            dBLog.createAt = TimeHelper.NowTimeMillisecond();
            await self.SaveLog(dBLog);
            dBLog.Dispose();
        }

        public static async ETTask SaveRecordLog(this DBProxyComponent self, long uid, RecordLog.LogType logType, ComponentWithId record)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            RecordLog recordLog = ComponentFactory.CreateWithId<RecordLog>(IdGenerater.GenerateId());
            recordLog.uid = uid;
            recordLog.logType = (int)logType;
            BsonDocument doc = null;
            BsonDocument.TryParse(record.ToJson(), out doc);
            recordLog.document = doc;
            recordLog.createAt = TimeHelper.NowTimeMillisecond();
            await self.SaveRecordLog(recordLog);
            recordLog.Dispose();
        }

        public static async ETTask SaveRecordLog(this DBProxyComponent self, long uid, RecordLog.LogType logType, BsonDocument record)
        {
            RecordLog recordLog = ComponentFactory.CreateWithId<RecordLog>(IdGenerater.GenerateId());
            recordLog.uid = uid;
            recordLog.logType = (int)logType;
            recordLog.document = record;
            recordLog.createAt = TimeHelper.NowTimeMillisecond();
            await self.SaveRecordLog(recordLog);
            recordLog.Dispose();
        }

        public static async ETTask SaveLogBatch(this DBProxyComponent self, long uid, DBLog.LogType logType, List<ComponentWithId> components)
        {
            var list = self.CreateLogs(uid, logType, components);
            await self.SaveBatch(list);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Dispose();
            }
        }
        public static List<ComponentWithId> CreateLogs(this DBProxyComponent self, long uid, DBLog.LogType logType, List<ComponentWithId> components)
        {
            List<ComponentWithId> list = new List<ComponentWithId>();
            for (int i = 0; i < components.Count; i++)
            {
                DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
                dBLog.uid = uid;
                dBLog.logType = (int)logType;
                BsonDocument doc = null;
                BsonDocument.TryParse(components[i].ToJson(), out doc);
                dBLog.document = doc;
                dBLog.createAt = TimeHelper.NowTimeMillisecond();
                list.Add(dBLog);
            }
            return list;
        }
        public static ComponentWithId CreateLog(this DBProxyComponent self, long uid, DBLog.LogType logType, List<ComponentWithId> components)
        {
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)logType;
            dBLog.document = new BsonDocument();
            for (int i = 0; i < components.Count; i++)
            {
                dBLog.document.AddRange(components[i].ToBsonDocument());
            }
            dBLog.createAt = TimeHelper.NowTimeMillisecond();

            return dBLog;
        }
        public static List<ComponentWithId> CreateLog(this DBProxyComponent self, long uid, DBLog.LogType logType, BsonDocument component)
        {
            List<ComponentWithId> list = new List<ComponentWithId>();
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)logType;
            dBLog.document = component;
            dBLog.createAt = TimeHelper.NowTimeMillisecond();
            list.Add(dBLog);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="uid">DBLog的玩家uid</param>
        /// <param name="logType">Log的類型</param>
        /// <param name="logDataName">在DBLog中 document的欄位名稱</param>
        /// <param name="records">Log資料</param>
        /// <returns></returns>
        public static async ETTask SaveLogBatch(this DBProxyComponent self, long uid, DBLog.LogType logType, string logDataName, List<BsonDocument> records)
        {
            DBLog dBLog = ComponentFactory.CreateWithId<DBLog>(IdGenerater.GenerateId());
            dBLog.uid = uid;
            dBLog.logType = (int)logType;
            dBLog.document = new BsonDocument()
            {
                [logDataName] = new BsonArray().AddRange(records)
            };
            dBLog.createAt = TimeHelper.NowTimeMillisecond();
            await self.Save(dBLog);

        }
        public static async ETTask<T> Query<T>(this DBProxyComponent self, long id) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryResponse dbQueryResponse = (DBQueryResponse)await session.Call(new DBQueryRequest { CollectionName = typeof(T).Name, Id = id });
            return (T)dbQueryResponse.Component;
        }

        /// <summary>
        /// 根据查询表达式查询
        /// </summary>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            return await self.Query<T>(json);
        }

        /// <summary>
		/// 根据查询表达式查询，並降序排列
		/// </summary>
        public static async ETTask<List<ComponentWithId>> QueryDescendingSort<T>
        (
            this DBProxyComponent self,
            Expression<Func<T, bool>> exp,
            Expression<Func<T, object>> expSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            SortDefinition<T> sort = Builders<T>.Sort.Descending(expSort);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            string sortJson = sort.Render(renderArgs).ToJson();
            return await self.QuerySort<T>(json, sortJson, skip, limit);
        }
        public static async ETTask<List<ComponentWithId>> QueryDescendingSort<T>
        (
            this DBProxyComponent self,
            List<Expression<Func<T, bool>>> ListExp,
            List<Expression<Func<T, object>>> listExpSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);

            Doc.Clear();
            for (int i = 0; i < ListExp.Count; i++)
            {
                var exp = ListExp[i];
                ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
                Doc.AddRange(filter.Render(renderArgs));
            }
            string sortJson = Doc.ToJson();

            Doc.Clear();
            for (int i = 0; i < listExpSort.Count; i++)
            {
                SortDefinition<T> sort = Builders<T>.Sort.Descending(listExpSort[i]);
                Doc.AddRange(sort.Render(renderArgs));
            }
            string json = Doc.ToJson();

            return await self.QuerySort<T>(json, sortJson, skip, limit);
        }
        public static async ETTask<List<ComponentWithId>> QueryDescendingSort<T>
        (
            this DBProxyComponent self,
            Expression<Func<T, bool>> exp,
            List<Expression<Func<T, object>>> listExpSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            BsonDocument doc = new BsonDocument();
            for (int i = 0; i < listExpSort.Count; i++)
            {
                SortDefinition<T> sort = Builders<T>.Sort.Descending(listExpSort[i]);
                doc.AddRange(sort.Render(renderArgs));
            }
            return await self.QuerySort<T>(json, doc.ToJson(), skip, limit);
        }

        /// <summary>
        /// 根据查询表达式查询，並升序排列
        /// </summary>
        public static async ETTask<List<ComponentWithId>> QueryAscendingSort<T>
        (
            this DBProxyComponent self,
            Expression<Func<T, bool>> exp,
            Expression<Func<T, object>> expSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            SortDefinition<T> sort = Builders<T>.Sort.Ascending(expSort);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            string sortJson = sort.Render(renderArgs).ToJson();
            return await self.QuerySort<T>(json, sortJson, skip, limit);
        }
        public static async ETTask<List<ComponentWithId>> QueryAscendingSort<T>
        (
            this DBProxyComponent self,
            Expression<Func<T, bool>> exp,
            List<Expression<Func<T, object>>> listExpSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            BsonDocument doc = new BsonDocument();
            for (int i = 0; i < listExpSort.Count; i++)
            {
                SortDefinition<T> sort = Builders<T>.Sort.Descending(listExpSort[i]);
                doc.AddRange(sort.Render(renderArgs));
            }
            return await self.QuerySort<T>(json, doc.ToJson(), skip, limit);
        }

        public static async ETTask<List<ComponentWithId>> QueryAscendingSort<T>
        (
            this DBProxyComponent self,
            List<Expression<Func<T, bool>>> expList,
            Expression<Func<T, object>> expSort,
            int skip = 0,
            int limit = 10
        ) where T : ComponentWithId
        {
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            Doc.Clear();
            for (int i = 0; i < expList.Count; i++)
            {
                var exp = expList[i];
                ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
                Doc.AddRange(filter.Render(renderArgs));
            }

            SortDefinition<T> sort = Builders<T>.Sort.Ascending(expSort);
            string sortJson = sort.Render(renderArgs).ToJson();
            return await self.QuerySort<T>(Doc.ToJson(), sortJson, skip, limit);
        }

        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, List<long> ids) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryBatchResponse dbQueryBatchResponse = (DBQueryBatchResponse)await session.Call(new DBQueryBatchRequest { CollectionName = typeof(T).Name, IdList = ids });
            return dbQueryBatchResponse.Components;
        }

        /// <summary>
        /// 根据json查询条件查询
        /// </summary>
        /// <param name="self"></param>
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, string json) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryJsonResponse dbQueryJsonResponse = (DBQueryJsonResponse)await session.Call(new DBQueryJsonRequest { CollectionName = typeof(T).Name, Json = json });
            return dbQueryJsonResponse.Components;
        }

        public static async ETTask<List<ComponentWithId>> QuerySort<T>(this DBProxyComponent self, string json, string sortJson, int skip, int limit) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryJsonSortResponse dbQueryJsonResponse = (DBQueryJsonSortResponse)await session.Call(new DBQueryJsonSortRequest { CollectionName = typeof(T).Name, Json = json, SortJson = sortJson, Skip = skip, Limit = limit });
            return dbQueryJsonResponse.Components;
        }

        /// <summary>
        /// 根據查詢表達式查詢，並約束結果範圍，再取回相應筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp, long skip, long limit) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            return await self.Query<T>(json, skip, limit);
        }

        /// <summary>
        /// 根據查詢表達式查詢，並約束結果範圍，再取回相應筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <param name="limit"></param>
        /// <returns></returns>

        public static async ETTask<List<T>> Query<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp, List<Expression<Func<T, object>>> projections, long skip = 0, long limit = 0) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            var filterDoc = filter.Render(renderArgs);
            Doc.Clear();
            for (int i = 0; i < projections.Count; i++)
            {
                Doc.AddRange(Builders<T>.Projection.Include(projections[i]).Render(renderArgs));
            }
            return (await self.Query<T>(filterDoc, Doc, (int)skip, (int)limit)).Select(e => BsonSerializer.Deserialize<T>(e)).ToList();
        }

        public static async ETTask<List<T>> Query<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp, Expression<Func<T, object>> projection, long skip = 0, long limit = 0) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            var filterDoc = filter.Render(renderArgs);
            Doc.Clear();
            Doc.AddRange(Builders<T>.Projection.Include(projection).Render(renderArgs).ToBsonDocument());
            return (await self.Query<T>(filterDoc, Doc, (int)skip, (int)limit)).Select(e => BsonSerializer.Deserialize<T>(e)).ToList();
        }

        /// <summary>
        /// 根據json查詢條件查詢，並約束結果範圍，再取回相應筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="json"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, string json, long skip, long limit) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryJsonResponse dbQueryJsonResponse = (DBQueryJsonResponse)await session.Call(new DBQueryJsonSkipLimitRequest { CollectionName = typeof(T).Name, Json = json, Skip = skip, Limit = limit });
            return dbQueryJsonResponse.Components;
        }

        public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, List<Expression<Func<T, bool>>> exps) where T : ComponentWithId
        {
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            Doc.Clear();
            for (int i = 0; i < exps.Count; i++)
            {
                var exp = exps[i];
                ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
                Doc.AddRange(filter.Render(renderArgs));
            }
            return await self.Query<T>(Doc.ToJson());
        }

        /// <summary>
        /// 根據json查詢條件查詢View，並約束結果範圍，再取回相應筆數
        /// </summary>
        public static async ETTask<List<BsonDocument>> QueryCommand
        (
            this DBProxyComponent self,
            string collectionName,
            BsonDocument filterBson,
            int skip = 0,
            int limit = 10,
            int batchSize = 1000
        )
        {
            var command = new BsonDocument
            {
                { "find", collectionName },
                { "filter", filterBson },
                { "skip", skip },
                { "limit", limit },
                { "batchSize", batchSize }
            };
            var json = command.ToJson();
            return await self.QueryCommand(json);
        }

        /// <summary>
        /// 根據json查詢條件查詢，並約束結果範圍，再取回相應筆數
        /// </summary>
        public static async ETTask<List<BsonDocument>> Query<T>
        (
            this DBProxyComponent self,
            BsonDocument filterBson,
            BsonDocument projectionBson,
            int skip = 0,
            int limit = 0,
            int batchSize = 1000
        )
        {
            var command = new BsonDocument
            {
                { "find", typeof(T).Name },
                { "filter", filterBson },
                { "projection", projectionBson },
                { "skip", skip },
                { "limit", limit },
                { "batchSize", batchSize }
            };
            return await self.QueryCommand(command.ToJson());
        }

        /// <summary>
        /// 根據json查詢條件查詢View
        /// </summary>
        public static async ETTask<List<BsonDocument>> QueryCommand
        (
            this DBProxyComponent self,
            string collectionName,
            BsonDocument filterBson,
            BsonDocument projectionBson,
            int batchSize = 1000
        )
        {
            var command = new BsonDocument
            {
                { "find", collectionName },
                { "filter", filterBson },
                { "projection", projectionBson },
                { "batchSize", batchSize }
            };
            var json = command.ToJson();
            return await self.QueryCommand(json);
        }


        /// <summary>
        /// 根據json查詢條件查詢View，並排序和約束結果範圍，再取回相應筆數
        /// </summary>
        public static async ETTask<List<BsonDocument>> QuerySortCommand
        (
            this DBProxyComponent self,
            string collectionName,
            BsonDocument filterBson,
            BsonDocument sortBson,
            BsonDocument projectionBson,
            int skip = 0,
            int limit = 10,
            int batchSize = 1000
        )
        {
            var command = new BsonDocument
            {
                { "find", collectionName },
                { "filter", filterBson },
                { "sort", sortBson },
                { "projection", projectionBson },
                { "skip", skip },
                { "limit", limit },
                { "batchSize", batchSize }
            };
            var json = command.ToJson();
            return await self.QueryCommand(json);
        }

        public static async ETTask<List<BsonDocument>> QuerySortCommand
        (
            this DBProxyComponent self,
            string collectionName,
            BsonDocument sortBson,
            BsonDocument projectionBson,
            int skip = 0,
            int limit = 10,
            int batchSize = 1000
        )
        {
            var command = new BsonDocument
            {
                { "find", collectionName },
                { "sort", sortBson },
                { "projection", projectionBson },
                { "skip", skip },
                { "limit", limit },
                { "batchSize", batchSize }
            };
            var json = command.ToJson();
            return await self.QueryCommand(json);
        }

        /// <summary>
        /// 根據json查詢條件查詢View
        /// </summary>
        public static async ETTask<List<BsonDocument>> QueryCommand(this DBProxyComponent self, string json)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryCommandResponse dBQueryJsonViewResponse = (DBQueryCommandResponse)await session.Call(new DBQueryCommandRequest { Json = json });
            return dBQueryJsonViewResponse.BsonDocuments;
        }
        public static async ETTask<List<BsonDocument>> AggregateCommand(this DBProxyComponent self, string json)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBAggregateJsonResponse dBQueryJsonViewResponse = (DBAggregateJsonResponse)await session.Call(new DBAggregateJsonRequest { Json = json });
            return dBQueryJsonViewResponse.BsonDocuments;
        }

        /// <summary>
        /// 根據查詢表達式查詢，並返回筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<long> QueryCount<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            return await self.QueryCount<T>(json);
        }

        /// <summary>
        /// 根據查詢表達式查詢，並返回筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<long> QueryCount<T>(this DBProxyComponent self, List<Expression<Func<T, bool>>> exps) where T : ComponentWithId
        {
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            Doc.Clear();
            for (int i = 0; i < exps.Count; i++)
            {
                var exp = exps[i];
                ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
                Doc.AddRange(filter.Render(renderArgs));
            }
            string json = Doc.ToJson();
            return await self.QueryCount<T>(json);
        }

        /// <summary>
        /// 根據json查詢條件查詢，並返回筆數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="json"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static async ETTask<long> QueryCount<T>(this DBProxyComponent self, string json) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBQueryJsonCountResponse dbQueryJsonResponse = (DBQueryJsonCountResponse)await session.Call(new DBQueryJsonCountRequest { CollectionName = typeof(T).Name, Json = json });
            return dbQueryJsonResponse.Count;
        }

        /// <summary>
        /// 根據查詢表達式刪除一筆或多筆紀錄
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static async ETTask DeleteJson<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp) where T : ComponentWithId
        {
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            RenderArgs<T> renderArgs = new RenderArgs<T>(documentSerializer, serializerRegistry);
            string json = filter.Render(renderArgs).ToJson();
            await self.DeleteJson<T>(json);
        }

        /// <summary>
        /// 根據json刪除一筆或多筆紀錄
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async ETTask DeleteJson<T>(this DBProxyComponent self, string json) where T : ComponentWithId
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBDeleteJsonResponse dbQueryJsonResponse = (DBDeleteJsonResponse)await session.Call(new DBDeleteJsonRequest { CollectionName = typeof(T).Name, Json = json });
        }

        /// <summary>
        /// Mongo GridFs 上傳檔案
        /// </summary>
        public static async ETTask<ObjectId> UploadFile(this DBProxyComponent self, string fileName, byte[] source, BsonDocument meta)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBUploadFileResponse dBUploadFileResponse = (DBUploadFileResponse)await session.Call(new DBUploadFileRequest { FileName = fileName, Source = source, Meta = meta });
            return dBUploadFileResponse.Id;
        }

        /// <summary>
        /// Mongo GridFs 下載檔案
        /// </summary>
        public static async ETTask<byte[]> DownloadFile(this DBProxyComponent self, ObjectId id)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
            DBDownloadFileResponse dBDownloadFileResponse = (DBDownloadFileResponse)await session.Call(new DBDownloadFileRequest { Id = id });
            return dBDownloadFileResponse.Source;
        }


    }
}