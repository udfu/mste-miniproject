using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.UI
{
    public enum AutoTyp
    {
        Standard,
        Mittelklasse,
        Luxus
    }

    public class Auto
    {
        public int Id { get; set; }
        public string Marke { get; set; }
        public AutoTyp Typ { get; set; }
        public double Tagestarif { get; set; }
        public double BasisTarif { get; set; }
    }
    public class AutoViewModel
    {
        public List<Auto> Autos { get; set; }
        public Auto SelectedAuto { get; set; }

        public IEnumerable<AutoTyp> AutoTypValues
        {
            get { return System.Enum.GetValues(typeof(AutoTyp)).Cast<AutoTyp>(); }
        }
    }

    public class AutoMock
    {
        public static AutoViewModel AutoShowroom = CreateAutoViewModel();

        private static AutoViewModel CreateAutoViewModel()
        {
            Auto voiture = new Auto() {
                Id = 1,
                Marke = "Fiat",
                Typ = AutoTyp.Standard,
                Tagestarif = 143.50
            };

            return new AutoViewModel()
            {
                Autos = new List<Auto>() { voiture },
                SelectedAuto = voiture
            };
        }
    }
}
