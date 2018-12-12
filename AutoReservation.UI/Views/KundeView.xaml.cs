using System.Collections.ObjectModel;
using System.Windows;

using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Service.Wcf;

namespace AutoReservation.UI.Views
{
    /// <summary>
    /// Interaction logic for KundeView.xaml
    /// </summary>
    public partial class KundeView : Window
    {
        private AutoReservationService _reservationService;

       public ObservableCollection<KundeDto> KundenDtos { get; set; }
      
        public KundeView()
        {
            InitializeComponent();

            _reservationService = new AutoReservationService();
            KundenDtos = new ObservableCollection<KundeDto>(_reservationService.ReadKundeDtos());
            DataContext = this;


        }
    }
}
