using System;
using System.Collections.Generic;
using Demo.Rates.FlatRates;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class WeekendRateTests
    {
        private WeekendRate _rate;
        private bool _result;
        private decimal _total;

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDates))]
        public void WeekendRateConditionsShouldMeetForEntryAndExiteDates(DateTime entryDateTime, DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfAWeekendRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDates))]
        public void WeekendRateConditionsShouldNotMeetForEntryAndExiteDates(DateTime entryDateTime, DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfAWeekendRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        private void AnInstanceOfAWeekendRate()
        {
            _rate = new WeekendRate();
        }

        private void TheConditionsAreEvaluatedWithEntryAndExitDates(DateTime entryDate, DateTime exitDate)
        {
            _result = _rate.IsMatch(entryDate, exitDate);
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
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2018, 03, 25, 23, 59, 59)},
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2018, 03, 24, 23, 59, 59)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDates()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 23, 23, 59, 59), new DateTime(2018, 03, 25, 23, 59, 59)},
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2018, 03, 26, 00, 00, 00)},
                new object[] {new DateTime(2018, 03, 24, 00, 00, 00), new DateTime(2019, 03, 25, 23, 59, 59)},
                new object[] {new DateTime(2018, 03, 17, 00, 00, 00), new DateTime(2018, 04, 29, 23, 59, 59)}
            };
        }

        [Fact]
        public void WeekendRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfAWeekendRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(10.00m))
                .BDDfy();
        }
    }
}