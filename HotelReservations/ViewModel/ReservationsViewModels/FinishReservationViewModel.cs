using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HotelReservations.Model;
using HotelReservations.Service;
using System;

namespace HotelReservations.ViewModels
{
    public class FinishReservationViewModel : INotifyPropertyChanged
    {
        private readonly GuestService _guestService;
        private readonly ReservationService _reservationService;
        private readonly Reservation _reservationToFinish;

        public ICommand FinishCommand { get; }
        public ICommand CancelCommand { get; }

        public FinishReservationViewModel(Reservation reservation)
        {
            _guestService = new GuestService();
            _reservationService = new ReservationService();
            _reservationToFinish = reservation;

            FinishCommand = new RelayCommand(ExecuteFinish);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteFinish(object? parameter)
        {
            var guestsToUpdate = _guestService.guestRepository.GetGuestsByReservationId(_reservationToFinish.Id);

            // Update guests list
            foreach (Guest guest in guestsToUpdate)
            {
                _guestService.guestRepository.Delete(guest.ReservationId);  // Delete guest corresponding to the reservation
            }

            _reservationService.GetReservationRepository().Delete(_reservationToFinish.Id);
            var totalPrice = _reservationService.FinishReservation(_reservationToFinish);

            MessageBox.Show($"You must pay: {totalPrice}", "Payment Information", MessageBoxButton.OK, MessageBoxImage.Information);
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