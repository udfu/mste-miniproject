using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Annotations;
using AutoReservation.UI.Views;

namespace AutoReservation.UI.ViewModels
{
    public class KundeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<KundeDto> KundenDtos { get; set; }
        public KundeDto CurrentKundeDto { get; set; }

        public bool DetailsVisibility { get; set; }
        public int Index { get; set; }

        public RelayCommand<KundeDto> DeleteCommand { get; set; }
        public RelayCommand<KundeDto> SaveCommand { get; set; }
        public RelayCommand<ObservableCollection<KundeDto>> RefreshCommand { get; set; }
        public RelayCommand<KundeDto> AddCommand { get; set; }

        public KundeViewModel()
        {
            KundenDtos = new ObservableCollection<KundeDto>(AppViewModel.Target.ReadKundeDtos());

            DetailsVisibility = false;
            Index = -1;

            DeleteCommand = new RelayCommand<KundeDto>(k => Delete(), k => CanDelete);
            SaveCommand = new RelayCommand<KundeDto>(k => Save(), k => CanSave);
            RefreshCommand = new RelayCommand<ObservableCollection<KundeDto>>(c => Refresh());
            AddCommand = new RelayCommand<KundeDto>(k => Add());
        }

        public bool CanDelete => CurrentKundeDto != null;

        public void Delete()
        {
            if (CurrentKundeDto == null)
            {
                return;
            }

            var win = new DialogWindowView();

            if (win.ShowDialog() == true)
            {
                AppViewModel.Target.deleteKunde(CurrentKundeDto.Id);
                KundenDtos.Remove(CurrentKundeDto);

                Refresh();
            }
        }

        public bool CanSave => CurrentKundeDto != null;

        public void Save()
        {
            if (CurrentKundeDto == null || !CheckInput())
            {
                return;
            }

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
            Index = -1;

            OnPropertyChanged(nameof(CurrentKundeDto));
            OnPropertyChanged(nameof(KundenDtos));
            OnPropertyChanged(nameof(DetailsVisibility));
            OnPropertyChanged(nameof(Index));
        }

        public void Add()
        {
            Index = -1;
            CurrentKundeDto = new KundeDto();
            DetailsVisibility = true;

            SaveCommand.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(Index));
            OnPropertyChanged(nameof(CurrentKundeDto));
            OnPropertyChanged(nameof(DetailsVisibility));
        }

        public bool CheckInput()
        {
            if (CurrentKundeDto.Nachname == null || CurrentKundeDto.Vorname == null || CurrentKundeDto.Nachname == "" ||
                CurrentKundeDto.Vorname == "")
            {
                string messageBoxText = "invalid names! Please check the input.";
                string caption = "Invalid Input";

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;

                MessageBox.Show(messageBoxText, caption, button, icon);

                return false;
            }

            return true;
        }

        public void SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DeleteCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();

            if (Index >= 0)
            {
                CurrentKundeDto = new KundeDto(KundenDtos[Index]);
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