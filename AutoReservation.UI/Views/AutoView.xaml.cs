using MahApps.Metro.Controls;

namespace AutoReservation.UI
{
    public partial class AutoView : MetroWindow
    {
        public AutoView()
        {
            InitializeComponent();
            // DataContext = new AutoViewModel();
            DataContext = AutoMock.Voiture;
        }
    }
}
