using System;

namespace AutoReservation.Dal.Entities
{
    class Kunde
    {
        public DateTime Geburtstagsdatum { get; set; }
        public int Id { get; set; }
        public string Nachname { get; set; }
        public byte RowVersion { get; set; }
        public string Vorname { get; set; }

    }
}
