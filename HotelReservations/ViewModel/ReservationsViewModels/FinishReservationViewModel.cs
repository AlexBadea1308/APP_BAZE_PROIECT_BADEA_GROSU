using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HotelReservations.Model;
using System;
using System.Linq;
using System.Data.Entity;

namespace HotelReservations.ViewModels
{
    public class FinishReservationViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly HotelDbContext _context;
        private readonly Reservation _reservationToFinish;

        public ICommand FinishCommand { get; }
        public ICommand CancelCommand { get; }

        public FinishReservationViewModel(Reservation reservation)
        {
            _context = new HotelDbContext();
            _reservationToFinish = reservation;

            FinishCommand = new RelayCommand(_ => ExecuteFinish());
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
        }

        private void ExecuteFinish()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Se asigură că obiectul este atașat la context
                    _context.Reservations.Attach(_reservationToFinish);

                    var guestsToDelete = _context.Guests
                        .Where(g => g.ReservationId == _reservationToFinish.Id)
                        .ToList();

                    // Șterge oaspeții
                    foreach (var guest in guestsToDelete)
                    {
                        _context.Guests.Remove(guest);
                    }

                    double totalPrice = _reservationToFinish.TotalPrice;

                    // Șterge rezervația
                    _context.Reservations.Remove(_reservationToFinish);

                    // Salvează modificările
                    _context.SaveChanges();
                    transaction.Commit();

                    // Afișează informațiile de plată
                    MessageBox.Show(
                        $"You must pay: {totalPrice:C}",
                        "Payment Information",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Închide fereastra
                    RequestClose?.Invoke(this, true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(
                        $"An error occurred while finishing the reservation: {ex.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }


        private void ExecuteCancel()
        {
            RequestClose?.Invoke(this, false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<bool>? RequestClose;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
