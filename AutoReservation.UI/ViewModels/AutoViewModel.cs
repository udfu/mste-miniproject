namespace AutoReservation.UI
{
    public enum AutoTyp
    {
        Standard,
        Mittelklasse,
        Luxus
    }

    public class AutoViewModel
    {
        public int Id { get; set; }
        public string Marke { get; set; }
        public AutoTyp Typ { get; set; }
        public double Tagestarif { get; set; }
        public double BasisTarif { get; set; }
    }

    public class AutoMock
    {
        public static AutoViewModel Voiture = new AutoViewModel()
        {
            Id = 1,
            Marke = "Fiat",
            Typ = AutoTyp.Standard,
            Tagestarif = 143.50
        };
    }
}
