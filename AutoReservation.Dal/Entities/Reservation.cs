using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
   public class Reservation
    {
        [ForeignKey(nameof(AutoId))]
        public int AutoId { get; set; }
        public DateTime Bis { get; set; }
        [ForeignKey(nameof(KundeId))]
        public int KundeId { get; set; }
        [Key]
        public int ReservationsNr { get; set; }
        public byte? RowVersion { get; set; }
        public DateTime Von { get; set; }


    }
}
