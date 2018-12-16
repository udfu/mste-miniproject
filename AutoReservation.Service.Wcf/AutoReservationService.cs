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
        private readonly AutoManager _autoManager = new AutoManager();
        private readonly KundeManager _kundenManager = new KundeManager();


        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");


        public List<AutoDto> ReadAutoDtos()
        {
            WriteActualMethod();

            return DtoConverter.ConvertToDtos(_autoManager.GetAutos());
        }

        public List<KundeDto> ReadKundeDtos()
        {
            WriteActualMethod();

            return DtoConverter.ConvertToDtos(_kundenManager.GetKunden());
        }

        public AutoDto ReadAutoDto(int autoId)
        {
            WriteActualMethod();

            try
            {
                return DtoConverter.ConvertToDto(_autoManager.GetAutoById(autoId));
            }

            catch (InvalidOperationException)
            {
                OutOfRangeFault fault = new OutOfRangeFault()
                {
                    Operation = "Read"
                };

                throw  new FaultException<OutOfRangeFault>(fault);
            }
        }
        
        public KundeDto ReadKundeDto(int kundeId)
        {
            WriteActualMethod();

            try
            {
                return DtoConverter.ConvertToDto(_kundenManager.GetKundeById(kundeId));
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

        public void InsertAuto(AutoDto newAuto)
        {
            WriteActualMethod();
            _autoManager.AddAuto(newAuto.ConvertToEntity());
        }


        public void insertKunde(string nachname, string vorname, DateTime geburtsDatum)
        {
            WriteActualMethod();

            _kundenManager.AddKunde(nachname, vorname, geburtsDatum);
           }


        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();

            try
            {
                _autoManager.UpdateAuto(auto.ConvertToEntity());
            }
            catch (InvalidOperationException)
            {
                OutOfRangeFault fault = new OutOfRangeFault
                {
                    Operation = "update"
                };

                throw new FaultException<OutOfRangeFault>(fault);
            }
            catch (DbUpdateConcurrencyException)
            {
                ConcurrencyFault fault = new ConcurrencyFault();
                throw new FaultException<ConcurrencyFault>(fault);
            }
        }

        public void updateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                _kundenManager.UpdateKunde(kunde.ConvertToEntity());
            }
            catch (InvalidOperationException)
            {
                OutOfRangeFault fault = new OutOfRangeFault
                {
                    Operation = "update"
                };

                throw new FaultException<OutOfRangeFault>(fault);
            }
            catch (DbUpdateConcurrencyException)
            {
                ConcurrencyFault fault = new ConcurrencyFault();

                throw new FaultException<ConcurrencyFault>(fault);
            }
           
        }

        public void DeleteAuto(int id)
        {
            WriteActualMethod();

            try
            {
                _autoManager.DeleteAuto(id);
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

        public void deleteKunde(int id)
        {
            WriteActualMethod();

            try
            {
                _kundenManager.deleteKunde(id);
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