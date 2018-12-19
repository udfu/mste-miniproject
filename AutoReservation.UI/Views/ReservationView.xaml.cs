using System;
using AutoReservation.UI.ViewModels;
using MahApps.Metro.Controls;

namespace AutoReservation.UI
{
    public partial class ReservationView : MetroWindow
    {
        public Boolean IsClosed { get; set; }

        public ReservationView()
        {
            InitializeComponent();
            ReservationViewModel ReservationVm = new ReservationViewModel();
            DataContext = ReservationVm;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

    }
}
