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
        public RelayCommand<KundeDto> DeleteCommand { get; set; }

        private int _selectedValue;

        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                OnPropertyChanged(nameof(CurrentKundeDto));
            }
        }


        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            CurrentKundeDto = KundenDtos[0];
            DeleteCommand = new RelayCommand<KundeDto>(kunde => Remove());
            SelectedValue = 0;
        }

        public void Remove()
        {
            KundenDtos.Remove(CurrentKundeDto);
            OnPropertyChanged(nameof(KundenDtos));
            SelectedValue -= 1;
            OnPropertyChanged(nameof(SelectedValue)); //Problem, da mehrere Threads????
            OnPropertyChanged(nameof(CurrentKundeDto));
        }


        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Console.Write(SelectedValue);

//            ListBox ourList = (ListBox) sender;
            CurrentKundeDto = KundenDtos[SelectedValue];
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