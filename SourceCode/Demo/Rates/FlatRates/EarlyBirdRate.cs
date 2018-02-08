using System;
using Demo.Extensions;

namespace Demo.Rates.FlatRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of the Early Bird rate
    /// </summary>
    internal class EarlyBirdRate : FlatRate
    {
        private const decimal TotalPrice = 13.00m;
        private static readonly TimeSpan EntryTimeStart = new TimeSpan(06, 0, 0);
        private static readonly TimeSpan EntryTimeEnd = new TimeSpan(09, 0, 0);
        private static readonly TimeSpan ExitTimeStart = new TimeSpan(15, 30, 0);
        private static readonly TimeSpan ExitTimeEnd = new TimeSpan(23, 30, 0);

        public EarlyBirdRate() : base("Early Bird", TotalPrice)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the given entry date is between 6:00 AM to 9:00 AM on week days
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
        ///     Checks whether the given entry and exit date times are on the same day and
        ///     the exit date time is between 3:30 PM to 11:30 PM
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var time = exitDateTime.TimeOfDay;
            return !exitDateTime.IsWeekend() && entryDateTime.IsOnSameDay(exitDateTime) &&
                   time >= ExitTimeStart && time <= ExitTimeEnd;
        }
    }
}