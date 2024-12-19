using HotelReservations.Model;
using HotelReservations.Service;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace HotelReservations.ViewModel
{
    public class EditGuestViewModel : ViewModelBase, IDataErrorInfo
    {
        private Guest _contextGuest;
        private GuestService _guestService;
        private bool _isEditing;

        // Parameterless constructor required for XAML
        public EditGuestViewModel()
        {
            _guestService = new GuestService();
            ContextGuest = new Guest();
            IsEditing = false;

            InitializeCommands();
        }

        // Constructor for programmatic initialization
        public EditGuestViewModel(Guest? guest = null)
        {
            _guestService = new GuestService();

            // If no guest provided, create a new one
            ContextGuest = guest ?? new Guest();
            IsEditing = guest != null;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        public Guest ContextGuest
        {
            get => _contextGuest;
            set
            {
                _contextGuest = value;
                OnPropertyChanged(nameof(ContextGuest));
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private bool CanExecuteSave(object parameter)
        {
            return !string.IsNullOrWhiteSpace(ContextGuest.Name) &&
                   !string.IsNullOrWhiteSpace(ContextGuest.Surname) &&
                   !string.IsNullOrEmpty(ContextGuest.CNP) &&
                   ContextGuest.CNP.Length == 13 &&
                   ContextGuest.CNP.All(char.IsDigit);
        }

        private void ExecuteSave(object parameter)
        {
            _guestService.SaveGuest(ContextGuest);
            CloseWindow(true);
        }

        private void ExecuteCancel(object parameter)
        {
            CloseWindow(false);
        }

        private void CloseWindow(bool result)
        {
            OnCloseRequested?.Invoke(this, result);
        }

        // IDataErrorInfo implementation for validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ContextGuest.Name):
                        if (string.IsNullOrWhiteSpace(ContextGuest.Name))
                            return "Name cannot be empty";
                        break;
                    case nameof(ContextGuest.Surname):
                        if (string.IsNullOrWhiteSpace(ContextGuest.Surname))
                            return "Surname cannot be empty";
                        break;
                    case nameof(ContextGuest.CNP):
                        if (string.IsNullOrEmpty(ContextGuest.CNP) ||
                            ContextGuest.CNP.Length != 13 ||
                            !ContextGuest.CNP.All(char.IsDigit))
                            return "CNP must be a 13-digit number";
                        break;
                }
                return null;
            }
        }

        public event CloseRequestedEventHandler OnCloseRequested;
    }
}