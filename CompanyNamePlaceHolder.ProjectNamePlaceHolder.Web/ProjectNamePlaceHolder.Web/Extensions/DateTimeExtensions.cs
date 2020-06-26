using System;
namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return time.ToTimeZoneTime(tzi);
        }

        public static DateTime ToPhilippineTimeZoneTime(this DateTime time)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Philippine Standard Time");
            return time.ToTimeZoneTime(tzi);
        }

        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }
    }
}
