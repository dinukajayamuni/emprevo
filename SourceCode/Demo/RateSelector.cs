using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Rates;

namespace Demo
{
    /// <summary>
    ///     y
    ///     This class inculdes the rate selection functionality
    /// </summary>
    internal class RateSelector : IRateSelector
    {
        private readonly IEnumerable<RateBase> _flatRates;
        private readonly IEnumerable<RateBase> _hourlyRates;

        public RateSelector(IEnumerable<RateBase> rates)
        {
            var ratesList = rates.ToList();
            _flatRates = ratesList.Where(r => r.Type == RateType.Flat);
            _hourlyRates = ratesList.Where(r => r.Type == RateType.Hourly);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Tries to select a Flat rate first for the given entry and exit date times
        ///     If not found then selects a Hourly rate
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the rate. Returned rate cannot be null</returns>
        public RateBase Select(DateTime entryDateTime, DateTime exitDateTime)
        {
            var flatRate =
                _flatRates.SingleOrDefault(r => r.IsMatch(entryDateTime, exitDateTime));
            if (flatRate != null) return flatRate;
            var hourlyRate =
                _hourlyRates.Single(r => r.IsMatch(entryDateTime, exitDateTime));
            return hourlyRate;
        }
    }
}