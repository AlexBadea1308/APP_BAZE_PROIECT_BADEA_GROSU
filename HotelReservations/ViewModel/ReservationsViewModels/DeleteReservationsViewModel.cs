using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Linq;
using System.Windows;

namespace HotelReservations.ViewModels
{
    public class DeleteReservationViewModel : INotifyPropertyChanged
    {
        private readonly Reservation _reservationToDelete;

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteReservationViewModel(Reservation reservation)
        {
            _reservationToDelete = reservation;

            DeleteCommand = new RelayCommand(ExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteDelete(object? parameter)
        {
            try
            {
                using (var context = new HotelDbContext())
                {
                    var guestsToUpdate = context.Guests
                        .Where(g => g.ReservationId == _reservationToDelete.Id)
                        .ToList();

                    context.Guests.RemoveRange(guestsToUpdate);

                    var reservationToDelete = context.Reservations
                        .FirstOrDefault(r => r.Id == _reservationToDelete.Id);

                    if (reservationToDelete != null)
                    {
                        context.Reservations.Remove(reservationToDelete);
                    }
                    context.SaveChanges();
                }

                RequestClose?.Invoke(this, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting reservation: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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