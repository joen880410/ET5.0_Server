using System;
using System.Text.RegularExpressions;

namespace ETModel
{
    /// <summary>
    /// 時間相關的Helper
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// Cronq排程的時間類別
        /// </summary>
        public enum CronTimeType
        {
            /// <summary>
            /// 年
            /// </summary>
            Year,
            /// <summary>
            /// 月
            /// </summary>
            Month,
            /// <summary>
            /// 日
            /// </summary>
            Day,
            /// <summary>
            /// 時
            /// </summary>
            Hour,
            /// <summary>
            /// 分
            /// </summary>
            Min
        }
        /// <summary>
        /// 自 1970-01-01T00:00:00.000Z 以來所經過的刻度數目
        /// </summary>
        public static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        /// <summary>
        /// 日期檢查(YYYY,MM,DD,HH,mm)
        /// </summary>
        public static readonly Regex dateRegex = new Regex(@"([0-9]\d{3},(0[1-9]|1[0-2]),(0[1-9]|[12]\d|3[01])),([0-1][0-9]|2[0-3]),([0-5][0-9])");
        /// <summary>
        /// 客户端时间(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ClientNowMilliSeconds()
        {
            return (DateTime.UtcNow.Ticks - epoch) / 10000;
        }

        /// <summary>
        /// UTC時間(秒)
        /// </summary>
        /// <returns></returns>
        public static long ClientNowSeconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
        /// <summary>
        /// 現在過後多少豪秒
        /// </summary>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static long NowAfterTimeMillisecond(long millisecond)
        {
            return ClientNowMilliSeconds() + millisecond;
        }
        /// <summary>
        /// 現在過後多少秒
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static long NowAfterTimeSeconds(double second)
        {
            return NowAfterTimeMillisecond((long)(second * 1000));
        }
        /// <summary>
        /// Tick 轉成 Seconds
        /// </summary>
        /// <param name="tick">刻度的指定數目</param>
        /// <returns></returns>
        public static long TickConvertToSeconds(long tick)
        {
            return (long)(TickToMillSeconds(tick) * 0.001f);
        }
        /// <summary>
        /// Tick 轉成 Milliseconds(但不是從UNIX 時間等於 0 的時間點開始算)
        /// </summary>
        /// <param name="tick">刻度的指定數目</param>
        /// <returns></returns>
        public static long TickToMillSeconds(long tick)
        {
            return (long)new TimeSpan(tick).TotalMilliseconds;
        }
        /// <summary>
        /// 現在时间(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long NowTimeMillisecond()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// string[] 轉成日期和時間
        /// </summary>
        /// <param name="timeStrs">[年、月、日、時、分]</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string[] timeStrs)
        {
            var length = Enum.GetValues(typeof(CronTimeType)).Length;
            if (timeStrs.Length > length)
            {
                timeStrs[(int)CronTimeType.Year] = DateTimeOffset.UtcNow.DateTime.Year.ToString();
            }
            if (timeStrs.Length < length)
            {
                return DateTimeOffset.UtcNow.DateTime.ThisYearMaxTime();
            }
            var year = int.TryParse(timeStrs[(int)CronTimeType.Year], out var _year) ? _year : 0;
            var month = int.TryParse(timeStrs[(int)CronTimeType.Month], out var _month) ? _month : 0;
            var day = int.TryParse(timeStrs[(int)CronTimeType.Day], out var _day) ? _day : 0;
            var hour = int.TryParse(timeStrs[(int)CronTimeType.Hour], out var _hour) ? _hour : 0;
            var min = int.TryParse(timeStrs[(int)CronTimeType.Min], out var _min) ? _min : 0;
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, min, 0, DateTimeOffset.Now.Offset);
            return dateTimeOffset.UtcDateTime;
        }
        /// <summary>
        /// string[] 轉成日期和時間
        /// </summary>
        /// <param name="timeStrs">年,月,日,時,分</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string timeStrs)
        {
            return ConvertToDateTime(timeStrs.Split(','));
        }
        /// <summary>
        /// 取得日期時間當年得最後一秒
        /// </summary>
        /// <param name="dateTime">日期和時間。</param>
        /// <returns></returns>
        public static DateTime ThisYearMaxTime(this DateTime dateTime)
        {
            var maxDate = DateTime.MaxValue;
            return new DateTime(dateTime.Year, maxDate.Month, maxDate.Day, maxDate.Hour, maxDate.Minute, maxDate.Second);
        }
    }
}