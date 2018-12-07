using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AutoReservation.Dal.Entities
{
   public class Kunde
    {
        [Key]
        public int Id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public DateTime Geburtsdatum { get; set; } 
        [Timestamp]
        public byte[] RowVersion { get; set; }
        

        public virtual ICollection<Reservation> Reservationen { get; set; }

    }
}
