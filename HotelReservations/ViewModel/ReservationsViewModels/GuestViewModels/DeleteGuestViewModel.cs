using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HotelReservations.Model;
using HotelReservations.Service;

namespace HotelReservations.ViewModel
{
    public class DeleteGuestViewModel : ViewModelBase
    {
        private readonly GuestService _guestService;
        private readonly Guest _guestToDelete;
        private readonly ObservableCollection<Guest> _guestsList;
        private readonly Action _closeAction;
        private string _confirmationMessage;

        public DeleteGuestViewModel(Guest guest, ObservableCollection<Guest> guestsList, Action closeAction)
        {
            _guestService = new GuestService();
            _guestToDelete = guest;
            _guestsList = guestsList;
            _closeAction = closeAction;
            _confirmationMessage = $"Are you sure you want to delete guest {guest.Name} {guest.Surname}?";

            DeleteCommand = new RelayCommand(ExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public string ConfirmationMessage
        {
            get => _confirmationMessage;
            set => SetProperty(ref _confirmationMessage, value);
        }

        private void ExecuteDelete(object parameter)
        {
            try
            {
                _guestService.DeleteGuestFromDatabase(_guestToDelete);
                _guestsList.Remove(_guestToDelete);
                _closeAction();
            }
            catch (Exception ex)
            {
                ConfirmationMessage = $"Error deleting guest: {ex.Message}";
            }
        }

        private void ExecuteCancel(object parameter)
        {
            _closeAction();
        }
    }
}