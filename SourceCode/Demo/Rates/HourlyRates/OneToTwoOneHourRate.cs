namespace Demo.Rates.HourlyRates
{
    /// <inheritdoc />
    /// <summary>
    ///     This class represents attributes and functionality of a One to Two Hour rate
    /// </summary>
    internal class OneToTwoOneHourRate : HourlyRate
    {
        public OneToTwoOneHourRate() : base(10.00m, 1, 2)
        {
        }
    }
}