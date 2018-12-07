using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto
    {
        public int Id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public byte[] RowVersion { get; set; }
        
        public KundeDto()
        {

        }

        public KundeDto(string nachname, string vorname, DateTime geburtsdatum, byte[] rowVersion)
        {
            Nachname = nachname;
            Vorname = vorname;
            Geburtsdatum = geburtsdatum;
            RowVersion = rowVersion;
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}