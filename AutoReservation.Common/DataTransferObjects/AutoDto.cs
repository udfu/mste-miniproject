namespace AutoReservation.Common.DataTransferObjects
{
    public class AutoDto
    {

        public int Id { get; set; }
        public string Marke { get; set; }
        public int Tagestarif { get; set; }
        public byte[] RowVersion { get; set; }
        public AutoKlasse AutoKlasse { get; set; }
        public int? Basistarif { get; set; }


        public AutoDto() { } 

        public AutoDto(AutoDto auto)
        {
            Id = auto.Id;
            Marke = auto.Marke;
            Tagestarif = auto.Tagestarif;
            RowVersion = auto.RowVersion;
            AutoKlasse = auto.AutoKlasse;
            Basistarif = auto.Basistarif;
        } 

        public AutoDto(string marke, int tagestarif, AutoKlasse autoKlasse)
        {
            Marke = marke;
            Tagestarif = tagestarif;
            AutoKlasse = autoKlasse;
        }

        public AutoDto(string marke, int tagestarif, AutoKlasse autoKlasse, int basistarif)
        {
            Marke = marke;
            Tagestarif = tagestarif;
            AutoKlasse = autoKlasse;
            Basistarif = basistarif;
        }

        

        public override bool Equals(object obj)
        {
            if (!(obj is AutoDto))
            {
                return false;
            }

            var item = (AutoDto) obj;

            if (Marke == item.Marke && Tagestarif == item.Tagestarif &&
                AutoKlasse == item.AutoKlasse && Basistarif == item.Basistarif)
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
            => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {RowVersion}";
    }
}