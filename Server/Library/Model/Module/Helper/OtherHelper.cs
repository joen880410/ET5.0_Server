using Google.Protobuf.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ETModel
{
    public static class OtherHelper
    {
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public const int INVALID_ID = -1;
#if !DBVIEWGENERATOR && !JACFIT
        public static T Search<T>(List<T> list, Predicate<T> predicate)
        {
            if (list == null)
            {
                return default;
            }
            for (int i = 0; i < list.Count; i++)
            {
                T obj = list[i];
                if (predicate.Invoke(obj))
                {
                    return obj;
                }
            }
            return default;
        }

        public static List<T> SearchAll<T>(List<T> list, Predicate<T> predicate)
        {
            List<T> results = new List<T>();
            if (list == null)
            {
                return results;
            }
            for (int i = 0; i < list.Count; i++)
            {
                T obj = list[i];
                if (predicate.Invoke(obj))
                {
                    results.Add(obj);
                }
            }
            return results;
        }

        public static T Search<T>(T[] array, Predicate<T> predicate)
        {
            if (array == null)
            {
                return default;
            }
            for (int i = 0; i < array.Length; i++)
            {
                T obj = array[i];
                if (predicate.Invoke(obj))
                {
                    return obj;
                }
            }
            return default;
        }

        public static T Search<T>(RepeatedField<T> list, Predicate<T> predicate)
        {
            if (list == null)
            {
                return default;
            }
            for (int i = 0; i < list.Count; i++)
            {
                T obj = list[i];
                if (predicate.Invoke(obj))
                {
                    return obj;
                }
            }
            return default;
        }


        public static Dictionary<K, List<V>> Group<K, V>(IEnumerable<V> iter, Func<V, K> func)
        {
            if (iter == null)
            {
                return default;
            }
            return iter.Aggregate(new Dictionary<K, List<V>>(), (map, item) =>
            {
                var key = func.Invoke(item);
                if (!map.ContainsKey(key))
                {
                    map.Add(key, new List<V>());
                }
                map[key].Add(item);
                return map;
            });
        }

        public static string GetCallStackMessage()
        {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)
            List<string> list = new List<string>(stackFrames.Length);

            // write call stack method names
            foreach (StackFrame stackFrame in stackFrames)
            {
                list.Add(new StackTrace(stackFrame).ToString());
            }
            return string.Join("\r\n", list);
        }

        public static void ShowCallStackMessage()
        {
            Console.WriteLine(GetCallStackMessage());   // write method name
        }

        public static void LogCallStackMessage(string tag)
        {
            Log.Trace($"-------------{tag}-------------\r\n");
            Log.Trace(GetCallStackMessage());   // write method name
        }

        public static T CopyDeep<T>(T targetObj)
        {
            return BsonSerializer.Deserialize<T>(targetObj.ToJson());
        }

        public static BsonDocument GetDifferenceDocument(this Entity entity, Entity target)
        {
            var doc = entity.ToBsonDocument();
            return (BsonDocument)doc.Except(target.ToBsonDocument());
        }


        /// <summary>
        /// Converts a byte value to a human-readable size string with an appropriate suffix (e.g., KB, MB).
        /// </summary>
        /// <param name="value">The size in bytes.</param>
        /// <param name="decimalPlaces">The number of decimal places to include in the returned string. Default is 1.</param>
        /// <returns>A string that represents the size in a human-readable format with an appropriate suffix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="decimalPlaces"/> is less than 0.</exception>
        /// <example>
        /// <code>
        /// string result = SizeSuffix(123456789, 2);
        /// // result: "117.74 MB"
        /// </code>
        /// </example>
        public static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0)
            {
                return "-" + SizeSuffix(-value);
            }
            if (value == 0)
            {
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
            }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static float ToKB(int length)
        {
            return ((float)length / (1 << 10));
        }

        /// <summary>
        /// If you want to implement both "*" and "?"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WildCardToRegular(string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", "(?!-[a-zA-Z]).*") + "$";
        }

        public static async ETTask<List<T>> BatchRun<T>(Func<int, int, ETTask<List<T>>> func, int length, int batch = 10, int batchCount = 100)
        {
            return await BatchRun(func, new List<T>(), length, batch, batchCount, 0);
        }

        private static async ETTask<List<T>> BatchRun<T>(Func<int, int, ETTask<List<T>>> func, List<T> data, int length, int batch = 10, int batchCount = 100, int round = 0)
        {
            float countPerRound = batch * batchCount;
            int roundCount = (int)Math.Ceiling(length / countPerRound);

            if (round >= roundCount)
            {
                return data.OfType<T>().ToList();
            }

            ETTask<List<T>>[] eTTasks = new ETTask<List<T>>[batch];
            for (int i = 0; i < batch; i++)
            {
                int skip = round * (int)countPerRound + batchCount * i;
                int total = skip + batchCount;

                if (total > length)
                {
                    eTTasks[i] = func.Invoke(skip, length % batchCount);
                    break;
                }
                else
                {
                    eTTasks[i] = func.Invoke(skip, batchCount);
                }
            }
            List<T>[] result = await ETTask.WaitAll(eTTasks);
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == null)
                    continue;
                var list = result[i];
                for (int j = 0; j < list.Count; j++)
                {
                    data.Add(list[j]);
                }
            }
            return await BatchRun(func, data, length, batch, batchCount, ++round);
        }

        public static async ETTask<List<T>> BatchRunWithThread<T, K>(List<K> datas, Func<int, T> action)
        {
            ETTask<T>[] eTTasks = new ETTask<T>[datas.Count];
            for (int j = 0; j < datas.Count; j++)
            {
                ETTaskCompletionSource<T> tcs = new ETTaskCompletionSource<T>();
                int i = j;
                ThreadPool.QueueUserWorkItem(e =>
                {
                    // 執行緒例外如果沒catch，會導致進程崩潰
                    try
                    {
                        var result = action.Invoke(i);
                        OneThreadSynchronizationContext.Instance.Post(r => { tcs.TrySetResult((T)r); }, result);

                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        OneThreadSynchronizationContext.Instance.Post(r => { tcs.TrySetResult((T)r); }, null);
                    }
                }, null);
                eTTasks[i] = tcs.Task;
            }
            return (await ETTask.WaitAll(eTTasks)).ToList();
        }
        public static string PruneStringWithByteLimit(this string text, Encoding encoding, int byteLimit = -1)
        {
            byte[] bytes = encoding.GetBytes(text);
            if (bytes.Length == 0 || byteLimit < 0)
            {
                return text;
            }
            else
            {
                return encoding.GetString(bytes, 0, bytes.Length > byteLimit ? byteLimit : bytes.Length);
            }
        }

        public static bool ParseType(this object value, Type conversionType, out object parseValue)
        {
            try
            {
                parseValue = Convert.ChangeType(value, conversionType);
                return true;
            }
            catch (Exception)
            {
                parseValue = null;
                return false;
            }

        }

        public static bool IsMatchingType(int lhs, int rhs)
        {
            return (lhs & rhs) == rhs;
        }

        public static int SetFlagType(int lhs, int rhs)
        {
            if (IsMatchingType(lhs, rhs))
            {
                return lhs;
            }

            return lhs | rhs;
        }

        public static int ClearFlagType(int lhs, int rhs)
        {
            if (!IsMatchingType(lhs, rhs))
            {
                return lhs;
            }

            return lhs & ~rhs;
        }
        public static List<T> GetEnumValues<T>() where T : IConvertible
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException($"{type.Name} must be an enumerated type");
            return Enum.GetValues(type).OfType<T>().ToList();
        }
    }
}
#else
    }
}
#endif
