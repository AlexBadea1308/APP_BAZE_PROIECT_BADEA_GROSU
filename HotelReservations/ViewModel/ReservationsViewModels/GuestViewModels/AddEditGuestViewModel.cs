using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows;
using HotelReservations.Model;
using HotelReservations.Service;
using System;
using HotelReservations.Windows;
using System.Collections.ObjectModel;

namespace HotelReservations.ViewModels
{
    public class AddEditGuestViewModel : INotifyPropertyChanged
    {
        private Guest _guest;
        private bool _isEditing;
        public bool IsCNPReadOnly => _isEditing;
        public string WindowTitle => "Add Guest";

        public Guest SavedGuest
        {
            get => _guest;
            set
            {
                _guest = value;
                OnPropertyChanged(nameof(Prices));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditGuestViewModel(Guest? guest = null)
        {
            _guest = guest ?? new Guest();
            _isEditing = guest != null;

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanExecuteSave(object? parameter)
        {
            return true;
        }

        private void ExecuteSave(object? parameter)
        {
            // Validation for Name
            if (string.IsNullOrWhiteSpace(SavedGuest.Name))
            {
                MessageBox.Show("Name can't be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Validation for Surname
            if (string.IsNullOrWhiteSpace(SavedGuest.Surname))
            {
                MessageBox.Show("Surname can't be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Validation for CNP
            if (string.IsNullOrEmpty(SavedGuest.CNP) || SavedGuest.CNP.Length != 13 || !SavedGuest.CNP.All(char.IsDigit))
            {
                MessageBox.Show("CNP must be a 13-digit number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!_isEditing)
            {
                SavedGuest = _guest;
            }

            RequestClose?.Invoke(this, true);
        }

        private void ExecuteCancel(object? parameter)
        {
            RequestClose?.Invoke(this, false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<bool>? RequestClose;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}