using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Annotations;
using AutoReservation.UI.Views;


namespace AutoReservation.UI.ViewModels
{
    public class ReservationViewModel : INotifyPropertyChanged
    {
        public List<ReservationDto> ReservationDtos { get; set; }
        public ReservationDto CurrentReservationDto { get; set; }
        public List<AutoDto> AutoDtos { get; set; }
        public AutoDto selectedAutoDto { get; set; }
        public List<KundeDto> KundeDtos { get; set; }
        public KundeDto selectedKundeDto { get; set; }



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
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ButtonState ButtonStateReservation { get; set; }

        public ButtonState ButtonStateAuto { get; set; }
        public ButtonState ButtonStateKunde { get; set; }

        public RelayCommand ReservationCommand { get; set; }
        public RelayCommand KundeCommand { get; set; }
        public RelayCommand AutoCommand { get; set; }

        public RelayCommand VonCommand { get; set; }
        public RelayCommand BisCommand { get; set; }

        public ButtonState ButtonStateVon { get; set; }
        public ButtonState ButtonStateBis { get; set; }
        private bool isFiltered = false;
   
        public ReservationViewModel()
        {
            ReservationDtos = new List<ReservationDto>(AppViewModel.Target.ReadReservationDtos());

            DetailsVisibility = false;

            DeleteCommand = new RelayCommand(() => Delete(), () => CanDelete);
            SaveCommand = new RelayCommand(() => Save(), () => CanSave);
            RefreshCommand = new RelayCommand(() => Refresh());
            AddCommand = new RelayCommand(() => Add());
            FilterCommand = new RelayCommand(() => FilterActive());

            ButtonStateReservation = ButtonState.Inactive;
            ReservationCommand = new RelayCommand(() => SortingByReservation());

            ButtonStateVon = ButtonState.Inactive;
            VonCommand = new RelayCommand(() => SortingByVon());

            ButtonStateBis = ButtonState.Inactive;
            BisCommand = new RelayCommand(() => SortingByBis());

            ButtonStateAuto = ButtonState.Inactive;
            AutoCommand = new RelayCommand(() => SortingByAuto());

            ButtonStateKunde = ButtonState.Inactive;
            KundeCommand = new RelayCommand(() => SortingByKunde());


            Index = -1;
        }

        public bool CanDelete => CurrentReservationDto != null;

        public void Delete()
        {
            var win = new DialogWindowView();

            if (win.ShowDialog() == true)
            {
                AppViewModel.Target.deleteReservation(CurrentReservationDto.ReservationsNr);
                ReservationDtos.Remove(CurrentReservationDto);

                Refresh();
            }
        }

        public bool CanSave => CurrentReservationDto != null;

        public void Save()
        {
            if (!CheckInput())
            {
                return;
            }
            
            foreach (ReservationDto collectionDto in ReservationDtos)
            {
                if (CurrentReservationDto.ReservationsNr == collectionDto.ReservationsNr)
                {
                    AppViewModel.Target.updateReservation(CurrentReservationDto);
                    Refresh();
                    return;
                }
            }

            CurrentReservationDto.Auto = AppViewModel.Target.ReadAutoDto(CurrentReservationDto.AutoId);
            CurrentReservationDto.Kunde = AppViewModel.Target.ReadKundeDto(CurrentReservationDto.KundeId);


            AppViewModel.Target.insertReservation(CurrentReservationDto);
            Refresh();
        }

        public int getNewReservationNr()
        {
            int max = 0;
            foreach(ReservationDto r in ReservationDtos)
            {
                if (max < r.ReservationsNr)
                {
                    max = r.ReservationsNr;
                }
            }
            return max + 1;
        }

        public void Refresh()
        {
            ReservationDtos = new List<ReservationDto>(AppViewModel.Target.ReadReservationDtos());

            CurrentReservationDto = null;
            DetailsVisibility = false;
            Index = -1;

            OnPropertyChanged(nameof(CurrentReservationDto));
            OnPropertyChanged(nameof(ReservationDtos));
            OnPropertyChanged(nameof(DetailsVisibility));
            OnPropertyChanged(nameof(Index));
        }

        public void FilterActive()
        {
            if (isFiltered)
            {
                isFiltered = false;
                Refresh();
            } else
            {
               ReservationDtos = new List<ReservationDto>(ReservationDtos.Where(p => p.Bis > DateTime.Today).ToList<ReservationDto>());
                isFiltered = true;
            }
            SortingOnPropertyChanged();
        }


        public void Add()
        {
            Index = -1;
            CurrentReservationDto = new ReservationDto();
            DetailsVisibility = true;

            SaveCommand.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(Index));
            OnPropertyChanged(nameof(CurrentReservationDto));
            OnPropertyChanged(nameof(DetailsVisibility));
        }

        public bool CheckInput()
        {
            return true;
        }

        public void SelectedIndexChanged()
        {
            if (Index >= 0)
            {
                CurrentReservationDto = new ReservationDto(ReservationDtos[Index]);
                DetailsVisibility = true;
                OnPropertyChanged(nameof(CurrentReservationDto));
                OnPropertyChanged(nameof(DetailsVisibility));
            }

            DeleteCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        public void SortingByReservation()
        {
            ButtonStateVon = ButtonState.Inactive;
            ButtonStateBis = ButtonState.Inactive;
            ButtonStateAuto = ButtonState.Inactive;
            ButtonStateKunde = ButtonState.Inactive;

            switch (ButtonStateReservation)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateReservation = ButtonState.Ascending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderBy(k => k.ReservationsNr));
                    break;

                case ButtonState.Ascending:
                    ButtonStateReservation = ButtonState.Descending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderByDescending(k => k.ReservationsNr));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByVon()
        {
            ButtonStateReservation = ButtonState.Inactive;
            ButtonStateBis = ButtonState.Inactive;
            ButtonStateAuto = ButtonState.Inactive;
            ButtonStateKunde = ButtonState.Inactive;

            switch (ButtonStateVon)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateVon = ButtonState.Ascending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderBy(k => k.Von));
                    break;

                case ButtonState.Ascending:
                    ButtonStateVon = ButtonState.Descending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderByDescending(k => k.Von));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByBis()
        {
            ButtonStateReservation = ButtonState.Inactive;
            ButtonStateVon = ButtonState.Inactive;
            ButtonStateAuto = ButtonState.Inactive;
            ButtonStateKunde = ButtonState.Inactive;

            switch (ButtonStateBis)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateBis = ButtonState.Ascending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderBy(k => k.Bis));
                    break;

                case ButtonState.Ascending:
                    ButtonStateBis = ButtonState.Descending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderByDescending(k => k.Bis));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByAuto()
        {
            ButtonStateVon = ButtonState.Inactive;
            ButtonStateBis = ButtonState.Inactive;
            ButtonStateKunde = ButtonState.Inactive;
            ButtonStateReservation = ButtonState.Inactive;

            switch (ButtonStateAuto)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateAuto = ButtonState.Ascending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderBy(k => k.Auto.Marke));
                    break;

                case ButtonState.Ascending:
                    ButtonStateAuto = ButtonState.Descending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderByDescending(k => k.Auto.Marke));
                    break;
            }

            SortingOnPropertyChanged();
        }

        public void SortingByKunde()
        {
            ButtonStateVon = ButtonState.Inactive;
            ButtonStateBis = ButtonState.Inactive;
            ButtonStateAuto = ButtonState.Inactive;
            ButtonStateReservation = ButtonState.Inactive;

            switch (ButtonStateKunde)
            {
                case ButtonState.Inactive:
                case ButtonState.Descending:
                    ButtonStateKunde = ButtonState.Ascending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderBy(k => k.Kunde.Nachname));
                    break;

                case ButtonState.Ascending:
                    ButtonStateKunde = ButtonState.Descending;
                    ReservationDtos = new List<ReservationDto>(ReservationDtos.OrderByDescending(k => k.Kunde.Nachname));
                    break;
            }
            SortingOnPropertyChanged();
        }
        

        public void SortingOnPropertyChanged()
        {
            OnPropertyChanged(nameof(ButtonStateReservation));
            OnPropertyChanged(nameof(ButtonStateVon));
            OnPropertyChanged(nameof(ButtonStateBis));
            OnPropertyChanged(nameof(ReservationDtos));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
