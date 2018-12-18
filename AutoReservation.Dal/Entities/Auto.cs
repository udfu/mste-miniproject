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
        [Timestamp]
        public byte[] RowVersion { get; set; }
        
        public virtual ICollection<Reservation> Reservationen { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Auto))
            {
                return false;
            }

            var item = (Auto) obj;

            if (Id == item.Id && Marke == item.Marke && Tagestarif == item.Tagestarif)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class StandardAuto : Auto
    {

    }

    public class LuxusklasseAuto : Auto
    {
        public int? Basistarif { get; set; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is LuxusklasseAuto && Basistarif == ((LuxusklasseAuto) obj).Basistarif)
            {
                return true;
            }
            return false;
        }
    }

    public class MittelklasseAuto : Auto
    {

    }
}


