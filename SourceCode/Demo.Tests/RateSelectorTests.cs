using System;
using System.Collections.Generic;
using Autofac;
using Demo.Rates;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Tests
{
    public class RateSelectorTests
    {
        private RateBase _rate;
        private IRateSelector _rateSelector;

        [Theory]
        [MemberData(nameof(Data))]
        public void AtleastOnRateIsSelectedForEntryAndExitDates(DateTime entryDateTime, DateTime exitDateTime)
        {
            this.Given(s => s.AnInstanceOfRateSelector())
                .When(s => s.ARateIsSelectedForEntryAndExitDates(entryDateTime, exitDateTime))
                .Then(s => s.AtleatOneRateIsSelected())
                .BDDfy();
        }

        private void AnInstanceOfRateSelector()
        {
            var container = Bootstrapper.Build();
            _rateSelector = container.Resolve<IRateSelector>();
        }

        private void ARateIsSelectedForEntryAndExitDates(DateTime entryDateTime, DateTime exitDateTime)
        {
            try
            {
                _rate = _rateSelector.Select(entryDateTime, exitDateTime);
            }
            catch (Exception)
            {
                _rate = null;
            }
        }

        private void AtleatOneRateIsSelected()
        {
            Assert.NotNull(_rate);
        }

        public static IEnumerable<object[]> Data()
        {
            var dates = new List<object[]>();
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var entryDateTime = RandomDay(random);
                var exitDateTime = RandomDay(random, entryDateTime);
                dates.Add(new object[] {entryDateTime, exitDateTime});
            }
            return dates;
        }

        private static DateTime RandomDay(Random gen, DateTime? start = null)
        {
            if (start == null) start = new DateTime(2018, 03, 22, 15, 00, 00);
            return start.Value.AddHours(gen.Next(72));
        }
    }
}