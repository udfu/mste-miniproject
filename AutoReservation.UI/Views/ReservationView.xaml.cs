using AutoReservation.UI.ViewModels;
using MahApps.Metro.Controls;

namespace AutoReservation.UI
{
    public partial class ReservationView : MetroWindow
    {
        public ReservationView()
        {
            InitializeComponent();
            ReservationViewModel ReservationVm = new ReservationViewModel();
            DataContext = ReservationVm;
        }
    }
}
