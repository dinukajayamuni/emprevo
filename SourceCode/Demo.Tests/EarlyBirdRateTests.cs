using System;
using System.Collections.Generic;
using Demo.Rates.FlatRates;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class EarlyBirdRateTests
    {
        private EarlyBirdRate _rate;
        private bool _result;
        private decimal _total;

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDates))]
        public void EarlyBirdRateConditionsShouldMeetForEntryAndExiteDates(DateTime entryDateTime,
            DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfAnEarlyBirdRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDates))]
        public void EarlyBirdRateConditionsShouldNotMeetForEntryAndExiteDates(DateTime entryDateTime,
            DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfAnEarlyBirdRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        private void AnInstanceOfAnEarlyBirdRate()
        {
            _rate = new EarlyBirdRate();
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
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 15, 30, 00)},
                new object[] {new DateTime(2018, 03, 22, 09, 00, 00), new DateTime(2018, 03, 22, 23, 30, 00)},
                new object[] {new DateTime(2018, 03, 22, 07, 32, 45), new DateTime(2018, 03, 22, 18, 12, 54)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDates()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 05, 59, 00), new DateTime(2018, 03, 22, 23, 30, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 23, 00, 00, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 01), new DateTime(2018, 03, 22, 12, 30, 59)},
                new object[] {new DateTime(2018, 03, 22, 09, 00, 00), new DateTime(2018, 03, 23, 23, 30, 00)},
                new object[] {new DateTime(2018, 03, 22, 09, 00, 00), new DateTime(2019, 03, 22, 23, 30, 00)},
                new object[] {new DateTime(2018, 03, 24, 09, 00, 00), new DateTime(2018, 03, 24, 23, 30, 00)}
            };
        }

        [Fact]
        public void EarlyBirdRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfAnEarlyBirdRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(13.00m))
                .BDDfy();
        }
    }
}