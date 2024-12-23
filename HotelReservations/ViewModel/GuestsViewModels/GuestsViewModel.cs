using HotelReservations.Model;
using HotelReservations.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data; // Necesită această referință pentru ICollectionView

namespace HotelReservations.ViewModels
{
    public class GuestsViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private ObservableCollection<Guest> _guestList;
        private ICollectionView _guestsView;
        private Guest _selectedGuest;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterGuests();
                }
            }
        }

        public ObservableCollection<Guest> GuestList
        {
            get => _guestList;
            set
            {
                _guestList = value;
                OnPropertyChanged(nameof(GuestList));
            }
        }

        public ICollectionView GuestsView
        {
            get => _guestsView;
            private set
            {
                _guestsView = value;
                OnPropertyChanged(nameof(GuestsView));
            }
        }

        public Guest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if (_selectedGuest != value)
                {
                    _selectedGuest = value;
                    OnPropertyChanged(nameof(SelectedGuest));
                }
            }
        }

        public ICommand EditGuestCommand { get; }

        public GuestsViewModel()
        {
            LoadGuests();
            EditGuestCommand = new RelayCommand(ExecuteEditGuest, CanExecuteEditGuest);
        }

        private void LoadGuests()
        {
            using (var context = new HotelDbContext())
            {
                var guests = context.Guests.ToList();
                var reservations = context.Reservations.ToList();

                foreach (var guest in guests)
                {
                    guest.Reservation = reservations.FirstOrDefault(r => r.Id == guest.ReservationId);
                }

                GuestList = new ObservableCollection<Guest>(guests);

                // Creează ICollectionView pentru filtrare și sortare
                GuestsView = CollectionViewSource.GetDefaultView(GuestList);
            }
        }

        private void ExecuteEditGuest(object parameter)
        {
            if (SelectedGuest != null)
            {
                var editGuestWindow = new EditGuest(SelectedGuest);
                bool? result = editGuestWindow.ShowDialog();

                // Reîmprospătează lista după editare
                if (result == true)
                {
                    LoadGuests();
                    FilterGuests();
                }
            }
        }

        private bool CanExecuteEditGuest(object parameter)
        {
            return SelectedGuest != null;
        }

        private void FilterGuests()
        {
            if (GuestsView != null)
            {
                GuestsView.Filter = (obj) =>
                {
                    var guest = obj as Guest;
                    return string.IsNullOrEmpty(SearchText) ||
                           guest.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand pentru a implementa ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
