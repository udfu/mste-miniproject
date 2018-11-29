namespace AutoReservation.Dal.Entities
{
    abstract class Auto
    {
        public int  Id { get; set; }
        public string Marke { get; set; }
        public byte RowVersion { get; set; }
        public int Tagestarif { get; set; }

    }

    class Standardauto : Auto
    {

    }

    class LuxusklasseAuto : Auto
    {
        public int Basistarif { get; set; }
    }

    class MittelklasseAuto : Auto
    {

    }
}


