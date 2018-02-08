using System;
using System.Collections.Generic;
using Demo.Rates.HourlyRates;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class HourlyRateTests
    {
        private HourlyRate _rate;
        private bool _result;
        private decimal _total;

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDatesForZeroToOneHourRate))]
        public void ZeroToOneHourRateConditionsShouldMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAZeroToOneHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    exitDate.DayOfWeek))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDatesForZeroToOneHourRate))]
        public void ZeroToOneHourRateConditionsShouldNotMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAZeroToOneHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    entryDate.DayOfWeek))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDatesForOneToTwoHourRate))]
        public void OneToTwoHourRateConditionsShouldMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAOneToTwoHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    exitDate.DayOfWeek))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDatesForOneToTwoHourRate))]
        public void OneToTwoHourRateConditionsShouldNotMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAOneToTwoHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    entryDate.DayOfWeek))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDatesForTwoToThreeHourRate))]
        public void TwoToThreeHourRateConditionsShouldMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfATwoToThreeHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    exitDate.DayOfWeek))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDatesForTwoToThreeHourRate))]
        public void TwoToThreeHourRateConditionsShouldNotMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfATwoToThreeHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    entryDate.DayOfWeek))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(InRangeEntryAndExitDatesForThreePlusHourRate))]
        public void ThreePlusHourRateConditionsShouldMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAThreePlusHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    exitDate.DayOfWeek))
                .Then(s => s.TheConditionsShoudMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(OutOfRangeEntryAndExitDatesForThreePlusHourRate))]
        public void ThreePlusHourRateConditionsShouldNotMeetForEntryAndExiteDate(DateTime entryDate, DateTime exitDate)
        {
            this.Given(s => s.AnInstanceOfAThreePlusHourRate())
                .When(s => s.TheConditionsAreEvaluatedWithEntryAndExitDates(entryDate, entryDate.DayOfWeek, exitDate,
                    entryDate.DayOfWeek))
                .Then(s => s.TheConditionsShouldNotMeet())
                .BDDfy();
        }

        [Theory]
        [MemberData(nameof(InRangeExpectedPriceEntryAndExitDatesForThreePlusHourRate))]
        public void ThreeOurplusRateTotalPriceIsNotFlat(DateTime entryDate, DateTime exitDate, decimal expectedPrice)
        {
            this.Given(s => s.AnInstanceOfAThreePlusHourRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(entryDate, exitDate))
                .Then(s => s.TheTotalCostIs(expectedPrice))
                .BDDfy();
        }

        private void AnInstanceOfAZeroToOneHourRate()
        {
            _rate = new ZeroToOneHourRate();
        }

        private void AnInstanceOfAOneToTwoHourRate()
        {
            _rate = new OneToTwoOneHourRate();
        }

        private void AnInstanceOfATwoToThreeHourRate()
        {
            _rate = new TwoToThreeHourRate();
        }

        private void AnInstanceOfAThreePlusHourRate()
        {
            _rate = new ThreePlusHourRate();
        }

        private void TheConditionsAreEvaluatedWithEntryAndExitDates(DateTime entryDate, DayOfWeek entryDayOfWeek,
            DateTime exitDate, DayOfWeek exitDayOfWeek)
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

        public static IEnumerable<object[]> InRangeEntryAndExitDatesForZeroToOneHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 06, 00, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 06, 59, 59)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDatesForZeroToOneHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 07, 00, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2019, 03, 22, 06, 59, 59)}
            };
        }

        public static IEnumerable<object[]> InRangeEntryAndExitDatesForOneToTwoHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 07, 59, 59)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDatesForOneToTwoHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 08, 00, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2019, 03, 22, 07, 59, 59)}
            };
        }

        public static IEnumerable<object[]> InRangeEntryAndExitDatesForTwoToThreeHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 08, 59, 59)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 00, 00)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDatesForTwoToThreeHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2019, 03, 22, 08, 59, 59)}
            };
        }

        public static IEnumerable<object[]> InRangeEntryAndExitDatesForThreePlusHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 01, 00)},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2019, 03, 22, 08, 59, 59)}
            };
        }

        public static IEnumerable<object[]> OutOfRangeEntryAndExitDatesForThreePlusHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 00, 00)}
            };
        }

        public static IEnumerable<object[]> InRangeExpectedPriceEntryAndExitDatesForThreePlusHourRate()
        {
            return new[]
            {
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 08, 01, 00), 20.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 22, 09, 01, 00), 20.00m},
                new object[] {new DateTime(2018, 03, 22, 06, 00, 00), new DateTime(2018, 03, 23, 08, 59, 59), 40.00m}
            };
        }

        [Fact]
        public void OneToTwoHourRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfAOneToTwoHourRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(10.00m))
                .BDDfy();
        }

        [Fact]
        public void TwoToThreeHourRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfATwoToThreeHourRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(15.00m))
                .BDDfy();
        }

        [Fact]
        public void ZeroToOneHourRateTotalPriceIsFlat()
        {
            this.Given(s => s.AnInstanceOfAZeroToOneHourRate())
                .When(s => s.TheTotalIsCalculatedForEntryAndExitDates(DateTime.MinValue, DateTime.MaxValue))
                .Then(s => s.TheTotalCostIs(5.00m))
                .BDDfy();
        }
    }
}