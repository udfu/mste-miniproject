﻿using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto
    {
        public int ReservationsNr { get; set; }
        public AutoDto Auto { get; set; }
        public KundeDto Kunde { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public byte[] RowVersion { get; set; }

        public ReservationDto()
        {
        }

        public ReservationDto(int id, AutoDto auto, KundeDto kunde, DateTime von, DateTime bis)
        {
            ReservationsNr = id;
            Auto = auto;
            Kunde = kunde;
            Von = von;
            Bis = bis;
        }

        public ReservationDto(ReservationDto reservationDtoTemplate)
        {
            ReservationsNr = reservationDtoTemplate.ReservationsNr;
            Auto = reservationDtoTemplate.Auto;
            Kunde = reservationDtoTemplate.Kunde;
            Von = reservationDtoTemplate.Von;
            Bis = reservationDtoTemplate.Bis;

            if (reservationDtoTemplate.RowVersion != null)
            {
                RowVersion = new byte[reservationDtoTemplate.RowVersion.Length];
                reservationDtoTemplate.RowVersion.CopyTo(this.RowVersion, 0);
            }

        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReservationDto))
            {
                return false;
            }

            var item = (ReservationDto)obj;

            if (this.ReservationsNr == item.ReservationsNr && this.Auto == item.Auto 
                && this.Kunde == item.Kunde & this.Von == item.Von && this.Bis == item.Bis)
            {
                return true;
            }

            return false;
        }



        public override int GetHashCode()
        {
            return ReservationsNr.GetHashCode();
        }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
    }
}
