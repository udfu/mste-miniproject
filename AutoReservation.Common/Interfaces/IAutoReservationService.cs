using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {

        [OperationContract]
        List<AutoDto> ReadAutoDtos();

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        AutoDto ReadAutoDto(int autoId);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void InsertAuto(AutoDto newAuto);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        [FaultContract(typeof(ConcurrencyFault))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void DeleteAuto(int id);
        
        [OperationContract]
        List<KundeDto> ReadKundeDtos();

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        KundeDto ReadKundeDto(int kundeId);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void insertKunde(string nachname, string vorname, DateTime geburtsDatum);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        [FaultContract(typeof(ConcurrencyFault))]
        void updateKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void deleteKunde(int id);

        [OperationContract]
        List<ReservationDto> ReadReservationDtos();

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        ReservationDto ReadReservationDto(int id);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void insertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        [FaultContract(typeof(ConcurrencyFault))]
        void updateReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        void deleteReservation(int id);

        [OperationContract]
        [FaultContract(typeof(OutOfRangeFault))]
        bool IsCarAvailable(int id, DateTime von, DateTime bis);

    }
}