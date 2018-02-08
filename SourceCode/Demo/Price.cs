namespace Demo
{
    /// <summary>
    ///     This class represents the total price and rate information
    /// </summary>
    internal class Price
    {
        public Price(string rateName, decimal rate, decimal total)
        {
            RateName = rateName;
            Rate = rate;
            Total = total;
        }

        /// <summary>
        ///     Gets the name of the rate used to calculate the total
        /// </summary>
        public string RateName { get; }

        /// <summary>
        ///     Gets the amount of the rate
        /// </summary>
        public decimal Rate { get; }

        /// <summary>
        ///     Gets the total price
        /// </summary>
        public decimal Total { get; }
    }
}