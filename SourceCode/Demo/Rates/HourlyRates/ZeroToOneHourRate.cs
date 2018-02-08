namespace Demo.Rates.HourlyRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a Zero to One Hour rate
    /// </summary>
    internal class ZeroToOneHourRate : HourlyRate
    {
        public ZeroToOneHourRate() : base(5.00m, 0, 1)
        {
        }
    }
}