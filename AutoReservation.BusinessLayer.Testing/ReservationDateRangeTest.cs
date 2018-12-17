using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void ScenarioOkay01Test()
        {
            Assert.True(Target.DateRangeCheck(new DateTime(2020, 01, 10), new DateTime(2020, 01, 20)));
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Assert.True(Target.DateRangeCheck(new DateTime(2018, 12, 20), new DateTime(2018, 12, 21)));
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Assert.Throws<InvalidDateRangeException>(() => Target.DateRangeCheck(new DateTime(2020, 01, 20), new DateTime(2020, 01, 10)));
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Assert.Throws<InvalidDateRangeException>(() => Target.DateRangeCheck(new DateTime(2020, 01, 20), new DateTime(2020, 01, 20)));
        }
    }
}
