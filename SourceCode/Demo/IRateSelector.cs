using System;
using Demo.Rates;

namespace Demo
{
    /// <summary>
    ///     The rate selector interface
    /// </summary>
    internal interface IRateSelector
    {
        /// <summary>
        ///     Selects rate for the given entry and exit times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the rate</returns>
        RateBase Select(DateTime entryDateTime, DateTime exitDateTime);
    }
}