using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ETModel
{
	[ObjectSystem]
	public class DBQueryJsonSortTaskAwakeSystem : AwakeSystem<DBQueryJsonSortTask, string, ETTaskCompletionSource<List<ComponentWithId>>>
	{
		public override void Awake(DBQueryJsonSortTask self, string collectionName, ETTaskCompletionSource<List<ComponentWithId>> tcs)
		{
			self.CollectionName = collectionName;
			self.Tcs = tcs;
		}
	}
	public class DBQueryJsonSortTask : DBTask
	{
		public string CollectionName { get; set; }

		public string Json { get; set; }

		public string SortJson { get; set; }

		public int Skip { get; set; }

		public int Limit { get; set; }

		public ETTaskCompletionSource<List<ComponentWithId>> Tcs { get; set; }

		public override async ETTask Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
			try
			{
				// 执行查询数据库任务
				SortDefinition<ComponentWithId> sortDefinition = new JsonSortDefinition<ComponentWithId>(this.SortJson);
				FilterDefinition<ComponentWithId> filterDefinition = new JsonFilterDefinition<ComponentWithId>(this.Json);
				IAsyncCursor<ComponentWithId> cursor = await dbComponent.GetCollection(this.CollectionName).FindAsync(filterDefinition, new FindOptions<ComponentWithId, ComponentWithId>
				{
					Sort = sortDefinition,
					Skip = Skip,
					Limit = Limit
				});
				List<ComponentWithId> components = await cursor.ToListAsync();
				this.Tcs.SetResult(components);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"查询数据库异常! {CollectionName} {this.Json}", e));
			}
		}
	}
}
