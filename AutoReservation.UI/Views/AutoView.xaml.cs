using System;
using MahApps.Metro.Controls;

namespace AutoReservation.UI
{
    public partial class AutoView : MetroWindow
    {

        public bool IsClosed { get; set; }

        public AutoView()
        {
            InitializeComponent();
            DataContext = AutoMock.AutoShowroom;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }
    }
}
