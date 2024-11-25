using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.Windows
{
    public partial class Reservations : Window
    {
        private ReservationService _reservationService;
        private ICollectionView view;

        public Reservations()
        {
            InitializeComponent();
            _reservationService = new ReservationService(); // Instanțiem serviciul
            DataContext = this;
            FillData(); // Încărcăm rezervările
        }

        public void FillData()
        {
            // Obținem toate rezervările din baza de date
            var reservations = Hotel.GetInstance().Reservations.ToList();

            // Dacă vrei să aplici un filtru, folosești o metodă similară cu `DoFilter` din exemplul tău
            view = CollectionViewSource.GetDefaultView(reservations);
            view.Filter = DoFilter; // Aplicăm filtrul (dacă ai unul definit)

            // Setăm lista de rezervări pentru DataGrid
            ReservationsDataGrid.ItemsSource = null;
            ReservationsDataGrid.ItemsSource = reservations;

            // Asigurăm sincronizarea cu item-ul selectat
            ReservationsDataGrid.IsSynchronizedWithCurrentItem = true;

            // Deselează orice selecție anterioară
            ReservationsDataGrid.SelectedItem = null;
        }




        private bool DoFilter(object resObject)
        {
            var res = resObject as Reservation;
            if (res == null) return false;

            // Obținem valori din câmpurile de căutare
            var roomNumberSearchParam = RoomNumberSearchTextBox.Text.Trim();
            var startDateSearchParam = StartDateSearchTextBox.Text.Trim();
            var endDateSearchParam = EndDateSearchTextBox.Text.Trim();

            // Verificări pe baza căutării
            bool isRoomNumberMatch = string.IsNullOrEmpty(roomNumberSearchParam) ||
                                     res.RoomNumber.IndexOf(roomNumberSearchParam, StringComparison.OrdinalIgnoreCase) >= 0;

            bool isStartDateMatch = string.IsNullOrEmpty(startDateSearchParam) ||
                                    res.StartDateTime.ToShortDateString().Contains(startDateSearchParam);

            bool isEndDateMatch = string.IsNullOrEmpty(endDateSearchParam) ||
                                  res.EndDateTime.ToShortDateString().Contains(endDateSearchParam);

            // Returnăm true doar dacă toate condițiile sunt îndeplinite
            return isRoomNumberMatch && isStartDateMatch && isEndDateMatch;
        }

        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddEditReservations();
            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData(); // Încărcăm din nou datele după ce adăugăm o rezervare
            }
            Show();
        }

        private void EditReservationButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation chosenReservation = (Reservation)ReservationsDataGrid.SelectedItem;
            if (chosenReservation == null)
            {
                MessageBox.Show("Please select a Reservation.", "Select Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editReservationWindow = new AddEditReservations(chosenReservation);
            Hide();
            if (editReservationWindow.ShowDialog() == true)
            {
                FillData(); // Încărcăm din nou datele după ce edităm o rezervare
            }
            Show();
        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenReservation = (Reservation)ReservationsDataGrid.SelectedItem;
            if (chosenReservation == null)
            {
                MessageBox.Show("Please select a Reservation.", "Select Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deleteReservationsWindow = new DeleteReservations(chosenReservation);
            Hide();
            if (deleteReservationsWindow.ShowDialog() == true)
            {
                FillData(); // Încărcăm din nou datele după ce ștergem o rezervare
            }
            Show();
        }

        private void FinishReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenReservation = (Reservation)ReservationsDataGrid.SelectedItem;
            if (chosenReservation == null)
            {
                MessageBox.Show("Please select a Reservation.", "Select Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var finishReservationsWindow = new FinishReservation(chosenReservation);
            Hide();
            if (finishReservationsWindow.ShowDialog() == true)
            {
                FillData(); // Încărcăm din nou datele după ce finalizăm o rezervare
            }
            Show();
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh(); // Reîmprospătăm datele pentru a aplica filtrul
        }

        private void ReservationsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Ascundem coloanele inutile
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "Guests".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "IsFinished".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
