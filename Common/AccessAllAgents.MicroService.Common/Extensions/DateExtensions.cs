using System;

namespace AccessAllAgents.MicroService.Common.Extensions
{
    public static class DateExtensions
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1);

        public static int ToInt(this DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            return year * 10000 + month * 100 + day;
        }

        public static DateTime FromInt(this int date)
        {
            int year = date / 10000;
            int month = (date - (year * 10000)) / 100;
            int day = date - (year * 10000) - (month * 100);

            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static long ToTimestamp(this DateTime dt)
        {
            return (long) dt.Subtract(_epoch).TotalMilliseconds;
        }

        public static DateTime ToDateTime(this long ts)
        {
            return _epoch.AddMilliseconds(ts);
        }
    }
}
