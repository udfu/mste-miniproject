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

        public KundeDto(int id, string nachname, string vorname, DateTime geburtsdatum)
        {
            Id = id;
            Nachname = nachname;
            Vorname = vorname;
            Geburtsdatum = geburtsdatum;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KundeDto))
            {
                return false;
            }

            var item = (KundeDto) obj;

            if (this.Id == item.Id && this.Nachname == item.Nachname && this.Vorname == item.Vorname &&
                this.Geburtsdatum == item.Geburtsdatum)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}