using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    public static class RandomHelper
    {
        public static readonly Random random = new Random();

        public const float RandFactor = 0.01f; // 隨機0 - 1切割100等分

        public static ulong RandUInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static long RandInt64()
        {
            var bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 获取lower与Upper之间的随机数 (包含最小值lower，但不會包含最大值Upper)
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int RandomNumber(int lower, int upper)
        {
            int value = random.Next(lower, upper);
            return value;
        }
        /// <summary>
        /// 返回 [minInclusive~maxInclusive] 內的隨機浮點數（範圍包括在內）
        /// </summary>
        /// <param name="minInclusive">最小值</param>
        /// <param name="maxInclusive">最大值</param>
        /// <returns></returns>
        public static float RandomNumber(float minInclusive, float maxInclusive)
        {
            if (minInclusive == maxInclusive)
            {
                return maxInclusive;
            }
            if (minInclusive > maxInclusive)
            {
                throw new ArgumentOutOfRangeException(nameof(minInclusive), $"cannot be greater than {nameof(maxInclusive)}.");
            }
            const float randFactor = 0.01f; //隨機0 - 1切割成100等分
            var r = random.NextDouble();//取隨機數
            var s = (maxInclusive - minInclusive) * randFactor;
            var r2 = r / randFactor;
            var s2 = (float)Math.Round((s * r2) + minInclusive, 7);

            return s2;
        }
        public static float RandomNumber(double minInclusive, double maxInclusive)
        {
            return RandomNumber((float)minInclusive, (float)maxInclusive);
        }
        /// <summary>
        /// 傳回大於或等於 0.0，且小於 1.0 的隨機浮點數
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble()
        {
            return random.NextDouble();
        }

        private static int Rand(int n)
        {
            // 注意，返回值是左闭右开，所以maxValue要加1
            return random.Next(1, n + 1);
        }

        public static T RandomList<T>(this List<T> list)
        {
            return list[RandomNumber(0, list.Count)];
        }

        /// <summary>
        /// 隨機根據權重陣列選出一個 index
        /// </summary>
        /// <param name="weights">每個選項的權重</param>
        /// <returns>選中的index，如果錯誤則回傳最後一個</returns>
        public static int RandomByWeight(int[] weights)
        {
            int sum = weights.Sum();
            int number_rand = Rand(sum);
            return RandomByWeight(weights, number_rand);
        }

        /// <summary>
        /// 根據值回傳對應陣列index所在區間
        /// </summary>
        /// <param name="weights">權重陣列</param>
        /// <param name="value">外部傳入的值</param>
        /// <returns>選中的index，或是最後一個</returns>
        public static int RandomByWeight(int[] weights, int value)
        {
            int cumulative = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                cumulative += weights[i];
                if (value < cumulative)
                    return i;
            }
            return weights.Last();
        }
       

    }
}