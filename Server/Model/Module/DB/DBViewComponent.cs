using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ETModel
{
    /// <summary>
    /// 用来缓存数据
    /// </summary>
    public class DBViewComponent : Component
    {
        public DBComponent db { private set; get; }
        public void Awake()
        {
            this.db = Game.Scene.GetComponent<DBComponent>();
        }

        public void Destroy()
        {

        }
        public async ETTask DropViewAsync(string viewName)
        {
            if (db.GetCollectionExists(viewName))
            {
                await db.database.DropCollectionAsync(viewName);
            }
        }

        private class DBViewNode
        {
            public DBViewBase dbView { private set; get; }

            public string viewName => dbView.GetType().Name;

            public readonly List<Type> dependencyTypes = new List<Type>();

            public readonly List<DBViewNode> childNodes = new List<DBViewNode>();

            public int weight { private set; get; }

            public DBViewNode(DBViewBase dbView) 
            {
                this.dbView = dbView;
            }

            public int GetWeight() 
            {
                int max = 0;
                for (int i = 0; i < childNodes.Count; i++) 
                {
                    var node = childNodes[i];
                    max = Math.Max(max, node.GetWeight());
                }
                if (childNodes.Count != 0)
                {
                    weight = max + 1;
                    return weight;
                }
                else 
                {
                    weight = max;
                    return weight;
                }
            }
        }

        public async ETTask CreateAllViewAsync()
        {
            Dictionary<Type, DBViewNode> graph = new Dictionary<Type, DBViewNode>();
            foreach (Type type in typeof(DBViewBase).Assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(DBViewBase)) && !type.IsAbstract) 
                {
                    CreateDBViewNode(graph, null, type);
                }
            }
            var sortedNodes = graph.Select(e => e.Value).OrderByDescending(e => e.GetWeight()).ToList();
            for (int i = 0; i < sortedNodes.Count; i++) 
            {
                var dbView = sortedNodes[i].dbView;
                var viewName = sortedNodes[i].viewName;
                if (db.GetCollectionExists(viewName))
                {
                    await db.database.DropCollectionAsync(viewName);
                }
                await db.database.RunCommandAsync<BsonDocument>(dbView._DBViewCommand);
            }
        }

        private void CreateDBViewNode(Dictionary<Type, DBViewNode> graph, DBViewNode childNode, Type type)
        {
            if (!graph.TryGetValue(type, out DBViewNode node))
            {
                DBViewDependencyAttribute attribute = type.GetCustomAttribute<DBViewDependencyAttribute>();
                DBViewBase dBView = (DBViewBase)Activator.CreateInstance(type);
                node = new DBViewNode(dBView);
                if (attribute != null)
                {
                    node.dependencyTypes.AddRange(attribute.Dependencies.ToList());
                }
                graph.Add(type, node);
            }
            if (childNode != null)
            {
                if (node.childNodes.Contains(childNode)) 
                {
                    return;
                }
                node.childNodes.Add(childNode);
            }
            for (int i = 0; i < node.dependencyTypes.Count; i++) 
            {
                var dependency = node.dependencyTypes[i];
                CreateDBViewNode(graph, node, dependency);
            }
        }
    }
}

