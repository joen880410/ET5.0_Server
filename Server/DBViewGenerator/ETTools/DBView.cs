using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ETTools
{
    public abstract class DBView<T> : DBView where T : Entity
    {
        protected sealed override string ViewOn()
        {
            return typeof(T).Name;
        }

        public class Lookup<U> : Lookup where U : Entity
        {
            public sealed override string from => typeof(U).Name;
            public override string localField { protected set; get; }
            public override string foreignField { protected set; get; }
            public override string asField { protected set; get; } = string.Empty;

            public Lookup(string localField, string foreignField, string asField) : base(localField, foreignField, asField)
            {
                Check(localField, foreignField);
            }

            public Lookup(string localField, string foreignField) : base(localField, foreignField)
            {
                Check(localField, foreignField);
            }

            private void Check(string localField, string foreignField)
            {
                Type type = null;
                string[] sp = null;
                if (localField != _id)
                {
                    type = typeof(T);
                    sp = localField.Split('.');
                    for (int i = 0; i < sp.Length; i++)
                    {
                        string s = sp[i];
                        var pr = type.GetProperties().FirstOrDefault(e => e.Name == s);
                        if (pr == null)
                        {
                            throw new Exception($"localField:{localField} there is not on collection:{typeof(T).Name}");
                        }
                        type = pr.PropertyType;
                    }
                }
                if (foreignField != _id)
                {
                    type = typeof(U);
                    sp = foreignField.Split('.');
                    for (int i = 0; i < sp.Length; i++)
                    {
                        string s = sp[i];
                        var pr = type.GetProperties().FirstOrDefault(e => e.Name == s);
                        if (pr == null)
                        {
                            throw new Exception($"foreignField:{foreignField} there is not on collection:{typeof(U).Name}");
                        }
                        type = pr.PropertyType;
                    }
                }
            }
        }
    }

    public abstract class DBView : Entity
    {
        public const string _id = "_id";

        public const string _t = "_t";

        public const string C = "C";

        public const string DefaultString = "";

        public const long DefaultLong = 0L;

        public const int DefaultInt = 0;

        public const float DefaultFloat = 0f;

        public const double DefaultDouble = 0d;

        public const bool DefaultBool = false;

        protected BsonDocument addField;

        protected abstract string ViewOn();

        protected abstract List<Lookup> LookupOn();

        protected abstract List<BsonDocument> CreatePipelineOn();
        protected BsonDocument CreatePipeline()
        {
            return new BsonDocument()
            {
                {"addFields", new BsonArray(CreatePipelineOn())}
            };
        }


        public string viewName => GetType().Name;

        public List<ProjectBase> GetProjectFieldsInformation()
        {
            return GetType().GetProperties().Where(e => e.PropertyType.IsSubclassOf(typeof(ProjectBase))).Select(e => (ProjectBase)e.GetValue(this)).ToList();
        }
        public object GetProjectFieldsDefaultValue(string field)
        {
            var filedInfo = GetType().GetProperty(field);
            if (filedInfo.IsDefined(typeof(BsonDefaultValueAttribute), false))
            {
                BsonDefaultValueAttribute bsonDVAttr = (BsonDefaultValueAttribute)filedInfo.GetCustomAttribute(typeof(BsonDefaultValueAttribute));
                return bsonDVAttr.DefaultValue;
            }
            else
            {
                return null;
            }
        }
        public List<ProjectArrayBase> GetProjectArrayFieldsInformation()
        {
            return GetType().GetProperties().Where(e => e.PropertyType.IsSubclassOf(typeof(ProjectArrayBase))).Select(e => (ProjectArrayBase)e.GetValue(this)).ToList();
        }

        public List<ProjectFilterBase> GetProjectFilterFieldsInformation()
        {
            return GetType().GetProperties().Where(e => e.PropertyType.IsSubclassOf(typeof(ProjectFilterBase))).Select(e => (ProjectFilterBase)e.GetValue(this)).ToList();
        }
        public List<ProjectAfterFilterBase> GetProjectAfterFilterFieldsInformation()
        {
            return GetType().GetProperties().Where(e => e.PropertyType.IsSubclassOf(typeof(ProjectAfterFilterBase))).Select(e => (ProjectAfterFilterBase)e.GetValue(this)).ToList();
        }
        public List<Type> GetLookupDependencies()
        {
            var lookups = LookupOn();
            var dependencies = new List<Type>();
            for (int i = 0; i < lookups.Count; i++)
            {
                var lookup = lookups[i];
                var generic = lookup.GetType().GetGenericArguments().Where(e => e.Name != ViewOn() && e.IsSubclassOf(typeof(DBView))).FirstOrDefault();
                if (generic != default)
                    dependencies.Add(generic);
            }
            return dependencies;
        }

        private List<BsonDocument> ToViewCommandBsonDocuments()
        {
            var list = new List<BsonDocument>();
            // lookup collection
            list.AddRange(LookupOn().Select(e => e.ToLookupBsonDocumentCommand()));
            // project filter field
            var projectFilterDoc = ToProjectFilterCommand();
            if (projectFilterDoc.ElementCount != 0)
            {
                list.Add(projectFilterDoc);
            }
            var projectAfterFilterDoc = ToProjectAfterFilterCommand();
            if (projectAfterFilterDoc.ElementCount != 0)
            {
                list.Add(projectAfterFilterDoc);
            }
            // create pipeline
            list.Add(CreatePipeline());
            list.AddRange(AddSchemaField());
            // project field
            var projectDoc = ToProjectCommand();
            if (projectDoc.ElementCount != 0)
            {
                list.Add(projectDoc);
            }

            return list;
        }

        protected BsonDocument AddFieldOnView(string fieldName, string fieldValue)
        {
            return new BsonDocument
            {
                {
                    "$addFields", new BsonDocument
                    {
                        { fieldName, fieldValue }
                    }
                }
            };
        }

        protected BsonDocument AddFieldOnView(string fieldName, BsonArray fieldValue)
        {
            return new BsonDocument
            {
                {
                    "$addFields", new BsonDocument
                    {
                        { fieldName, fieldValue }
                    }
                }
            };
        }

        protected BsonDocument AddFieldOnView(string fieldName, BsonDocument expressBson)
        {
            return new BsonDocument
            {
                {
                    "$addFields", new BsonDocument
                    {
                        { fieldName, expressBson }
                    }
                }
            };
        }


        protected BsonDocument AddFieldOnView(string fieldName, BsonDocument expressBson, BsonValue defaultVal)
        {
            return new BsonDocument
            {
                { fieldName, new BsonDocument
                    {
                        {
                            "$ifNull", new BsonArray
                            {
                                expressBson, defaultVal
                            }
                        }
                    }
                }
            };
        }

        protected BsonDocument AddFieldOnView(string fieldName, string fieldValue, BsonValue defaultVal)
        {
            return new BsonDocument
            {
                {
                    "$addFields", new BsonDocument
                    {
                        { fieldName, new BsonDocument
                            {
                                {
                                    "$ifNull", new BsonArray
                                    {
                                        fieldValue, defaultVal
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        protected BsonDocument AddFieldOnViewWithArrayElemAt(string fieldName, string value, BsonValue defaultValue, int index = 0)
        {
            return AddFieldOnView(fieldName, new BsonDocument
            {
                {
                    "$ifNull", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "$arrayElemAt", new BsonArray { value, index } }
                        }, defaultValue
                    }
                }
            });
        }
        protected BsonDocument AddFilteredOnViewField(FilteredField filter, BsonValue defaultValue)
        {
            return AddFieldOnView(filter.fieldName, new BsonDocument
            {
                {
                    "$first", new BsonDocument
                    {
                        {
                            "$map",filter.ToBsonDocumentCommand()
                        }
                    }

                },
            }, defaultValue);
        }
        protected BsonDocument AddFieldOnViewWithArrayElemAt(string fieldName, BsonDocument value, BsonValue defaultValue, int index = 0)
        {
            return AddFieldOnView(fieldName, new BsonDocument
            {
                {
                    "$ifNull", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "$arrayElemAt", new BsonArray { value, index } }
                        }, defaultValue
                    }
                }
            });
        }
        /// <summary>
        /// <param name="queryValueName">queryValueName:如果要找的變數欄位是DB中的只能是一個"$"</param>
        /// </summary>
        public class FilteredField : FilterBase
        {
            public string MapAs { get; set; }
            public string MapField { get; set; }
            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { "input", new BsonDocument
                        {
                            { "$filter", new BsonDocument
                                {
                                    { "input", $"${inputName}" },
                                    { "as", asName },
                                    { "cond",
                                        new BsonDocument()
                                        {
                                            {
                                                $"{queryObject.queryOperatorName}",new BsonArray()
                                                {
                                                    $"{queryObject.queryFieldName}",$"${queryObject.queryValueName}"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                    },
                    { "as", MapAs },
                    { "in", $"$${MapAs}.{MapField}" }
                };
            }
        }
        protected BsonDocument ConvertArrayAsDictionaryOnView(string newFieldName, string arrayFieldName, string queryArrayName, string keyFieldName, string valueFieldName)
        {
            return AddFieldOnView(newFieldName, new BsonDocument
            {
                { "$arrayToObject", new BsonDocument
                    {
                        {
                            "$map", new BsonDocument
                            {
                                { "input", $"${arrayFieldName}" },
                                { "as", $"this" },
                                { "in", new BsonDocument
                                    {
                                        {
                                            "k", new BsonDocument
                                            {
                                                { "$arrayElemAt", new BsonArray
                                                    {
                                                        new BsonDocument
                                                        {
                                                            {
                                                                "$map", new BsonDocument
                                                                {
                                                                    {
                                                                        "input", new BsonDocument
                                                                        {
                                                                            {
                                                                                "$filter", new BsonDocument
                                                                                {
                                                                                    { "input", $"${queryArrayName}" },
                                                                                    { "as", $"{queryArrayName.ToLower()}" },
                                                                                    { "cond", new BsonDocument
                                                                                        {
                                                                                            {
                                                                                                "$eq", new BsonArray
                                                                                                {
                                                                                                    $"$${queryArrayName.ToLower()}.{valueFieldName}", "$$this"
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    },
                                                                    { "as", "this2" },
                                                                    {
                                                                        "in", new BsonDocument
                                                                        {
                                                                            { "$toString", $"$$this2.{keyFieldName}" }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }, 0
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            "v", "$$this"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        private BsonDocument ToProjectCommand()
        {
            var list = GetProjectFieldsInformation();
            var doc = new BsonDocument();
            if (list.Count == 0)
            {
                return doc;
            }
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var command = item.ToBsonDocumentCommand();
                var element = command.GetElement(0); // 0:表示只拿第1個元素 
                doc.Add(element.Name, element.Value);
            }

            var arrayList = GetProjectArrayFieldsInformation();
            if (arrayList.Count != 0)
            {
                for (int i = 0; i < arrayList.Count; i++)
                {
                    var item = arrayList[i];
                    var command = item.ToBsonDocumentCommand();
                    for (int j = 0; j < command.Count(); j++)
                    {
                        var element = command.GetElement(j);
                        doc.Add(element.Name, element.Value);
                    }
                }
            }

            // add schema
            doc.Add(_t, 1);
            doc.Add(C, 1);

            return new BsonDocument
            {
                { "$project", doc }
            };
        }

        private BsonDocument ToProjectFilterCommand()
        {
            var list = GetProjectFilterFieldsInformation();
            var doc = new BsonDocument();
            if (list.Count == 0)
            {
                return doc;
            }
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var command = item.ToBsonDocumentCommand();
                var element = command.GetElement(0); // 0:表示只拿第1個元素 
                doc.Add(element.Name, element.Value);
            }

            return new BsonDocument
            {
                { "$project", doc }
            };
        }
        private BsonDocument ToProjectAfterFilterCommand()
        {
            var list = GetProjectAfterFilterFieldsInformation();
            var list2 = GetProjectFilterFieldsInformation();
            var doc = new BsonDocument();
            if (list.Count == 0 || list2.Count == 0)
            {
                return doc;
            }
            for (int i = 0; i < list2.Count; i++)
            {
                var item = list2[i];
                var command = item.ToBsonCommand();
                var element = command.GetElement(0); // 0:表示只拿第1個元素 
                doc.Add(element.Name, element.Value);
            }
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var command = item.ToBsonDocumentCommand();
                var element = command.GetElement(0); // 0:表示只拿第1個元素 
                doc.Add(element.Name, element.Value);
            }

            return new BsonDocument
            {
                { "$project", doc }
            };
        }
        public string ToViewCommand()
        {
            return new BsonDocument
            {
                { "create", GetType().Name },
                { "viewOn", ViewOn() },
                { "pipeline", new BsonArray(ToViewCommandBsonDocuments()) }
            }.ToJson().Replace("\"", "\\\"");
        }

        private List<BsonDocument> AddSchemaField()
        {
            return new List<BsonDocument>
            {
                AddFieldOnView(_t, GetType().Name),
                AddFieldOnView(C, new BsonArray())
            };
        }

        public class Lookup
        {
            public virtual string from { protected set; get; }
            public virtual string localField { protected set; get; }
            public virtual string foreignField { protected set; get; }
            public virtual string asField { protected set; get; }

            //public Lookup(string from, string localField, string foreignField) : this(localField, foreignField)
            //{
            //    this.from = from;
            //}

            public Lookup(string localField, string foreignField, string asField) : this(localField, foreignField)
            {
                this.asField = asField;
            }

            protected Lookup(string localField, string foreignField)
            {
                this.localField = localField;
                this.foreignField = foreignField;
            }

            public BsonDocument ToLookupBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    {
                        "$lookup", new BsonDocument
                        {
                            { "from", from },
                            { "localField", localField },
                            { "foreignField", foreignField },
                            { "as", !asField.IsEmpty() ? asField : from }
                        }
                    }
                };
            }
        }

        public abstract class ProjectBase
        {
            public string fieldName { set; get; }

            public virtual string typeName { set; get; }

            public abstract BsonDocument ToBsonDocumentCommand();
        }

        public abstract class ProjectArrayBase
        {
            public string fieldName { set; get; }
            public virtual string typeName { set; get; }
            public virtual Type type { set; get; }
            public List<ProjectBase> GetProjectFieldsInformation()
            {
                return type.GetProperties().Where(e => e.PropertyType.IsSubclassOf(typeof(ProjectBase)))
                                           .Select(e => (ProjectBase)e.GetValue(Activator.CreateInstance(type))).ToList();
            }
            public abstract BsonDocument ToBsonDocumentCommand();
        }

        public abstract class ProjectFilterBase : FilterBase
        {
            public abstract BsonDocument ToBsonCommand();
        }

        public class Project<V> : ProjectBase
        {
            public override string typeName => typeof(V).Name;

            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { fieldName, 1 }
                };
            }
        }
        public abstract class ProjectAfterFilterBase : FilterBase
        {

        }
        public struct QueryObject
        {
            public string queryOperatorName { set; get; }
            public string queryFieldName { set; get; }
            public BsonValue queryValueName { set; get; }
        }


        public abstract class FilterBase
        {
            public string fieldName { set; get; }
            public string inputName { set; get; }
            public string asName { set; get; }

            public QueryObject queryObject { set; get; }

            public abstract BsonDocument ToBsonDocumentCommand();
        }
        /// <summary>
        /// 依條件做除法
        /// </summary>
        /// <typeparam name="V"></typeparam>
        public class ProjectCondDivide<V> : Project<V>
        {
            public string divisorName;
            public string dividendName;
            public string condName;
            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { fieldName, new BsonDocument
                        {
                            { "$cond",  new BsonArray()
                            {
                                new BsonDocument()
                                {
                                    { "$eq",new BsonArray()
                                        { $"${condName}",0}
                                    }
                                },
                                0,
                                new BsonDocument()
                                {
                                    {
                                       "$convert",new BsonDocument()
                                       {
                                           {"input",new BsonDocument()
                                           {
                                               {"$divide",new BsonArray()
                                                   {$"${divisorName}",$"${dividendName}"}
                                               }
                                           }},
                                           {"to",ToConvertType<V>() }
                                        }
                                    }
                                }
                            }
                            }
                        }
                    }
                };
            }
        }
        /// <summary>
        /// 依條件做除法
        /// </summary>
        /// <typeparam name="V"></typeparam>
        public class ProjectCond : ProjectFilterBase
        {
            public override BsonDocument ToBsonCommand()
            {
                return new BsonDocument
                {
                    { fieldName, 1 }
                };
            }

            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { fieldName, new BsonDocument
                        {
                            { "$cond",  new BsonArray()
                                {queryObject.ToBsonDocument(),}
                            }
                        }
                    }
                };
            }
        }
        public class ProjectArray<V> : ProjectArrayBase
        {
            public override string typeName => $"RepeatedField<{typeof(V).Name}>";
            public override Type type => typeof(V);

            public override BsonDocument ToBsonDocumentCommand()
            {
                var list = GetProjectFieldsInformation();
                if (list.Count <= 0)
                {
                    return new BsonDocument();
                }

                var bson = new BsonDocument();
                for (int i = 0; i < list.Count; i++)
                {
                    var command = list[i].ToBsonDocumentCommand();
                    var element = command.GetElement(0);
                    var arrayElement = new BsonElement($"{fieldName}.{element.Name}", element.Value);
                    bson.Add(arrayElement);
                }

                return bson;
            }
        }
        /// <summary>
        /// <param name="queryValueName">queryValueName:如果要找的變數欄位是DB中的只能是一個"$"</param>
        /// </summary>
        public class ProjectFilter : ProjectFilterBase
        {
            public override BsonDocument ToBsonCommand()
            {
                return new BsonDocument
                {
                    { fieldName, 1 }
                };
            }

            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { fieldName, new BsonDocument
                        {
                            { "$filter", new BsonDocument
                                {
                                    { "input", inputName },
                                    { "as", asName },
                                    { "cond",
                                        new BsonDocument()
                                        {
                                            {
                                                $"{queryObject.queryOperatorName}",new BsonArray()
                                                {
                                                    $"{queryObject.queryFieldName}",queryObject.queryValueName
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }
        public class ProjectAfterFilter : ProjectAfterFilterBase
        {
            /// <summary>
            /// 有多筆的查詢條件
            /// </summary>
            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                    { fieldName, new BsonDocument
                        {
                            { "$filter", new BsonDocument
                                {
                                    { "input", inputName },
                                    { "as", asName },
                                    { "cond",
                                        new BsonDocument()
                                        {
                                            {
                                                $"{queryObject.queryOperatorName}",new BsonArray()
                                                {
                                                    $"{queryObject.queryFieldName}",queryObject.queryValueName
                                                }
                                            }
                                        }
                                    }
                                 }
                            }
                        }
                    }
                };
            }
        }
        /// <summary>
        /// 有多筆的查詢條件
        /// </summary>
        public class ProjectAfterFilters : ProjectAfterFilterBase
        {
            /// <summary>
            /// 有多筆的查詢條件
            /// </summary>
            public List<QueryObject> queryObjects;
            public override BsonDocument ToBsonDocumentCommand()
            {
                var bson = new List<BsonDocument>();
                for (int i = 0; i < queryObjects.Count; i++)
                {
                    bson.Add(
                        new BsonDocument()
                        {
                            {
                                $"{queryObjects[i].queryOperatorName}",new BsonArray()
                                    {
                                        $"{queryObjects[i].queryFieldName}",queryObjects[i].queryValueName
                                    }
                                }
                            }
                        );
                }

                return new BsonDocument
                {
                    { fieldName, new BsonDocument
                        {
                            { "$filter", new BsonDocument
                                {
                                    { "input", inputName },
                                    { "as", asName },
                                    { "cond",new BsonDocument()
                                        {
                                            {"$and",bson.ToBsonArray()}
                                        }
                                    }
                                 }
                            }
                        }
                    }
                };
            }
        }


        /// <summary>
        /// 沒有查詢條件
        /// </summary>
        public class ProjectNoFilter : ProjectFilterBase
        {
            public override BsonDocument ToBsonCommand()
            {
                return new BsonDocument
                {
                    { fieldName , 1}
                };
            }
            public override BsonDocument ToBsonDocumentCommand()
            {
                return new BsonDocument
                {
                   { fieldName , 1}
                };
            }
        }

        public BsonDocument SortBsonArray(string bsonName, string sortKey, double sortMode)
        {
            return new BsonDocument()
            {
                {
                        "$addFields", new BsonDocument
                        {
                            {
                                bsonName,new BsonDocument()
                                {
                                    {
                                        "$sortArray",new BsonDocument()
                                        {
                                            {"input", $"${bsonName}" },
                                            {"sortBy",new BsonDocument()
                                                {
                                                    {
                                                        sortKey,sortMode
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                }
            };
        }
        /// <summary>
        /// 要轉型mongoDB類型要用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToConvertType<T>()
        {
            var type = typeof(T);
            switch (type.Name)
            {
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "Single":
                case "Double":
                    return "double";
                default:
                    throw new Exception($"不支援此類型: {type}");
            }
        }
    }
}
