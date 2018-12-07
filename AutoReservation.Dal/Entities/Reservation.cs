using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
   public class Reservation
    {
        [Key]
        public int ReservationsNr { get; set; }
        [ForeignKey(nameof(AutoId))]
        public int AutoId { get; set; }
        [ForeignKey(nameof(KundeId))]
        public int KundeId { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public byte?[] RowVersion { get; set; }
        
        [NotMapped]
        public Auto Auto { get; set; }
        [NotMapped]
        public Kunde Kunde { get; set; }

    }
}
