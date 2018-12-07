using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {



        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");


        public Collection<KundeDto> ReadKundeDtos(Collection<int> kundenId)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public AutoDto ReadKundeDto(int kundeId)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void insertKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte?[] rowVersion)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void updateKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte?[] rowVersion)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }

        public void deleteKunde(int id)
        {
            WriteActualMethod();
            throw new NotImplementedException();
        }
    }
}