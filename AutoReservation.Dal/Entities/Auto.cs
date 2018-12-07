using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        [Key]
        public int  Id { get; set; }
        public string Marke { get; set; }
        [Required]
        public int Tagestarif { get; set; }
        public byte?[] RowVersion { get; set; }
        

        public virtual ICollection<Reservation> Reservationen { get; set; }

    }

    public class StandardAuto : Auto
    {

    }

    public class LuxusklasseAuto : Auto
    {
        public int? Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto
    {

    }
}


