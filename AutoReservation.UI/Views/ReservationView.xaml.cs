using MahApps.Metro.Controls;

namespace AutoReservation.UI
{
    public partial class ReservationView : MetroWindow
    {
        public ReservationView()
        {
            InitializeComponent();
            // DataContext = new AutoViewModel();
            DataContext = AutoMock.AutoShowroom;
        }
    }
}
