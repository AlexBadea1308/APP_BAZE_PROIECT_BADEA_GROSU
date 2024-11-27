using HotelReservations.Model;
using HotelReservations.Repositories;
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
            _reservationService = new ReservationService(); 
            DataContext = this;
            FillData(); // Incarcam rezervarile Hotel
        }

        public void FillData()
        {
            // Obtinem toate rezervarile din baza de date
            var loadedReservationRepository = _reservationService.reservationRepository.GetAll();
            if (loadedReservationRepository != null)
            {
                Hotel.GetInstance().Reservations = loadedReservationRepository;
            }
            var reservations = Hotel.GetInstance().Reservations.ToList();

            view = CollectionViewSource.GetDefaultView(reservations);
            view.Filter = DoFilter; //Aplicam filtrul pentru validarea datelor

            // Setam lista de rezervari pentru DataGrid
            ReservationsDataGrid.ItemsSource = null;
            ReservationsDataGrid.ItemsSource = reservations;

            // Asiguram sincronizarea cu item-ul selectat
            ReservationsDataGrid.IsSynchronizedWithCurrentItem = true;

            // Deselectam orice selectie anterioara
            ReservationsDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object resObject)
        {
            var res = resObject as Reservation;
            if (res == null) return false;

            // Luam valorile din campurile Data Gridului
            var roomNumberSearchParam = RoomNumberSearchTextBox.Text.Trim();
            var startDateSearchParam = StartDateSearchTextBox.Text.Trim();
            var endDateSearchParam = EndDateSearchTextBox.Text.Trim();

            // Verificam daca sunt valide
            bool isRoomNumberMatch = string.IsNullOrEmpty(roomNumberSearchParam) ||
                                     res.RoomNumber.IndexOf(roomNumberSearchParam, StringComparison.OrdinalIgnoreCase) >= 0;

            bool isStartDateMatch = string.IsNullOrEmpty(startDateSearchParam) ||
                                    res.StartDateTime.ToShortDateString().Contains(startDateSearchParam);

            bool isEndDateMatch = string.IsNullOrEmpty(endDateSearchParam) ||
                                  res.EndDateTime.ToShortDateString().Contains(endDateSearchParam);

            return isRoomNumberMatch && isStartDateMatch && isEndDateMatch;
        }

        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddReservations();
            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData(); //Daca am reusit sa inseram cu succes o rezervare reincarcam tabelul cu rezervari
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
                FillData();  //Daca am reusit sa stergem cu succes o rezervare reincarcam tabelul cu rezervari
            }
            ShowDialog();
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
                FillData();  //Daca am reusit sa finalizam cu succes o rezervare reincarcam tabelul cu rezervari
            }
            ShowDialog();
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh(); // Refresh la tabel cand facem search
        }

    }
}
