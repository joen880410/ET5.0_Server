using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;
using System.ComponentModel;

namespace ETModel
{
    public static class MongoHelper
    {
#if LIBRARY
        public static List<Type> IgnoreType = new List<Type>() { typeof(StartConfig) };
#endif
        private static readonly BsonDocument Doc = new BsonDocument();
        static MongoHelper()
        {
            var objectSerializer = new ObjectSerializer(ObjectSerializer.AllAllowedTypes);
            BsonSerializer.RegisterSerializer(objectSerializer);
            ConventionPack conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };

            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);

            Type[] types = typeof(Game).Assembly.GetTypes();

            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(Component)))
                {
                    continue;
                }

                if (type.IsGenericType)
                {
                    continue;
                }
#if LIBRARY
                if (IgnoreType.Any(e => e == type))
                {
                    continue;
                }
#endif
                try
                {
                    if (!BsonClassMap.IsClassMapRegistered(type))
                    {
                        BsonClassMap.LookupClassMap(type);
                    }

                }
                catch (Exception e)
                {
                    Log.Error($"call {nameof(BsonClassMap)}.{nameof(BsonClassMap.LookupClassMap)} error: {type.Name} {e}");
                }

            }
        }
        /// <summary>
        /// DB紀錄原始資料
        /// </summary>
        public const string ORIGIN_Str = "Origin";
        public static string ToJson(object obj)
        {
            return obj.ToJson();
        }

        public static string ToJson(object obj, JsonWriterSettings settings)
        {
            return obj.ToJson(settings);
        }

        public static T FromJson<T>(string str)
        {
            return BsonSerializer.Deserialize<T>(str);
        }

        public static object FromJson(Type type, string str)
        {
            return BsonSerializer.Deserialize(str, type);
        }

        public static byte[] ToBson(object obj)
        {
            return obj.ToBson();
        }

        public static void ToBson(object obj, MemoryStream stream)
        {
            using (BsonBinaryWriter bsonWriter = new BsonBinaryWriter(stream, BsonBinaryWriterSettings.Defaults))
            {
                BsonSerializationContext context = BsonSerializationContext.CreateRoot(bsonWriter);
                BsonSerializationArgs args = default(BsonSerializationArgs);
                args.NominalType = typeof(object);
                IBsonSerializer serializer = BsonSerializer.LookupSerializer(args.NominalType);
                serializer.Serialize(context, args, obj);
            }
        }

        public static object FromBson(Type type, byte[] bytes)
        {
            object message = BsonSerializer.Deserialize(bytes, type);
            if (type is ISupportInitialize iSupportInitialize)
            {
                iSupportInitialize.EndInit();
            }
            return message;
        }

        public static object FromBson(Type type, byte[] bytes, int index, int count)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes, index, count))
            {
                object message = BsonSerializer.Deserialize(memoryStream.GetBuffer(), type);
                if (type is ISupportInitialize iSupportInitialize)
                {
                    iSupportInitialize.EndInit();
                }
                return message;
            }
        }

        public static object FromBson(object instance, byte[] bytes, int index, int count)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes, index, count))
            {
                object message = BsonSerializer.Deserialize(bytes, instance.GetType());
                if (instance is ISupportInitialize iSupportInitialize)
                {
                    iSupportInitialize.EndInit();
                }
                return message;
            }
        }

        public static object FromBson(object instance, Stream stream)
        {
            Type type = instance.GetType();
            object message = BsonSerializer.Deserialize(stream, type);
            if (type is ISupportInitialize iSupportInitialize)
            {
                iSupportInitialize.EndInit();
            }
            return message;
        }
        public static T FromBson<T>(byte[] bytes)
        {
            return (T)FromBson(typeof(T), bytes);
        }

        public static T FromBson<T>(byte[] bytes, int index, int count)
        {
            return (T)FromBson(typeof(T), bytes, index, count);
        }

        public static object FromStream(Type type, Stream stream)
        {
            object message = BsonSerializer.Deserialize(stream, type);
            if (type is ISupportInitialize iSupportInitialize)
            {
                iSupportInitialize.EndInit();
            }
            return message;
        }

        public static T Clone<T>(T t)
        {
            return FromBson<T>(ToBson(t));
        }

        public static void AvoidAOT()
        {
            ArraySerializer<int> aint = new ArraySerializer<int>();
            ArraySerializer<string> astring = new ArraySerializer<string>();
            ArraySerializer<long> along = new ArraySerializer<long>();
            EnumerableInterfaceImplementerSerializer<List<int>> e = new EnumerableInterfaceImplementerSerializer<List<int>>();
            EnumerableInterfaceImplementerSerializer<List<int>, int> elistint = new EnumerableInterfaceImplementerSerializer<List<int>, int>();
        }
        private static readonly List<BsonDocument> lists = new List<BsonDocument>();
        public static List<BsonDocument> ToBsonDocumentList(this BsonArray array)
        {
            lists.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                lists.Add(array[i].AsBsonDocument);
            }
            return lists;
        }

        public static BsonType ToBsonType(string type)
        {
            var bsonType = BsonType.Null;
            switch (type)
            {
                case nameof(BsonValue.ToInt32):
                    bsonType = BsonType.Int32;
                    break;
                case nameof(BsonValue.ToInt64):
                    bsonType = BsonType.Int64;
                    break;
                case nameof(BsonValue.ToDouble):
                    bsonType = BsonType.Double;
                    break;
                case nameof(BsonValue.ToString):
                    bsonType = BsonType.String;
                    break;
                default:
                    throw new Exception($"不支援此類型: {type}");
            }

            return bsonType;
        }

        public static BsonValue ToBsonNullValue(BsonType bsonType)
        {
            BsonValue bsonValue = BsonNull.Value;
            switch (bsonType)
            {
                case BsonType.Int32:
                    bsonValue = 0;
                    break;
                case BsonType.Int64:
                    bsonValue = 0L;
                    break;
                case BsonType.Double:
                    bsonValue = 0d;
                    break;
                case BsonType.String:
                    bsonValue = string.Empty;
                    break;
                default:
                    throw new Exception($"不支援此類型: {bsonType}");
            }

            return bsonValue;
        }

        public static BsonValue ToBsonNullValue(BsonValue bsonValue)
        {
            BsonValue value = BsonNull.Value;
            switch (bsonValue.BsonType)
            {
                case BsonType.Int32:
                    value = bsonValue.AsInt32 != 0 ? bsonValue : 0;
                    break;
                case BsonType.Int64:
                    value = bsonValue.AsInt64 != 0L ? bsonValue : 0L;
                    break;
                case BsonType.Double:
                    value = bsonValue.AsDouble != 0d ? bsonValue : 0d;
                    break;
                case BsonType.String:
                    value = bsonValue.AsString != string.Empty ? bsonValue : string.Empty;
                    break;
                default:
                    throw new Exception($"不支援此類型: {bsonValue.BsonType}");
            }

            return value;
        }

        public static BsonValue GetBsonValue(string key, BsonValue bsonDefaultValue, BsonDocument bson)
        {
            var initVal = ToBsonNullValue(bsonDefaultValue);
            if (initVal == BsonNull.Value)
            {
                return initVal;
            }

            if (bson == null)
            {
                bson = new BsonDocument();

                bson[key] = initVal;

                return bson[key];
            }

            BsonValue originValue = BsonNull.Value;
            switch (bsonDefaultValue.BsonType)
            {
                case BsonType.Int32:
                    originValue = bson.TryGetValue(key, out var intValue) ? intValue.AsInt32 : initVal;
                    break;
                case BsonType.Int64:
                    originValue = bson.TryGetValue(key, out var longValue) ? longValue.AsInt64 : initVal;
                    break;
                case BsonType.Double:
                    originValue = bson.TryGetValue(key, out var doubleValue) ? doubleValue.AsDouble : initVal;
                    break;
                case BsonType.String:
                    originValue = bson.TryGetValue(key, out var stringValue) ? stringValue.AsString : initVal;
                    break;
                case BsonType.Array:
                    originValue = bson.TryGetValue(key, out var arrayValue) ? arrayValue.AsBsonArray : initVal;
                    break;
                default:
                    throw new Exception($"不支援此類型: {bsonDefaultValue.BsonType}");
            }

            return originValue;
        }

        public static void SetBsonValue(this BsonDocument bson, string key, BsonValue bsonValue)
        {
            var originValue = GetBsonValue(key, bsonValue, bson);
            if (originValue == BsonNull.Value)
            {
                return;
            }

            if (!bson.Contains(key))
            {
                bson.Add(key, bsonValue);
                return;
            }

            switch (bsonValue.BsonType)
            {
                case BsonType.Int32:
                    {
                        if (originValue.AsInt32 != bsonValue.AsInt32)
                        {
                            bson[key] = bsonValue.AsInt32;
                        }
                        break;
                    }
                case BsonType.Int64:
                    {
                        if (originValue.AsInt64 != bsonValue.AsInt64)
                        {
                            bson[key] = bsonValue.AsInt64;
                        }
                        break;
                    }
                case BsonType.Double:
                    {
                        if (Math.Abs(originValue.AsDouble - bsonValue.AsDouble) > 0f)
                        {
                            bson[key] = bsonValue.AsDouble;
                        }
                        break;
                    }
                case BsonType.String:
                    {
                        if (originValue.AsString != bsonValue.AsString)
                        {
                            bson[key] = bsonValue.AsString;
                        }
                        break;
                    }
                case BsonType.Array:
                    {
                        if (originValue.AsBsonArray != bsonValue.AsBsonArray)
                        {
                            bson[key] = bsonValue.AsBsonArray;
                        }
                        break;
                    }
                default:
                    throw new Exception($"不支援此類型: {bsonValue.BsonType}");
            }
        }

        public static void SetBsonValue(this BsonDocument bson, string key, BsonValue bsonValue, BsonDocument log)
        {
            var originValue = GetBsonValue(key, bsonValue, bson);
            if (originValue == BsonNull.Value)
            {
                return;
            }

            if (!bson.Contains(key))
            {
                bson.Add(key, bsonValue);
                log[key] = bson[key];
                return;
            }

            switch (bsonValue.BsonType)
            {
                case BsonType.Int32:
                    {
                        if (originValue.AsInt32 != bsonValue.AsInt32)
                        {
                            bson[key] = bsonValue.AsInt32;
                            log[key] = bson[key];
                        }
                        break;
                    }
                case BsonType.Int64:
                    {
                        if (originValue.AsInt64 != bsonValue.AsInt64)
                        {
                            bson[key] = bsonValue.AsInt64;
                            log[key] = bson[key];
                        }
                        break;
                    }
                case BsonType.Double:
                    {
                        if (Math.Abs(originValue.AsDouble - bsonValue.AsDouble) > 0f)
                        {
                            bson[key] = bsonValue.AsDouble;
                            log[key] = bson[key];
                        }
                        break;
                    }
                case BsonType.String:
                    {
                        if (originValue.AsString != bsonValue.AsString)
                        {
                            bson[key] = bsonValue.AsString;
                            log[key] = bson[key];
                        }
                        break;
                    }
                default:
                    throw new Exception($"不支援此類型: {bsonValue.BsonType}");
            }
        }

        public static void AddBsonValue(this BsonDocument bson, string key, BsonValue bsonValue)
        {
            var initVal = ToBsonNullValue(bsonValue.BsonType);
            if (initVal == BsonNull.Value)
            {
                return;
            }

            var val = GetBsonValue(key, initVal, bson);

            switch (bsonValue.BsonType)
            {
                case BsonType.Int32:
                    {
                        bson[key] = val.AsInt32 + bsonValue.AsInt32;
                        break;
                    }
                case BsonType.Int64:
                    {
                        bson[key] = val.AsInt64 + bsonValue.AsInt64;
                        break;
                    }
                case BsonType.Double:
                    {
                        bson[key] = val.AsDouble + bsonValue.AsDouble;
                        break;
                    }
                case BsonType.String:
                    {
                        if (val.AsString != bsonValue.AsString)
                        {
                            bson[key] = bsonValue.AsString;
                        }
                        break;
                    }
                default:
                    throw new Exception($"不支援此類型: {bsonValue.BsonType}");
            }
        }

        public static void AddBsonValue(this BsonDocument bson, string key, BsonValue bsonValue, BsonDocument log)
        {
            var val = GetBsonValue(key, bsonValue, bson);

            switch (bsonValue.BsonType)
            {
                case BsonType.Int32:
                    {
                        log[$"{ORIGIN_Str}_{key}"] = val.AsInt32;
                        bson[key] = val.AsInt32 + bsonValue.AsInt32;
                        log[key] = bson[key];
                        break;
                    }
                case BsonType.Int64:
                    {
                        log[$"{ORIGIN_Str}_{key}"] = val.AsInt64;
                        bson[key] = val.AsInt64 + bsonValue.AsInt64;
                        log[key] = bson[key];
                        break;
                    }
                case BsonType.Double:
                    {
                        log[$"{ORIGIN_Str}_{key}"] = val.AsDouble;
                        bson[key] = val.AsDouble + bsonValue.AsDouble;
                        log[key] = bson[key];
                        break;
                    }
                case BsonType.String:
                    {
                        if (val.AsString != bsonValue.AsString)
                        {
                            log[$"{ORIGIN_Str}_{key}"] = val.AsString;
                            bson[key] = bsonValue.AsString;
                            log[key] = bson[key];
                        }
                        break;
                    }
                default:
                    throw new Exception($"不支援此類型: {bsonValue.BsonType}");
            }
        }
        public static bool TryUpdateField(this BsonDocument doc, string key, BsonValue newValue)
        {
            bool isChanged = false;
            if (doc.TryGetValue(key, out var currentValue))
            {
                if (!currentValue.Equals(BsonValue.Create(newValue)))
                {
                    doc[key] = newValue; // 更新值
                    isChanged = true;   // 標記為改變
                }
            }
            else
            {
                // 若字段不存在，也可以選擇添加，根據需求調整
                doc[key] = newValue;
                isChanged = true;
            }

            return isChanged;
        }

       

    }
}