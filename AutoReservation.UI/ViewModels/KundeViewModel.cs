using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Annotations;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KundeDto> KundenDtos { get; set; }
        public KundeDto CurrentKundeDto { get; set; }


        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            CurrentKundeDto = KundenDtos[0];
        }

        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ourList = (ListBox) sender;
            CurrentKundeDto = KundenDtos[ourList.SelectedIndex];
            OnPropertyChanged(nameof(CurrentKundeDto));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}