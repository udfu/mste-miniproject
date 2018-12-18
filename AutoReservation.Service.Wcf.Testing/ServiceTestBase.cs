using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase

    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            List<AutoDto> list = Target.ReadAutoDtos();

            Assert.Equal(new AutoDto("Fiat Punto", 50, AutoKlasse.Standard), list[0]);
            Assert.Equal(new AutoDto("VW Golf", 120, AutoKlasse.Mittelklasse), list[1]);
            Assert.Equal(new AutoDto("Audi S6", 180, AutoKlasse.Luxusklasse, 50), list[2]);
            Assert.Equal(new AutoDto("Fiat 500", 75, AutoKlasse.Standard), list[3]);
        }

        [Fact]
        public void GetKundenTest()
        {
            List<KundeDto> list = Target.ReadKundeDtos();

            Assert.Equal(new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05)), list[0]);
            Assert.Equal(new KundeDto(2, "Beil", "Timo", new DateTime(1980, 09, 09)), list[1]);
            Assert.Equal(new KundeDto(3, "Pfahl", "Martha", new DateTime(1990, 07, 03)), list[2]);
            Assert.Equal(new KundeDto(4, "Zufall", "Rainer", new DateTime(1954, 11, 11)), list[3]);
        }

        [Fact]
        public void GetReservationenTest()
        {
            KundeDto k1 = new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05));
            KundeDto k2 = new KundeDto(2, "Beil", "Timo", new DateTime(1980, 09, 09));
            KundeDto k3 = new KundeDto(3, "Pfahl", "Martha", new DateTime(1990, 07, 03));

            AutoDto a1 = new AutoDto("Fiat Punto", 50, AutoKlasse.Standard);
            AutoDto a2 = new AutoDto("VW Golf", 120, AutoKlasse.Mittelklasse);
            AutoDto a3 = new AutoDto("Audi S6", 180, AutoKlasse.Luxusklasse, 50);

            List<ReservationDto> list = Target.ReadReservationDtos();
            Assert.Equal(4, list.Count);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            Assert.Equal(new AutoDto("Fiat Punto", 50, AutoKlasse.Standard), Target.ReadAutoDto(1));
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            Assert.Equal(new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05)), Target.ReadKundeDto(1));
        }

        [Fact]
        public void GetReservationByNrTest()
        { 
            Assert.Equal(new DateTime(2020,01,10),Target.ReadReservationDto(1).Von);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<AutoReservation.Common.OutOfRangeFault>>(() => Target.ReadAutoDto(5));
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<AutoReservation.Common.OutOfRangeFault>>(() => Target.ReadKundeDto(5));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Throws<FaultException<AutoReservation.Common.OutOfRangeFault>>(() 
                => Target.ReadReservationDto(999));
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            AutoDto auto = new AutoDto("Fiat Punto", 50, AutoKlasse.Standard);
            Target.InsertAuto(auto);
            Assert.True(Target.ReadAutoDtos().Exists(e => e.Equals(auto)));
        }

        [Fact]
        public void InsertKundeTest()
        {
            string lastName = "Fuoco";
            string name = "Dario";
            DateTime birthDate = new DateTime(1995, 12, 12);
            int id = 5;

            Target.insertKunde(lastName, name, birthDate);

            KundeDto expectedDto = new KundeDto(id, lastName, name, birthDate);

            Assert.Equal(expectedDto, Target.ReadKundeDto(id));
        }

        [Fact]
        public void InsertReservationTest()
        {
            KundeDto k1 = new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05));
            AutoDto a1 = new AutoDto("Fiat Punto", 50, AutoKlasse.Standard);
            int amountOfReservationsBefore = Target.ReadReservationDtos().Count;
            Target.insertReservation(new ReservationDto { Auto = a1, Kunde = k1, Von = new DateTime(2023, 01, 01), Bis = new DateTime(2023, 02, 01) });
            int amountOfReservationsAfter = Target.ReadReservationDtos().Count;
            Assert.Equal(amountOfReservationsBefore + 1,amountOfReservationsAfter);
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(1);
            Assert.False(Target.ReadAutoDtos().Exists(e => e.Id == 1));
        }

        [Fact]
        public void DeleteKundeTest()
        {
            int amountOfKundenBefore = Target.ReadKundeDtos().Count;
            Target.deleteKunde(1);
            int amountOfKundenAfter = Target.ReadKundeDtos().Count;
            Assert.Equal(1, amountOfKundenBefore - amountOfKundenAfter);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            int amountOfReservations = Target.ReadReservationDtos().Count;
            Target.deleteKunde(1);
            int amountOfReservationsAfter = Target.ReadKundeDtos().Count;
            Assert.Equal(1, amountOfReservations - amountOfReservationsAfter);
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            AutoDto auto = Target.ReadAutoDto(1);

            auto.Marke = "Aston Martin";
            Target.UpdateAuto(auto);
            Assert.Equal(auto, Target.ReadAutoDto(1));
        }

        [Fact]
        public void UpdateKundeTest()
        {
            KundeDto kunde = Target.ReadKundeDto(1);

            kunde.Nachname = "Fuoco";
            Target.updateKunde(kunde);
            Assert.Equal(kunde, Target.ReadKundeDto(1));
        }

        [Fact]
        public void UpdateReservationTest()
        {
            int id = 4;
            ReservationDto reservation = Target.ReadReservationDto(id);
            reservation.Auto = Target.ReadAutoDto(3);

            Target.updateReservation(reservation);

            Assert.Equal(reservation, Target.ReadReservationDto(id));
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto = Target.ReadAutoDto(1);
            AutoDto sameAuto = Target.ReadAutoDto(1);

            auto.Marke = "Aston Martin";
            Target.UpdateAuto(auto);

            sameAuto.Marke = "Tesla Roadster";
            Assert.Throws<FaultException<AutoReservation.Common.ConcurrencyFault>>(() => Target.UpdateAuto(sameAuto));
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kunde = Target.ReadKundeDto(1);
            KundeDto sameKunde = Target.ReadKundeDto(1);

            kunde.Nachname = "Fuoco";
            Target.updateKunde(kunde);

            sameKunde.Vorname = "Dario";
            Action action = () => Target.updateKunde(sameKunde);
            Assert.Throws<FaultException<AutoReservation.Common.ConcurrencyFault>>(action);
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Auto = Target.ReadAutoDto(1),
                Kunde = Target.ReadKundeDto(1),
                Bis = new DateTime(2018, 12, 20),
                Von = new DateTime(2018, 12, 24)
            };

            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.insertReservation(reservation));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            Assert.True(Target.IsCarAvailable(2, new DateTime(2022, 01, 01), new DateTime(2022, 02, 01)));
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            Assert.Throws<FaultException<AutoReservation.Common.AutoUnavailableFault>>(() => Target.IsCarAvailable(2, new DateTime(2020, 06, 18), new DateTime(2020, 06, 25)));
        }

        #endregion
    }
}