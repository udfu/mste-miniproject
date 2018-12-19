using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private AutoView AutoView { get; set; }
        private KundeView KundeView { get; set; }
        private ReservationView ReservationView { get; set; }

        public MainWindowView()
        {
            InitializeComponent();
            AutoView = null;
            KundeView = null;
            ReservationView = null;
        }


        private void Auto_OnClick(object sender, RoutedEventArgs e)
        {
            if (AutoView == null || AutoView.IsClosed)
            {
                AutoView = new AutoView();
                AutoView.Show();
            }
            else
            {
                AutoView.Focus();
            }
        }


        private void Kunde_OnClick(object sender, RoutedEventArgs e)
        {
            if (KundeView == null || KundeView.IsClosed)
            {
                KundeView = new KundeView();
                KundeView.Show();
            }
            else
            {
                KundeView.Focus();
            }
        }

        private void Reservation_OnClick(object sender, RoutedEventArgs e)
        {
            if (ReservationView == null || ReservationView.IsClosed)
            {
                ReservationView = new ReservationView();
                ReservationView.Show();
            }
            else
            {
                ReservationView.Focus();
            }
        }
    }
}