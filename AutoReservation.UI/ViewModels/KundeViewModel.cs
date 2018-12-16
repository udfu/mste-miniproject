using System;
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
        public RelayCommand<object[]> SaveCommand { get; set; }
        public RelayCommand<ObservableCollection<KundeDto>> RefreshCommand { get; set; }


        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            DeleteCommand = new RelayCommand<KundeDto>(kunde => Remove());
            SaveCommand = new RelayCommand<object[]>(values => Save(values));
            RefreshCommand = new RelayCommand<ObservableCollection<KundeDto>>(collection => Refresh());
        }

        public void Remove()
        {
            int index = CurrentKundeDto.Id;
            AppViewModel.Target.deleteKunde(index);
            KundenDtos.Remove(CurrentKundeDto);
            CurrentKundeDto = null;
            OnPropertyChanged(nameof(CurrentKundeDto));
        }

        public void Save(object[] values)
        {
            CurrentKundeDto.Nachname = ((TextBox) values[0]).Text;
            CurrentKundeDto.Vorname = ((TextBox) values[1]).Text;
            CurrentKundeDto.Geburtsdatum = DateTime.ParseExact(((TextBox) values[2]).Text, "dd.MM.yyyy", null);

            AppViewModel.Target.updateKunde(CurrentKundeDto);

            Refresh();
        }

        public void Refresh()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            OnPropertyChanged(nameof(KundenDtos));
        }

        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListBox ourList = (ListBox) sender;
            int index = ourList.SelectedIndex;
            Console.WriteLine(index);
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