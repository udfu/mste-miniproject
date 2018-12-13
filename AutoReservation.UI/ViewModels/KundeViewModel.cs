using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Annotations;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KundeDto> KundenDtos { get; set; }
        public KundeDto CurrentKundeDto { get; set; }
        public RelayCommand<KundeDto> DeleteCommand { get; set; }

        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            DeleteCommand = new RelayCommand<KundeDto>(kunde => Remove());
        }

        public void Remove()
        {
            int index = CurrentKundeDto.Id;
            AppViewModel.Target.deleteKunde(index);
            KundenDtos.Remove(CurrentKundeDto);
            CurrentKundeDto = null;
            OnPropertyChanged(nameof(CurrentKundeDto));
        }


        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ourList = (ListBox) sender;
            int index = ourList.SelectedIndex;
            if (index >= 0)
            {
                CurrentKundeDto = KundenDtos[index];
                OnPropertyChanged(nameof(CurrentKundeDto));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}