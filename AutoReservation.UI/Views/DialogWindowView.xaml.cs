using System.Windows;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DialogWindowView : Window
    {
        public DialogWindowView()
        {
            InitializeComponent();
        }

        private void JaButton_Onclick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
