using System;
using System.Collections.Generic;
using Autofac;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class PriceCalculatorTests
    {
        private IPriceCalculator _calculator;
        private Price _price;

        [Theory]
        [MemberData(nameof(Data))]
        public void PriceCalculatedCorrectlyForEntryAndExiteDates(DateTime entryDateTime, DateTime exitDateTime,
            decimal totalPrice)
        {
            this.Given(s => s.AnInstanceOfAPriceCalculator())
                .When(s => s.ThePriceIsCalculatedForEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheTotalPriceShouldBe(totalPrice))
                .BDDfy();
        }

        private void AnInstanceOfAPriceCalculator()
        {
            var container = Bootstrapper.Build();
            _calculator = container.Resolve<IPriceCalculator>();
        }

        private void ThePriceIsCalculatedForEntryAndExitDates(DateTime entryDateTime, DateTime exitDateTime)
        {
            _price = _calculator.Calculate(entryDateTime, exitDateTime);
        }

        private void TheTotalPriceShouldBe(decimal price)
        {
            Assert.Equal(price, _price.Total);
        }

        public static IEnumerable<object[]> Data()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 15, 30, 00), 13.00m},
                new object[] {new DateTime(2018, 03, 22, 09, 00, 00), new DateTime(2018, 03, 22, 23, 30, 00), 13.00m},
                new object[] {new DateTime(2018, 03, 22, 07, 32, 45), new DateTime(2018, 03, 22, 18, 12, 54), 13.00m},
                new object[] {new DateTime(2018, 03, 22, 18, 00, 00), new DateTime(2018, 03, 23, 05, 59, 59), 6.50m},
                new object[] {new DateTime(2018, 03, 21, 18, 00, 00), new DateTime(2018, 03, 22, 05, 59, 59), 6.50m},
                new object[] {new DateTime(2018, 03, 22, 18, 00, 00), new DateTime(2018, 03, 23, 05, 59, 59), 6.50m},
                new object[] {new DateTime(2018, 03, 22, 18, 01, 00), new DateTime(2018, 03, 23, 05, 59, 59), 6.50m},
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2018, 03, 25, 23, 59, 59), 10.00m},
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2018, 03, 24, 23, 59, 59), 10.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 06, 00, 00), 5.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 06, 59, 59), 5.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 07, 59, 59), 10.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 08, 59, 59), 15.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 00, 00), 15.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 08, 01, 00), 15.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 01, 00), 20.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 23, 08, 59, 59), 40.00m}
            };
        }
    }
}