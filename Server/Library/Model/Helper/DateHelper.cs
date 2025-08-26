using MongoDB.Bson;
using System;

namespace ETModel
{
    /// <summary>
    /// 日期相關的Helper
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// seconds 轉 UTC DateTime
        /// </summary>
        /// <param name="second">傳入自 1970-01-01T00:00:00.000Z 以來所經過的秒數</param>
        /// <returns></returns>
        public static DateTime TimestampSecondToDateTimeUTC(long second)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(second);
        }
        /// <summary>
        /// milliseconds 轉 UTC DateTime
        /// </summary>
        /// <param name="milliseconds">傳入自 1970-01-01T00:00:00.000Z 以來所經過的毫秒數</param>
        /// <returns></returns>
        public static DateTime TimestampMillisecondToDateTimeUTC(long milliseconds)
        {

            return new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds).UtcDateTime;
        }
        /// <summary>
        /// milliseconds 轉 Local DateTime
        /// </summary>
        /// <param name="milliseconds">傳入自 1970-01-01T00:00:00.000Z 以來所經過的毫秒數。</param>
        /// <returns></returns>
        public static DateTime TimestampMillisecondToDateTimeLocal(long milliseconds)
        {
            return new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds).ToLocalTime().DateTime;
        }
        /// <summary>
        /// 使用指定的 DateTime 值 轉成 millisecond
        /// </summary>
        /// <param name="dateTime">日期和時間。</param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
        /// <summary>
        /// 使用指定的 DateTime 值 轉成 millisecond(UTC時間)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUTCUnixTimeMilliseconds(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeMilliseconds();
        }
        /// <summary>
        /// 取得玩家登入天數
        /// </summary>
        /// <param name="milliseconds">最後登入時間</param>
        /// <param name="originDay">原始登入天數</param>
        /// <returns></returns>
        public static int GetLoginDay(long milliseconds, int originDay)
        {
            // 新玩家
            if (milliseconds <= 0)
            {
                return originDay + 1;
            }

            var startDate = TimestampMillisecondToDateTimeUTC(milliseconds);
            var endDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0).ToUniversalTime().AddDays(1);//最後登入當天UTC 16:0:0
            var nowDate = TimestampMillisecondToDateTimeUTC(TimeHelper.NowTimeMillisecond());
            var isToday = (nowDate > startDate) && (nowDate < endDate);
            if (isToday)// 少於一天，還沒到12點更新
            {
                return originDay;
            }

            var span = nowDate - startDate;
            var day = span.TotalDays;
            if (day > 1)// 大於一天，歸零
            {
                return 0;
            }

            return originDay + 1;
        }
        /// <summary>
        /// 取得玩家累積天數
        /// </summary>
        /// <param name="milliseconds">最後目前登入時間</param>
        /// <param name="originDay">原始累積天數</param>
        /// <returns></returns>
        public static int GetStackDay(long milliseconds, int originDay)
        {
            // 新玩家
            if (milliseconds <= 0)
            {
                return originDay + 1;
            }

            var startDate = TimestampMillisecondToDateTimeUTC(milliseconds);
            var nowDate = TimestampMillisecondToDateTimeUTC(TimeHelper.NowTimeMillisecond());

            var span = nowDate - startDate;
            var day = span.TotalDays;
            if (day > 1f) // 超過一天登入
            {
                return originDay + 1;
            }
            else
            {
                var endDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0).ToUniversalTime().AddDays(1);//最後登入當天UTC 16:0:0
                var isToday = (nowDate > startDate) && (nowDate < endDate);
                if (isToday)// 少於一天，還沒到12點更新
                {
                    return originDay;
                }
                else // 12點更新
                {
                    return originDay + 1;
                }
            }
        }
    }
}
