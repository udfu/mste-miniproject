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
            Reservation reservation = new Reservation { ReservationsNr = 5, KundeId = 1, AutoId = 1, Von = new DateTime(2018, 12, 24), Bis = new DateTime(2018, 12, 27) };

            Target.AddReservation(reservation);

            Assert.Equal(reservation, Target.GetReservationById(5));
        }

        [Fact]
        public void UpdateAutoTest()
        {
            int id = 4;
            Reservation reservation = Target.GetReservationById(id);
            reservation.AutoId = 3;
            
            Target.UpdateReservation(reservation);

            Assert.Equal(reservation, Target.GetReservationById(id));
        }

        [Fact]
        public void DeleteAutoTest()
        {
            Target.DeleteReservation(1);
            Assert.Equal(3, Target.GetReservations().Count);
        }

    }
}
