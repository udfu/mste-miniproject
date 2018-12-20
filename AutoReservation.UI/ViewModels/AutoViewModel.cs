using AutoReservation.Common.DataTransferObjects;
using AutoReservation.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoReservation.UI.ViewModels
{
    public class AutoViewModel : INotifyPropertyChanged
    {
        public List<AutoDto> Autos
        {
            get { return _autos; }
            set
            {
                _autos = value;
                OnPropertyChanged();
            }
        }

        public AutoDto ActiveAuto
        {
            get { return _activeAuto; }
            set
            {
                _activeAuto = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;

                if (SelectedIndex >= 0)
                {
                    ActiveAuto = new AutoDto(Autos[SelectedIndex]);
                    IsDetailsVisible = true;
                }

                DeleteCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        public bool IsDetailsVisible
        {
            get { return _isDetailsVisible; }
            set
            {
                _isDetailsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool IsInputValid => !string.IsNullOrEmpty(ActiveAuto.Marke) && ActiveAuto.Tagestarif != 0;

        public RelayCommand AddCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<AutoKlasse> AutoKlasseValues
        {
            get { return Enum.GetValues(typeof(AutoKlasse)).Cast<AutoKlasse>(); }
        }

        private List<AutoDto> _autos;
        private AutoDto _activeAuto;
        private bool _isDetailsVisible = false;
        private int _selectedIndex = -1;

        public AutoViewModel()
        {
            Autos = new List<AutoDto>(AppViewModel.Target.ReadAutoDtos());

            AddCommand = new RelayCommand(() => Add());
            SaveCommand = new RelayCommand(() => Save());
            DeleteCommand = new RelayCommand(() => Delete(), () => ActiveAuto != null);
            RefreshCommand = new RelayCommand(() => Refresh());
        }

        private void Add()
        {
            SelectedIndex = -1;
            ActiveAuto = new AutoDto();
            IsDetailsVisible = true;
        }

        private void Save()
        {
            if (!IsInputValid)
            {
                ShowExclamination("One or more inputs are not valid, pwiz fix.", "Invalid input");
                return;
            }

            for (var i = 0; i < Autos.Count; i++)
            {
                if (ActiveAuto.Id == Autos[i].Id)
                {
                    AppViewModel.Target.UpdateAuto(ActiveAuto);
                    Refresh();
                    return;
                }
            }

            AppViewModel.Target.InsertAuto(ActiveAuto);
            Refresh();
        }

        private void Refresh()
        {
            Autos = new List<AutoDto>(AppViewModel.Target.ReadAutoDtos());
            ActiveAuto = null;
            IsDetailsVisible = false;
            SelectedIndex = -1;
        }

        private void Delete()
        {
            var win = new DialogWindowView();

            if (win.ShowDialog() == true)
            {
                AppViewModel.Target.DeleteAuto(ActiveAuto.Id);
                Refresh();
            }
        }

        private void ShowExclamination(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AutoMock
    {
        public static AutoViewModel AutoShowroom = CreateAutoViewModel();

        private static AutoViewModel CreateAutoViewModel()
        {
            var laVoiture = new AutoDto()
            {
                Id = 1,
                Marke = "Seat",
                Tagestarif = 143,
                AutoKlasse = AutoKlasse.Standard
            };

            var laLimousine = new AutoDto()
            {
                Id = 2,
                Marke = "Lexus",
                Tagestarif = 250,
                Basistarif = 100,
                AutoKlasse = AutoKlasse.Luxusklasse
            };

            return new AutoViewModel()
            {
                Autos = new List<AutoDto>() { laVoiture, laLimousine },
                SelectedIndex = 0
            };
        }
    }
}
