using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal.Entities;
using AutoReservation.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        KundeManager kundenManager = new KundeManager();

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public List<KundeDto> ReadKundeDtos()
        {
            WriteActualMethod();

            return DtoConverter.ConvertToDtos(kundenManager.GetKunden());
        }

        public KundeDto ReadKundeDto(int kundeId)
        {
            WriteActualMethod();

            try
            {
                return DtoConverter.ConvertToDto(kundenManager.GetKundeById(kundeId));
            }

            catch (InvalidOperationException)
            {
                OutOfRangeFault fault = new OutOfRangeFault()
                {
                    Operation = "Read"
                };

                throw new FaultException<OutOfRangeFault>(fault);
            }

           
        }

        public void insertKunde(string nachname, string vorname, DateTime geburtsDatum)
        {
            WriteActualMethod();

            kundenManager.AddKunde(nachname, vorname, geburtsDatum);
           }

        public void updateKunde(int id, string nachname, string vorname, DateTime geburtsDatum)
        {
            WriteActualMethod();

            try
            {
                kundenManager.UpdateKunde(id, geburtsDatum, nachname, vorname);
            }
            catch (InvalidOperationException e)
            {
                OutOfRangeFault fault = new OutOfRangeFault
                {
                    Operation = "update"
                };

                throw new FaultException<OutOfRangeFault>(fault);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ConcurrencyFault fault = new ConcurrencyFault();

                throw new FaultException<ConcurrencyFault>(fault);
            }
                  
        }

        public void deleteKunde(int id)
        {
            WriteActualMethod();

            try
            {
                kundenManager.deleteKunde(id);
            }
            catch (InvalidOperationException)
            {
                OutOfRangeFault fault = new OutOfRangeFault()
                {
                    Operation = "delete"
                };

                throw new FaultException<OutOfRangeFault>(fault);
            }
            
        }
    }
}