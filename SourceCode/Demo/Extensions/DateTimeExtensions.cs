using System;

namespace Demo.Extensions
{
    internal static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime dateTime)
        {
            var day = dateTime.DayOfWeek;
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        public static bool IsOnSameDay(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.Year == otherDateTime.Year &&
                   dateTime.DayOfYear == otherDateTime.DayOfYear;
        }

        public static bool IsOnNextDay(this DateTime dateTime, DateTime otherDateTime)
        {
            return dateTime.Year == otherDateTime.Year &&
                   dateTime.DayOfYear + 1 == otherDateTime.DayOfYear;
        }

        public static bool IsOnSameWeek(this DateTime dateTime, DateTime otherDateTime)
        {
            var duration = otherDateTime - dateTime;
            return duration.Days <= 7;
        }
    }
}