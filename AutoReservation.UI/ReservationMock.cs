using System;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI
{
    public class ReservationMock
    {


        //        new AutoDto("Fiat Punto", 50, AutoKlasse.Standard), list[0]);
        //        (new AutoDto("VW Golf", 120, AutoKlasse.Mittelklasse), list[1]);
        //        (new AutoDto("Audi S6", 180, AutoKlasse.Luxusklasse, 50), list[2]);
        //        (new AutoDto("Fiat 500", 75, AutoKlasse.Standard), list[3]);
        //
        //        Assert.Equal(new KundeDto(1, "Nass", "Anna", new DateTime(1981, 05, 05)), list[0]);
        //        Assert.Equal(new KundeDto(2, "Beil", "Timo", new DateTime(1980, 09, 09)), list[1]);
        //        Assert.Equal(new KundeDto(3, "Pfahl", "Martha", new DateTime(1990, 07, 03)), list[2]);
        //        Assert.Equal(new KundeDto(4, "Zufall", "Rainer", new DateTime(1954, 11, 11)), list[3]);
        //
        //        new ReservationDto()
        //
        //        private List<ReservationDto> testDtos
        //        {
        //            new ReservationDto { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)}
        ////            new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
        ////            new Reservation { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
        ////            new Reservation { ReservationsNr = 4, AutoId = 2, KundeId = 1, Von = new DateTime(2020, 05, 19), Bis = new DateTime(2020, 06, 19)},
        //        };

        //
        //        new List<Reservation>
        //        {
        //            new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
        //            new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
        //            new Reservation { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
        //            new Reservation { ReservationsNr = 4, AutoId = 2, KundeId = 1, Von = new DateTime(2020, 05, 19), Bis = new DateTime(2020, 06, 19)},
        //        };

        private List<ReservationDto> testDtos = null;

        List<ReservationDto> ReadReservationDtos()
        {
            return testDtos;
        }

        ReservationDto ReadReservationDto(int id)
        {
            return testDtos[id];
        }

        void insertReservation(int id, AutoDto auto, KundeDto kunde, DateTime von, DateTime bis)
        {
        }

        void updateReservation(ReservationDto reservation)
        {
            int id = reservation.ReservationsNr;

            ReservationDto deletableDto = null;

            foreach (var currentDto in testDtos)
            {
                if (currentDto.ReservationsNr == reservation.ReservationsNr)
                {
                    deletableDto = currentDto;
                    break;
                }
            }

            testDtos.Remove(deletableDto);
            testDtos.Add(reservation);
        }

        void deleteReservation(int id)
        {
            ReservationDto deletableDto = null;

            foreach (ReservationDto currentDto in testDtos)
            {
                if (currentDto.ReservationsNr == id)
                {
                    deletableDto = currentDto;
                    break;
                }
            }

            testDtos.Remove(deletableDto);
        }

        bool IsCarAvailable(int id, DateTime von, DateTime bis)
        {
            return true;
        }
    }
}