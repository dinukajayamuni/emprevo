using System;
using Demo.Extensions;

namespace Demo.Rates.FlatRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of the Night rate
    /// </summary>
    internal class NightRate : FlatRate
    {
        private const decimal TotalPrice = 6.50m;
        private static readonly TimeSpan EntryTimeStart = new TimeSpan(18, 00, 00);
        private static readonly TimeSpan EntryTimeEnd = new TimeSpan(1, 00, 00, 00);
        private static readonly TimeSpan ExitTimeStart = new TimeSpan(00, 00, 00);
        private static readonly TimeSpan ExitTimeEnd = new TimeSpan(06, 00, 00);

        public NightRate() : base("Night Rate", TotalPrice)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the given entry date is between 6:00 PM to midnight on week days
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <returns>Returns true if the entry condition is met</returns>
        protected override bool EntryCondition(DateTime entryDateTime)
        {
            var time = entryDateTime.TimeOfDay;
            return !entryDateTime.IsWeekend() && time >= EntryTimeStart && time <= EntryTimeEnd;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the given exit date time is on the next day and
        ///     the exit date time is before 6:00 AM
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var time = exitDateTime.TimeOfDay;
            return !exitDateTime.IsWeekend() && entryDateTime.IsOnNextDay(exitDateTime) && time >= ExitTimeStart &&
                   time < ExitTimeEnd;
        }
    }
}