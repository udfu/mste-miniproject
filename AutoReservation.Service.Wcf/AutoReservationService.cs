using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal.Entities;

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

            return DtoConverter.ConvertToDto(kundenManager.GetKundeById(kundeId));
        }

        public void insertKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte[] rowVersion)
        {
            WriteActualMethod();
            
            kundenManager.AddKunde(DtoConverter.ConvertToEntity(new KundeDto(nachname, vorname, geburtsDatum, rowVersion)));
           }

        public void updateKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte[] rowVersion)
        {
            WriteActualMethod();

            kundenManager.UpdateKunde(id, geburtsDatum, nachname, vorname);        
        }

        public void deleteKunde(int id)
        {
            WriteActualMethod();
           
            kundenManager.deleteKunde(id);
        }
    }
}