using Google.Protobuf.Collections;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    public static class IEnumerableHelper
    {
        private static readonly List<long> list = new List<long>();
        private static readonly Random rng = new Random();

        public static IEnumerable<long> Except(this IEnumerable<long> self, params long[] value)
        {
            list.Clear();
            list.AddRange(value);
            return self.Except(list);

        }
        /// <summary>
        /// 當index超過原本list長度會先塞default進去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<T> InsertRangeWithDefault<T>(this List<T> self, int index, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                new ArgumentNullException(nameof(collection));
            }
            if (index > self.Count)
            {
                var count = self.Count;
                for (int i = 0; i < index - count; i++)
                {
                    self.Add(default);
                }
                self.InsertRange(index, collection);
            }
            else
            {
                IEnumerator<T> enumerator = collection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (self[index] == null)
                    {
                        self[index] = enumerator.Current;
                        index++;
                    }
                    else
                    {
                        self.Insert(index++, enumerator.Current);
                    }
                }
            }
            return self;
        }
        public static IEnumerable<long> Concat(this IEnumerable<long> self, params long[] value)
        {
            list.Clear();
            list.AddRange(value);
            return self.Concat(list);
        }
        public static RepeatedField<T> ToRepeatedField<T>(this IEnumerable<T> self)
        {
            var datas = new RepeatedField<T>();
            datas.AddRange(self);
            return datas;
        }
        public static void Shuffle<T>(this IList<T> _list)
        {
            int n = _list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (_list[k], _list[n]) = (_list[n], _list[k]);
            }
        }
        public static BsonArray ToBsonArray(this IEnumerable list)
        {
            BsonArray bsonArray = new BsonArray();
            foreach (var item in list) 
            {
                bsonArray.Add(item.ToBsonDocument());
            }
            return bsonArray;
        }
    }
}