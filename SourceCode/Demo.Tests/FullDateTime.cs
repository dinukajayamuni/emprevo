using System;

namespace Demo.Tests
{
    /// <summary>
    /// This class is used override the date time format when BDDfy renders the scenario
    /// </summary>
    public class FullDateTime
    {
        public FullDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            DateTime = new DateTime(year, month, day, hour, minute, second);
        }

        private DateTime DateTime { get; }

        public override string ToString()
        {
            return DateTime.ToString("f");
        }

        public static implicit operator DateTime(FullDateTime d)
        {
            return d.DateTime;
        }

        public static explicit operator FullDateTime(DateTime d)
        {
            return new FullDateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }
    }
}