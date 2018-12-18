using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;
using AutoReservation.Common;
using Microsoft.EntityFrameworkCore;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private readonly AutoManager _autoManager = new AutoManager();
        private readonly KundeManager _kundenManager = new KundeManager();
        private readonly ReservationManager _reservationManager = new ReservationManager();

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

        public List<ReservationDto> ReadReservationDtos()
        {
            WriteActualMethod();
            return DtoConverter.ConvertToDtos(_reservationManager.GetReservations());
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

        public ReservationDto ReadReservationDto(int id)
        {
            WriteActualMethod();

            try
            {
                //return DtoConverter.ConvertToDto(_reservationManager.GetReservationById(id));
                return _reservationManager.GetReservationById(id).ConvertToDto();
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

        public void insertReservation( ReservationDto reservation)
        {

            WriteActualMethod();
            try
            {
                _reservationManager.AddReservation(reservation.ConvertToEntity());
            }
            catch (AutoUnavailableException)
            {
                AutoUnavailableFault fault = new AutoUnavailableFault()
                {
                    Operation = "insert reservation"
                };
                throw new FaultException<AutoUnavailableFault>(fault);
            }
            catch (InvalidDateRangeException)
            {
                InvalidDateRangeFault fault = new InvalidDateRangeFault()
                {
                    Operation = "insert reservation"
                };
                throw new FaultException<InvalidDateRangeFault>(fault);
            }
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

        public void updateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                _reservationManager.UpdateReservation(reservation.ConvertToEntity());
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
            catch (AutoUnavailableException)
            {
                AutoUnavailableFault fault = new AutoUnavailableFault()
                {
                    Operation = "update"
                };

                throw new FaultException<AutoUnavailableFault>(fault);
            }
            catch (InvalidDateRangeException)
            {
                InvalidDateRangeFault fault = new InvalidDateRangeFault()
                {
                    Operation = "update"
                };

                throw new FaultException<InvalidDateRangeFault>(fault);
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

        public void deleteReservation(int id)
        {
            WriteActualMethod();
            try
            {
                _reservationManager.DeleteReservation(id);
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

        public bool IsCarAvailable(int id, DateTime von, DateTime bis)
        {
            WriteActualMethod();
            try
            {
                return _reservationManager.IsCarAvailable(id,von,bis);
            }
            catch (AutoUnavailableException)
            {
                AutoUnavailableFault fault = new AutoUnavailableFault()
                {
                    Operation = "Availability Check"
                };

                throw new FaultException<AutoUnavailableFault>(fault);
            }
        }

    }
}