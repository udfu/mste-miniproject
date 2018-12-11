namespace AutoReservation.UI.ViewModels
{
    public enum AutoTyp
    {
        Standard,
        Mittelklasse,
        Luxus
    }

    public class AutoViewModel
    {
        public int Id;
        public string Marke;
        public AutoTyp Typ;
        public double Tagestarif;
        public double BasisTarif;
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
