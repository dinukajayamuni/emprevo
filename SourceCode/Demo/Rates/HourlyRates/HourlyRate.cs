using System;

namespace Demo.Rates.HourlyRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a houlry rate
    /// </summary>
    internal abstract class HourlyRate : RateBase
    {
        protected HourlyRate(decimal rate, int startHour, int? endHour) : base(CreateName(startHour, endHour),
            RateType.Hourly, rate)
        {
            StartHour = startHour;
            EndHour = endHour;
        }

        /// <summary>
        ///     Gets the upper boundary of the condition (in hours)
        /// </summary>
        protected int? EndHour { get; }

        /// <summary>
        ///     Gets the lower boundary of the condition (in hours)
        /// </summary>
        protected int StartHour { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Always true
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <returns>Returns true</returns>
        protected override bool EntryCondition(DateTime entryDateTime)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Checks whether the duration is greater than or equals to lower boundry
        ///     and less than the upper boundary between the given entry and exit date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected override bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime)
        {
            var duration = exitDateTime - entryDateTime;
            return duration >= TimeSpan.FromHours(StartHour) &&
                   (EndHour == null || duration < TimeSpan.FromHours(EndHour.Value));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Calculates the total price.
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns rate as the total price for entry and exit date.</returns>
        public override decimal CalculateTotal(DateTime entryDateTime, DateTime exitDateTime)
        {
            return Rate;
        }

        private static string CreateName(int start, int? end)
        {
            return end != null ? $"{start} - {end} hours" : $"{start} + hours";
        }
    }
}