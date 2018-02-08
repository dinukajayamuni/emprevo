using System;
using Demo.Extensions;

namespace Demo.Rates.FlatRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of the Weekend rate
    /// </summary>
    internal class WeekendRate : FlatRate
    {
        private const decimal TotalPrice = 10.00m;
        private static readonly TimeSpan ExitTimeEnd = new TimeSpan(23, 59, 59);

        public WeekendRate() : base("Weekend Rate", TotalPrice)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the given entry date is anytime on Saturday or Sunday
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <returns>Returns true if the entry condition is met</returns>
        protected override bool EntryCondition(DateTime entryDateTime)
        {
            return entryDateTime.IsWeekend();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the given entry and exit date times are on weekend and
        ///     the exit date time is any time before midnight of Sunday
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var time = exitDateTime.TimeOfDay;
            var day = exitDateTime.DayOfWeek;

            return entryDateTime.IsOnSameWeek(exitDateTime) && (day == DayOfWeek.Saturday ||
                                                                day == DayOfWeek.Sunday) && time <= ExitTimeEnd;
        }
    }
}