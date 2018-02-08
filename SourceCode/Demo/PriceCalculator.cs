using System;

namespace Demo
{
    /// <inheritdoc />
    /// <summary>
    ///     This class includes the price calculation functionality
    /// </summary>
    internal class PriceCalculator : IPriceCalculator
    {
        private readonly IRateSelector _rateSelector;

        public PriceCalculator(IRateSelector rateSelector)
        {
            _rateSelector = rateSelector;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Selects the rate for the given entry and exit date times and caluates the total price
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the total price</returns>
        public Price Calculate(DateTime entryDateTime, DateTime exitDateTime)
        {
            var rate = _rateSelector.Select(entryDateTime, exitDateTime);
            var total = rate.CalculateTotal(entryDateTime, exitDateTime);
            return new Price(rate.Name, rate.Rate, total);
        }
    }
}