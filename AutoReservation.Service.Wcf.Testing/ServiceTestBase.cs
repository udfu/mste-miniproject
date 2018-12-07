using System;
using System.Collections.Generic;
using System.ServiceModel;
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
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            Assert.Equal(new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05)), Target.ReadKundeDto(1));
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<AutoReservation.Common.OutOfRangeFault>>(() => Target.ReadKundeDto(5));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeTest()
        {
            string lastName = "Fuoco";
            string name = "Dario";
            DateTime birthDate = new DateTime(1995, 12, 12);
            int id = 1;

            Target.updateKunde(1, lastName, name, birthDate);

            KundeDto expectedDto = new KundeDto(id, lastName, name, birthDate);

            Assert.Equal(expectedDto, Target.ReadKundeDto(id));
        }

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
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
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}