using System;
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
            Assert.True(Target.DateRangeCheck(DateTime.Today, DateTime.Today.AddDays(2)));
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            throw new NotImplementedException("Test not implemented.");
        }
    }
}
