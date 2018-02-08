using System;

namespace Demo.Rates.HourlyRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a Two to Three Hour rate
    /// </summary>
    internal class TwoToThreeHourRate : HourlyRate
    {
        public TwoToThreeHourRate() : base(15.00m, 2, 3)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the duration is greater than or equals to 2 hours
        ///     and less than or equals to 3 hours between the  given exit and entry date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var duration = exitDateTime - entryDateTime;
            return duration >= TimeSpan.FromHours(StartHour) &&
                   (EndHour == null || duration <= TimeSpan.FromHours(EndHour.Value));
        }
    }
}