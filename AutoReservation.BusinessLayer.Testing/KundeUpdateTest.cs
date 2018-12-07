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
        public void KundeTest()
        {

        }


        [Fact]
        public void UpdateKundeTest()
        {
            Target.UpdateKunde(1, new DateTime(1994,9,17), "Fuoco", "Dario");
            Kunde kunde = Target.GetKundeById(1);
            Assert.Equal("Dario", kunde.Vorname);
        }

        [Fact]
        public void AddKundeTest()
        {
            Kunde newKunde = new Kunde { Id = 5, Nachname = "Bucher", Vorname = "Daniel", Geburtsdatum = new DateTime(1995, 05, 05) };
            Target.AddKunde(newKunde);
            Kunde dani = Target.GetKundeById(5);
            Assert.Equal("Bucher", dani.Nachname);
        }

        [Fact]
        public void GetKundenTest()
        {   
            Assert.Equal(4, Target.GetKunden().Count);
        }
    }
}
