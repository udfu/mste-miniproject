using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [Fact]
        public void GetReservationsTest()
        {
            Assert.Equal(4, Target.GetReservations().Count);
        }

        [Fact]
        public void GetReservationByIdTest()
        {
            Assert.Equal(1, Target.GetReservationById(1).AutoId);
        }

        [Fact]
        public void AddReservationTest()
        {
            KundeManager km = new KundeManager();
            AutoManager am = new AutoManager();
            Kunde k1 = km.GetKundeById(1);
            Auto a1 = am.GetAutoById(1);
            Reservation reservation = new Reservation { KundeId = k1.Id, AutoId = a1.Id, Von = new DateTime(2018, 12, 24), Bis = new DateTime(2018, 12, 27) };

            Target.AddReservation(reservation);
            Assert.Equal(5, Target.GetReservations().Count);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            int id = 4;
            Reservation reservation = Target.GetReservationById(id);
            reservation.AutoId = 3;
            
            Target.UpdateReservation(reservation);

            Assert.Equal(reservation.AutoId, Target.GetReservationById(id).AutoId);
        }

        [Fact]
        public void DeleteAutoTest()
        {
            Target.DeleteReservation(1);
            Assert.Equal(3, Target.GetReservations().Count);
        }

    }
}
