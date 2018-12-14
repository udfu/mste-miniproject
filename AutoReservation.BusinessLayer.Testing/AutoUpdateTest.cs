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
            Assert.Equal(4, Target.GetAutos().Count);
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
            LuxusklasseAuto auto = new LuxusklasseAuto();

            auto.Marke = "Porsche";
            auto.Tagestarif = 300;
            auto.Basistarif = 400;

            Target.AddAuto(auto);

            Assert.Equal(5, Target.GetAutos().Count);
        }

        [Fact]
        public void UpdateAutoTest()
        {
            Auto auto = Target.GetAutoById(3);

            auto.Marke = "Porsche";
            Target.UpdateAuto(auto);

            Assert.Equal(auto, Target.GetAutoById(3));
        }

        [Fact]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(1);
            Assert.Equal(3, Target.GetAutos().Count);
        }

    }
}