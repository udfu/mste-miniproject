using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        
        [Fact]
        public void ScenarioOkay01Test()
        {
            Assert.True(Target.IsCarAvailable(2, new DateTime(2018, 12, 20), new DateTime(2018, 12, 25)));
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Assert.True(Target.IsCarAvailable(2, new DateTime(2020, 06, 19), new DateTime(2020, 06, 25)));
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            Assert.True(Target.IsCarAvailable(2, new DateTime(2020, 04, 19), new DateTime(2020, 05, 19)));
        }

        [Fact]
        public void ScenarioOkay04Test()
        {
            Assert.True(Target.IsCarAvailable(2, new DateTime(2022, 12, 20), new DateTime(2022, 12, 25)));
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Assert.False(Target.IsCarAvailable(2, new DateTime(2020, 06, 18), new DateTime(2020, 06, 25)));
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Assert.False(Target.IsCarAvailable(2, new DateTime(2020, 05, 16), new DateTime(2020, 05, 20)));
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Assert.False(Target.IsCarAvailable(2, new DateTime(2020, 05, 16), new DateTime(2020, 06, 20)));
        }

        [Fact]
        public void ScenarioNotOkay04Test()
        {
            Assert.False(Target.IsCarAvailable(2, new DateTime(2020, 01, 02), new DateTime(2020, 05, 20)));
        }

        [Fact]
        public void ScenarioNotOkay05Test()
        {
            Assert.False(Target.IsCarAvailable(2, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20)));
        }
    }
}
