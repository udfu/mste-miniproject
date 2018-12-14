using System;
using System.Collections.Generic;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void GetKundenTest()
        {
            Assert.Equal(4, Target.GetKunden().Count);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde kunde = Target.GetKundeById(1);
            kunde.Nachname = "Fuoco";

            Target.UpdateKunde(kunde);

            Assert.Equal("Fuoco", Target.GetKundeById(1).Nachname);
        }

        [Fact]
        public void AddKundeTest()
        {
            Target.AddKunde("Bucher","Daniel", new DateTime(1995, 05, 05));
            Kunde dani = Target.GetKundeById(5);
            Assert.Equal("Bucher", dani.Nachname);
        }

        [Fact]
        public void ListKundeByIdTest()
        {
            Target.AddKunde("Bucher", "Daniel", new DateTime(1995, 05, 05));
            Kunde kunde = Target.GetKundeById(5);
            Assert.Equal("Daniel", kunde.Vorname);
        }

        [Fact]
        public void DeleteKundeTest()
        {
            Target.AddKunde("Bucher", "Daniel", new DateTime(1995, 05, 05));
            Target.deleteKunde(5);
            Assert.Equal(4, Target.GetKunden().Count);
        }

    }
}
