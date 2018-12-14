using System;
using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        public List<Auto> GetAutos()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.ToList<Auto>();
            }
        }

        public Auto GetAutoById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Auto auto = context
                    .Autos
                    .Single(a => a.Id == id);

                return auto;
            }
        }

        public void AddAuto(Auto newAuto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Autos.Add(newAuto);
                context.SaveChanges();
            }
        }

        public void UpdateAuto(Auto updatedAuto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(updatedAuto).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteAuto(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                Auto autoToBeDeleted = context
                    .Autos
                    .First(a => a.Id == id);

                context.Entry(autoToBeDeleted).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}