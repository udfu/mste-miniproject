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
        //        [OperationContract]
        //        bool CheckAvailability(int autoId);
        //
        //        [OperationContract]
        //        Collection<AutoDto> ReadAutoDtos(Collection<int> autoId);
        //
        //        [OperationContract]
        //        AutoDto ReadAutoDto(int autoId);

        //        [OperationContract]
        //        void insertAuto(string marke, int tagestarif, )


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
    }
}