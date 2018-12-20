using AutoReservation.UI.ViewModels;
using MahApps.Metro.Controls;
using System;

namespace AutoReservation.UI.Views
{
    public partial class AutoView : MetroWindow
    {

        public bool IsClosed { get; set; }

        public AutoView()
        {
            InitializeComponent();

            //DataContext = AutoMock.AutoShowroom;
            DataContext = new AutoViewModel();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }
    }
}
