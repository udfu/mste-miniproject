using System;
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
        Collection<KundeDto> ReadKundeDtos(Collection<int> kundenId);

        [OperationContract]
        AutoDto ReadKundeDto(int kundeId);

        [OperationContract]
        void insertKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte?[] rowVersion);

        [OperationContract]
        void updateKunde(int id, string nachname, string vorname, DateTime geburtsDatum, byte?[] rowVersion);

        [OperationContract]
        void deleteKunde(int id);
    }
}
