using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for Reservations.xaml
    /// </summary>
    public partial class Reservations : Window
    {
        private ICollectionView view;
        public Reservations()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            var reservationService = new ReservationService();
            var reservations = Hotel.GetInstance().Reservations.Where(res => res.IsActive).ToList();

            view = CollectionViewSource.GetDefaultView(reservations);
            view.Filter = DoFilter;

            ReservationsDataGrid.ItemsSource = null;
            ReservationsDataGrid.ItemsSource = view;
            ReservationsDataGrid.IsSynchronizedWithCurrentItem = true;
            ReservationsDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object resObject)
        {
            var res = resObject as Reservation;

            var roomNumberSearchParam = RoomNumberSearchTextBox.Text;
            var startDateSearchParam = StartDateSearchTextBox.Text;
            var endDateSearchParam = EndDateSearchTextBox.Text;

            bool isRoomNumberMatch = res.RoomNumber.Contains(roomNumberSearchParam);
            bool isStartDateMatch = res.StartDateTime.ToShortDateString().Contains(startDateSearchParam);
            bool isEndDateMatch = res.EndDateTime.ToShortDateString().Contains(endDateSearchParam);

            return isRoomNumberMatch && isStartDateMatch && isEndDateMatch;
        }

        private void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var addReservationWindow = new AddEditReservations();
            Hide();
            if (addReservationWindow.ShowDialog() == true)
            {
                FillData();
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
                FillData();
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
                FillData();
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
                FillData();
            }
            Show();
        }

        private void CheckActiveReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var showActiveReservationWindow = new ActiveReservations();
            Hide();
            if (showActiveReservationWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void CheckFinishedReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var showFinishedReservationWindow = new FinishedReservations();
            Hide();
            if (showFinishedReservationWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void ReservationsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
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
