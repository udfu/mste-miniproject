using System;
using System.Collections.Generic;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void GetAutosTest()
        {
            List<Auto> allAutos = target.GetAutos();
            Assert.Equal(4, allAutos.Count);
        }

        [Fact]
        public void GetAutoByIdTest()
        {
            Auto auto = new LuxusklasseAuto {Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50};
            Assert.Equal(auto, Target.GetAutoById(3));
        }

        [Fact]
        public void AddAutoTest()
        {

        }

        [Fact]
        public void UpdateAutoTest()
        {

        }

        [Fact]
        public void DeleteAutoTest()
        {

        }

    }
}