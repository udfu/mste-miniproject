using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {

        public List<Reservation> GetReservations()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var reservations = context.Reservationen;
                return reservations.ToList<Reservation>();
            }
        }

        public Reservation GetReservationById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Reservation reservation = context
                    .Reservationen
                    .Single(c => c.ReservationsNr == id);

                return reservation;
            }
        }

        public void AddReservation(int id, int kundeId, int autoId, DateTime von, DateTime bis)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Reservationen.Add(new Reservation { ReservationsNr = id, KundeId = kundeId, AutoId = autoId, Von = von, Bis = bis  });
                context.SaveChanges();
            }
        }

        public void AddReservation(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }


        public void UpdateReservation(int id,int kundeId, int autoId, DateTime von, DateTime bis)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Reservation reservation = context
                    .Reservationen
                    .Single(c => c.ReservationsNr == id);
                
            }
        }

        public void DeleteReservation(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Reservation reservation = context
                    .Reservationen
                    .Single(c => c.ReservationsNr == id);

                context.Reservationen.Remove(reservation);
                context.SaveChanges();
            }
        }

        private bool areForeignKeysValid(int AutoId, int KundeId)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Kunde kunde = context
                    .Kunden
                    .Single(c => c.Id == KundeId);

                Auto auto = context
                    .Autos
                    .Single(c => c.Id == AutoId);

                //var query = 
                //    from k in context.Kunden
                //    where k.Id

                //if(kunde.)
                //{

                //}
            }
            return false;
        }

        public bool DateRangeCheck(DateTime von, DateTime bis)
        {
            TimeSpan diff = bis.Subtract(von);
            if(bis>von && diff.Hours>=24)
            {
                return true;
            }
            throw InvalidDateRangeException("not a valid reservation range");
        }

        private Exception InvalidDateRangeException(string v)
        {
            throw new NotImplementedException();
        }

        public bool IsCarAvailable(int id)
        {
            return false;
        }
    }
}
