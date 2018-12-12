using System;
using System.Windows.Input;

namespace AutoReservation.UI
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException();
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T) parameter) ?? true;
        public void Execute(object parameter) => _execute((T) parameter);

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}