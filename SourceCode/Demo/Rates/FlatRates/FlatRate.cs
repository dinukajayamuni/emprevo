using System;

namespace Demo.Rates.FlatRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a flat rate
    /// </summary>
    internal abstract class FlatRate : RateBase
    {
        protected FlatRate(string name, decimal rate) : base(name, RateType.Flat, rate)
        {
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
    }
}