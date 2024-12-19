using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HotelReservations.Model;
using HotelReservations.Service;
using System;

namespace HotelReservations.ViewModels
{
    public class DeleteReservationViewModel : INotifyPropertyChanged
    {
        private readonly GuestService _guestService;
        private readonly ReservationService _reservationService;
        private readonly Reservation _reservationToDelete;

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteReservationViewModel(Reservation reservation)
        {
            _guestService = new GuestService();
            _reservationService = new ReservationService();
            _reservationToDelete = reservation;

            DeleteCommand = new RelayCommand(ExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteDelete(object? parameter)
        {
            // Find guests in database with this ReservationId
            var guestsToUpdate = _guestService.guestRepository.GetGuestsByReservationId(_reservationToDelete.Id);

            // Update found guests
            foreach (Guest guest in guestsToUpdate)
            {
                _guestService.guestRepository.Delete(guest.ReservationId);  // Delete guest with this ReservationId
            }

            _reservationService.DeleteRezervationFromDatabase(_reservationToDelete);
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