using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.UI.ViewModels
{
    class ReservationViewModel
    {
        public int ReservationsNr { get; set; }
        
        public int AutoId { get; set; }
        public int KundeId { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public byte[] RowVersion { get; set; }

        public AutoViewModel Auto { get; set; }
        //public KundeViewModel Kunde { get; set; }
    }
}
