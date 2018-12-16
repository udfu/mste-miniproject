using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        
        public bool DetailsVisibility { get; set; }
        public int index { get; set; }
  
        public RelayCommand<KundeDto> DeleteCommand { get; set; }
        public RelayCommand<KundeDto> SaveCommand { get; set; }
        public RelayCommand<ObservableCollection<KundeDto>> RefreshCommand { get; set; }
        public RelayCommand<KundeDto> AddCommand { get; set; }

        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());

            DetailsVisibility = false;
            index = -1;

            DeleteCommand = new RelayCommand<KundeDto>(kunde => Delete());
            SaveCommand = new RelayCommand<KundeDto>(k => Save());
            RefreshCommand = new RelayCommand<ObservableCollection<KundeDto>>(collection => Refresh());
            AddCommand = new RelayCommand<KundeDto>(k => Add());
        }

        public void Delete()
        {
            AppViewModel.Target.deleteKunde(index);
            KundenDtos.Remove(CurrentKundeDto);
            
            Refresh();
        }

        public void Save()
        {
            foreach (KundeDto collectionDto in KundenDtos)
            {
                if (CurrentKundeDto.Id == collectionDto.Id)
                {
                    AppViewModel.Target.updateKunde(CurrentKundeDto);
                    Refresh();
                    return;
                }
            }

            AppViewModel.Target.insertKunde(CurrentKundeDto.Nachname, CurrentKundeDto.Vorname,
                CurrentKundeDto.Geburtsdatum);
            Refresh();
        }

        public void Refresh()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());
            CurrentKundeDto = null;
            DetailsVisibility = false;
            index = -1;
            
            OnPropertyChanged(nameof(CurrentKundeDto));
            OnPropertyChanged(nameof(KundenDtos));
            OnPropertyChanged(nameof(DetailsVisibility));
            OnPropertyChanged(nameof(index));
        }

        public void Add()
        {
            index = -1;
            CurrentKundeDto = new KundeDto();
            DetailsVisibility = true;

            OnPropertyChanged(nameof(index));
            OnPropertyChanged(nameof(CurrentKundeDto));
            OnPropertyChanged(nameof(DetailsVisibility));
        }

        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (index >= 0)
            {
                CurrentKundeDto = new KundeDto(KundenDtos[index]);
                DetailsVisibility = true;
                OnPropertyChanged(nameof(CurrentKundeDto));
                OnPropertyChanged(nameof(DetailsVisibility));
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