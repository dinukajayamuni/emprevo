using System;

namespace Demo.Rates.HourlyRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a Three Plus Hour rate
    /// </summary>
    internal class ThreePlusHourRate : HourlyRate
    {
        public ThreePlusHourRate() : base(20.00m, 3, null)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Calculates the total price. $20.00 flat rate for each calendar day between the entry and the exit date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the total.</returns>
        public override decimal CalculateTotal(DateTime entryDateTime, DateTime exitDateTime)
        {
            var duration = exitDateTime - entryDateTime;
            return (int) Math.Ceiling(duration.TotalDays) * Rate;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the duration is greater than 3 hours between the  given exit and entry date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var duration = exitDateTime - entryDateTime;
            return duration > TimeSpan.FromHours(StartHour);
        }
    }
}