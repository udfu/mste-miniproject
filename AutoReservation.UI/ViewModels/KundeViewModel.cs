using System;
using System.Collections.Generic;
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
        public List<KundeDto> KundenDtos { get; set; }
        public KundeDto CurrentKundeDto { get; set; }

        public bool DetailsVisibility { get; set; }
        private int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                SelectedIndexChanged();
            }
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ButtonState ButtonStateNachname { get; set; }

        public RelayCommand NachnameCommand { get; set; }
        public RelayCommand VornameCommand { get; set; }
        public RelayCommand GeburtstagCommand { get; set; }

        public ButtonState ButtonStateVorname { get; set; }
        public ButtonState ButtonStateGeburtsdatum { get; set; }

        public KundeViewModel()
        {
            KundenDtos = new List<KundeDto>(AppViewModel.Target.ReadKundeDtos());

            DetailsVisibility = false;

            DeleteCommand = new RelayCommand(() => Delete(), () => CanDelete);
            SaveCommand = new RelayCommand(() => Save(), () => CanSave);
            RefreshCommand = new RelayCommand(() => Refresh());
            AddCommand = new RelayCommand(() => Add());

            ButtonStateNachname = ButtonState.Inactive;
            NachnameCommand = new RelayCommand(() => SortingByNachname());

            ButtonStateVorname = ButtonState.Inactive;
            VornameCommand = new RelayCommand(() => SortingByVorname());

            ButtonStateGeburtsdatum = ButtonState.Inactive;
            GeburtstagCommand = new RelayCommand(()=> SortingByGeburtsdatum());

            Index = -1;
        }

        public bool CanDelete => CurrentKundeDto != null;

        public void Delete()
        {
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
            if (!CheckInput())
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
            KundenDtos = new List<KundeDto>(AppViewModel.Target.ReadKundeDtos());
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

        public void SelectedIndexChanged()
        {
            if (Index >= 0)
            {
                CurrentKundeDto = new KundeDto(KundenDtos[Index]);
                DetailsVisibility = true;
                OnPropertyChanged(nameof(CurrentKundeDto));
                OnPropertyChanged(nameof(DetailsVisibility));
            }

            DeleteCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        public void SortingByNachname()
        {
            ButtonStateVorname = ButtonState.Inactive;
            ButtonStateGeburtsdatum = ButtonState.Inactive;

            switch (ButtonStateNachname)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateNachname = ButtonState.Ascending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderBy(k => k.Nachname));
                    break;

                case ButtonState.Ascending:
                    ButtonStateNachname = ButtonState.Descending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderByDescending(k => k.Nachname));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByVorname()
        {
            ButtonStateNachname = ButtonState.Inactive;
            ButtonStateGeburtsdatum = ButtonState.Inactive;

            switch (ButtonStateVorname)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateVorname = ButtonState.Ascending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderBy(k => k.Vorname));
                    break;

                case ButtonState.Ascending:
                    ButtonStateVorname = ButtonState.Descending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderByDescending(k => k.Vorname));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByGeburtsdatum()
        {
            ButtonStateNachname = ButtonState.Inactive;
            ButtonStateVorname = ButtonState.Inactive;

            switch (ButtonStateGeburtsdatum)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateGeburtsdatum = ButtonState.Ascending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderBy(k => k.Geburtsdatum));
                    break;

                case ButtonState.Ascending:
                    ButtonStateGeburtsdatum = ButtonState.Descending;
                    KundenDtos = new List<KundeDto>(KundenDtos.OrderByDescending(k => k.Geburtsdatum));
                    break;
            }

           SortingOnPropertyChanged();
        }

        public void SortingOnPropertyChanged()
        {
            OnPropertyChanged(nameof(ButtonStateNachname));
            OnPropertyChanged(nameof(ButtonStateVorname));
            OnPropertyChanged(nameof(ButtonStateGeburtsdatum));
            OnPropertyChanged(nameof(KundenDtos));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}