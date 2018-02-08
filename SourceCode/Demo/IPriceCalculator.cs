using System;

namespace Demo
{
    /// <summary>
    ///     The price calculator interface
    /// </summary>
    internal interface IPriceCalculator
    {
        /// <summary>
        ///     Calculates total price for the given entry and exit date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the total price</returns>
        Price Calculate(DateTime entryDateTime, DateTime exitDateTime);
    }
}