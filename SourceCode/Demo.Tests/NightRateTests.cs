using System;
using System.Collections.Generic;
using Demo.Rates.FlatRates;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class NightRateTests
    {
        private NightRate _rate;
        private bool _result;

        private decimal _total;

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDates))]
        public void NightRateConditionsShouldMeetForEntryAndExiteDates(DateTime entryDateTime,
            DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfANightRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDates))]
        public void NightRateConditionsShouldNotMeetForEntryAndExiteDates(DateTime entryDateTime,
            DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfANightRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        private void AnInstanceOfANightRate()
        {
            _rate = new NightRate();
        }

        private void TheConditionsAreEvaluatedWithEntryAndExitDates(DateTime entryDateTime,
            DateTime exitDateTime)
        {
            _result = _rate.IsMatch(entryDateTime, exitDateTime);
        }

        private void TheTotalIsCalculatedForEntryAndExitDates(DateTime entryDate, DateTime exitDate)
        {
            _total = _rate.CalculateTotal(entryDate, exitDate);
        }

        private void TheTotalCostIs(decimal total)
        {
            Assert.Equal(total, _total);
        }

        private void TheConditionsShoudMeet()
        {
            Assert.True(_result);
        }

        private void TheConditionsShouldNotMeet()
        {
            Assert.False(_result);
        }

        public static IEnumerable<object[]> InRangeEntryAndExitDates()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 18, 00, 00), new DateTime(2018, 03, 23, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 21, 18, 00, 00), new DateTime(2018, 03, 22, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 22, 18, 00, 00), new DateTime(2018, 03, 23, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 22, 18, 01, 00), new DateTime(2018, 03, 23, 05, 59, 59)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDates()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 17, 59, 59), new DateTime(2018, 03, 23, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 22, 18, 00, 00), new DateTime(2018, 03, 23, 06, 00, 00)},
                new object[] {new DateTime(2018, 03, 24, 18, 00, 00), new DateTime(2018, 03, 25, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 24, 18, 00, 00), new DateTime(2019, 03, 25, 05, 59, 59)},
                new object[] {new DateTime(2018, 03, 23, 18, 00, 00), new DateTime(2018, 03, 24, 05, 59, 59)}
            };
        }

        [Fact]
        public void NightRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfANightRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(6.50m))
                .BDDfy();
        }
    }
}