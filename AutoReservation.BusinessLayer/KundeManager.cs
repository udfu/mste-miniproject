using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> GetKunden()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var kunden = context.Kunden;
                return kunden.ToList<Kunde>();
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Kunde kunde = context
                    .Kunden
                    .Single(c => c.Id == id);

                return kunde;
            }
        }

        public void AddKunde(String Nachname, String Vorname, DateTime Geburtsdatum)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Kunden.Add(new Kunde { Nachname = Nachname, Vorname = Vorname, Geburtsdatum = Geburtsdatum });
                context.SaveChanges();
            }
        }

        public void AddKunde(Kunde newKunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Kunden.Add(newKunde);
                context.SaveChanges();
            }
        }

        public void UpdateKunde(int id, DateTime Geburtsdatum, String Nachname, String Vorname)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Kunde kunde = context
                    .Kunden
                    .Single(c => c.Id == id);

                if (kunde.Id != id)
                {
                    kunde.Id = id;
                }
                if (kunde.Geburtsdatum != Geburtsdatum)
                {
                    kunde.Geburtsdatum = Geburtsdatum;
                }
                if (kunde.Nachname != Nachname && Nachname != null)
                {
                    kunde.Nachname = Nachname;
                }
                if (kunde.Vorname != Vorname && Vorname != null)
                {
                    kunde.Vorname = Vorname;
                }
                context.SaveChanges();
            }
        }

        public void UpdateKunde(Kunde updatedKunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
               
                context.Entry(updatedKunde).State = EntityState.Modified;
                context.SaveChanges();

                
            }
        }

        public void deleteKunde(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Kunde kunde = context
                    .Kunden
                    .Single(c => c.Id == id);

                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }
    }
}
