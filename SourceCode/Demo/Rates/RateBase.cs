using System;

namespace Demo.Rates
{
    /// <summary>
    ///     This class represents the base rate attributes and functionality
    /// </summary>
    internal abstract class RateBase
    {
        protected RateBase(string name, RateType type, decimal rate)
        {
            Name = name;
            Type = type;
            Rate = rate;
        }

        /// <summary>
        ///     The name of the rate
        ///     <example>Early Bird</example>
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The type of rate
        ///     <value>Flat/Hourly</value>
        /// </summary>
        public RateType Type { get; }

        /// <summary>
        ///     The amount of the rate
        /// </summary>
        public decimal Rate { get; }

        /// <summary>
        ///     Checks whether the entry condition is met given an entry date time
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <returns>Returns true if the entry condition is met</returns>
        protected abstract bool EntryCondition(DateTime entryDateTime);

        /// <summary>
        ///     Checks whether the exit condition is met given the entry and exit date times
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns true if the exit condition is met</returns>
        protected abstract bool ExitCondition(DateTime entryDateTime, DateTime exitDateTime);

        /// <summary>
        ///     Calculates the total price.
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>Returns the total price</returns>
        public abstract decimal CalculateTotal(DateTime entryDateTime, DateTime exitDateTime);

        /// <summary>
        ///     Checkes whether the entry and the exit conditions are met given the entry and exit dates
        /// </summary>
        /// <param name="entryDateTime">The entry date time</param>
        /// <param name="exitDateTime">The exit date time</param>
        /// <returns>
        ///     Returns false if the entry date time is later than the exit date time.
        ///     Retruns true if the entry and the exit conditions are met.
        /// </returns>
        public bool IsMatch(DateTime entryDateTime, DateTime exitDateTime)
        {
            return entryDateTime <= exitDateTime && EntryCondition(entryDateTime) &&
                   ExitCondition(entryDateTime, exitDateTime);
        }
    }
}