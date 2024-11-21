//using HotelReservations.Model;
//using HotelReservations.Repository;
//using HotelReservations.Service;
//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Input;

//namespace HotelReservations.Windows
//{
//    /// <summary>
//    /// Interaction logic for AddEditReservations.xaml
//    /// </summary>
//    public partial class AddEditReservations : Window
//    {
//        private ReservationService reservationService;
//        private Reservation contextReservation;
//        private ICollectionView view;
//        public bool isEditing;

//        // this will be for creating reservation by picking room
//        public AddEditReservations(Reservation? res = null)
//        {
//            InitializeComponent();

//            if (res == null)
//            {
//                contextReservation = new Reservation();
//                isEditing = false;
//                AddGuestButton.Visibility = Visibility.Visible;
//            }
//            else
//            {
//                contextReservation = res.Clone();
//                isEditing = true;
//                AddGuestButton.Visibility = Visibility.Hidden;
//            }

//            reservationService = new ReservationService();
//            AdjustWindow(res);
//            FillData(res);
//            this.DataContext = contextReservation;
//        }

//        private bool DoFilter(object roomObject)
//        {
//            var room = roomObject as Room;
//            var reservations = Hotel.GetInstance().Reservations.Where(res => res.RoomNumber == room.RoomNumber);

//            var roomTypeValue = RoomTypeComboBox.SelectedValue;
//            DateTime? startDate = CheckStartDate.SelectedDate;
//            DateTime? endDate = CheckEndDate.SelectedDate;

//            /* you might be a little bit confused here so i will explain short
//            im checking is room type even selected so if its not selected i will just return true */

//            if (roomTypeValue != null && roomTypeValue != "")
//            {
//                // if its selected then we will compare and also we will iterate over loop to check are dates overlapping(means available dates if not overlap)
//                bool roomType = room.RoomType.ToString() == roomTypeValue.ToString();

//                if(roomType == false)
//                {
//                    return false;
//                }

//                foreach (Reservation r in reservations)
//                {
//                    if (AreDatesOverlapping(startDate, endDate, r.StartDateTime, r.EndDateTime))
//                    {
//                        return false; // If there is an overlap, return false
//                    }
//                }

//            }

//            return true;
//        }

//        // this is just helper function to check are dates overlapping
//        private bool AreDatesOverlapping(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2)
//        {
//            if (!start1.HasValue || !end1.HasValue || !start2.HasValue || !end2.HasValue)
//            {
//                return false; // if something is null, its no overlapped
//            }

//            if (start1 >= start2 && start1 <= end2)
//            {
//                return true;
//            }
//            if (end1 >= start2 && end1 <= end2)
//            {
//                return true;
//            }

//            return false;
//        }

//        private void AdjustWindow(Reservation? res = null)
//        {
//            var roomTypeList = Hotel.GetInstance().RoomTypes.Where(roomType => roomType.IsActive).ToList();
//            RoomTypeComboBox.ItemsSource = roomTypeList;

//            if (res != null)
//            {
//                Title = "Edit Reservation";
//            }
//            else
//            {
//                Title = "Add Reservation";
//            }
//        }

//        public void FillData(Reservation? res = null, Guest? newGuest = null)
//        {
//            var guests = Hotel.GetInstance().Guests.Where(g => g.IsActive && g.ReservationId == 0);

//            if (isEditing)
//            {
//                guests = Hotel.GetInstance().Guests?.Where(g => g.ReservationId == contextReservation.Id && g.IsActive) ?? Enumerable.Empty<Guest>();
//            }

//            if (newGuest != null)
//            {
//                guests.Prepend(newGuest);
//            }

//            var rooms = Hotel.GetInstance().Rooms.Where(room => room.IsActive).ToList();

//            view = CollectionViewSource.GetDefaultView(guests);
//            GuestsDataGrid.ItemsSource = null;
//            GuestsDataGrid.ItemsSource = view;
//            GuestsDataGrid.IsSynchronizedWithCurrentItem = true;
//            GuestsDataGrid.SelectedItem = null;

//            view = CollectionViewSource.GetDefaultView(rooms);
//            view.Filter = DoFilter;
//            RoomsDataGrid.ItemsSource = null;
//            RoomsDataGrid.ItemsSource = view;
//            RoomsDataGrid.IsSynchronizedWithCurrentItem = true;
//            RoomsDataGrid.SelectedItem = null;

//        }

//        private void RoomsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
//        {
//            if (e.PropertyName.ToLower() == "IsActive".ToLower())
//            {
//                e.Column.Visibility = Visibility.Collapsed;
//            }
//        }

//        private void GuestsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
//        {
//            if (e.PropertyName.ToLower() == "IsActive".ToLower())
//            {
//                e.Column.Visibility = Visibility.Collapsed;
//            }

//            if (e.PropertyName.ToLower() == "Id".ToLower())
//            {
//                e.Column.Visibility = Visibility.Collapsed;
//            } 

//            if (e.PropertyName.ToLower() == "ReservationId".ToLower())
//            {
//                e.Column.Visibility = Visibility.Collapsed;
//            }
//        }

//        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
//        {
//            var addGuestWindow = new AddEditGuest();
//            Hide();
//            if (addGuestWindow.ShowDialog() == true)
//            {
//                FillData();
//            }
//            Show();
//        }

//        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
//        {
//            Guest chosenGuest = (Guest)GuestsDataGrid.SelectedItem;
//            if (chosenGuest == null)
//            {
//                MessageBox.Show("Please select a Guest.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
//                return;
//            }
//            var editGuestWindow = new AddEditGuest(chosenGuest);
//            Hide();
//            if (editGuestWindow.ShowDialog() == true)
//            {
//                FillData();
//            }
//            Show();
//        }

//        private void DeleteGuestButton_Click(object sender, RoutedEventArgs e)
//        {
//            var chosenGuest = (Guest)GuestsDataGrid.SelectedItem;
//            if (chosenGuest == null)
//            {
//                MessageBox.Show("Please select a Guest.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
//                return;
//            }
//            var deleteGuestWindow = new DeleteGuest(chosenGuest);
//            Hide();
//            if (deleteGuestWindow.ShowDialog() == true)
//            {
//                FillData();
//            }
//            Show();
//        }

//        private void SaveButton_Click(object sender, RoutedEventArgs e)
//        {
//            Room selectedRoom = RoomsDataGrid.SelectedItem as Room;
//            DateTime? startDate = StartDatePicker.SelectedDate;
//            DateTime? endDate = EndDatePicker.SelectedDate;

//            if (selectedRoom == null)
//            {
//                MessageBox.Show("Please select a room.");
//                return;
//            }

//            if (!startDate.HasValue || !endDate.HasValue)
//            {
//                MessageBox.Show("Please select both start and end dates.");
//                return;
//            }

//            // Not in the past
//            if (startDate < DateTime.Today)
//            {
//                MessageBox.Show("Start date cannot be in the past.");
//                return;
//            }

//            // Is Start date less than or equal to the end date
//            if (startDate > endDate)
//            {
//                MessageBox.Show("Start date must be equal or earlier than end date.");
//                return;
//            }

//            var reservations = Hotel.GetInstance().Reservations;
//            foreach (Reservation r in reservations)
//            {
//                if(selectedRoom.RoomNumber == r.RoomNumber && r.IsFinished == false && r.IsActive == true)
//                {
//                    if (AreDatesOverlapping(startDate, endDate, r.StartDateTime, r.EndDateTime))
//                    {
//                        MessageBox.Show("Try with different dates, these are overlapping. " + r.StartDateTime + "-" + r.EndDateTime);
//                        return;
//                    }
//                }

//            }

//            reservationService.SaveReservation(contextReservation, selectedRoom);
//            DialogResult = true;
//            Close();
//        }

//        private void CancelButton_Click(object sender, RoutedEventArgs e)
//        {
//            DialogResult = false;
//            Close();
//        }

//        private void CheckAvailableRoomsButton_Click(object sender, RoutedEventArgs e)
//        {
//            view.Refresh();
//        }

//    }
//}

using HotelReservations.Model;
using HotelReservations.Repository;
using HotelReservations.Service;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditReservations.xaml
    /// </summary>
    public partial class AddEditReservations : Window
    {
        private ReservationService reservationService;
        private Reservation contextReservation;
        private ICollectionView view;
        public bool isEditing;

        // Constructor for creating or editing a reservation
        public AddEditReservations(Reservation? res = null)
        {
            InitializeComponent();

            if (res == null)
            {
                contextReservation = new Reservation();
                isEditing = false;
                AddGuestButton.Visibility = Visibility.Visible;
            }
            else
            {
                contextReservation = res.Clone();
                isEditing = true;
                AddGuestButton.Visibility = Visibility.Hidden;
            }

            reservationService = new ReservationService();
            AdjustWindow(res);
            FillData(res);
            this.DataContext = contextReservation;
        }

        private bool DoFilter(object roomObject)
        {
            var room = roomObject as Room;
            var reservations = Hotel.GetInstance().Reservations.Where(res => res.RoomNumber == room.RoomNumber);

            var roomTypeValue = RoomTypeComboBox.SelectedValue;
            DateTime? startDate = CheckStartDate.SelectedDate;
            DateTime? endDate = CheckEndDate.SelectedDate;

            if (roomTypeValue != null && roomTypeValue != "")
            {
                bool roomType = room.RoomType.ToString() == roomTypeValue.ToString();

                if (!roomType)
                {
                    return false;
                }

                foreach (Reservation r in reservations)
                {
                    if (AreDatesOverlapping(startDate, endDate, r.StartDateTime, r.EndDateTime))
                    {
                        return false; // If there is an overlap, return false
                    }
                }
            }

            return true;
        }

        private bool AreDatesOverlapping(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2)
        {
            if (!start1.HasValue || !end1.HasValue || !start2.HasValue || !end2.HasValue)
            {
                return false; // If any dates are null, consider them not overlapping
            }

            return (start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2);
        }

        private void AdjustWindow(Reservation? res = null)
        {
            var roomTypeList = Hotel.GetInstance().RoomTypes.Where(roomType => roomType.IsActive).ToList();
            RoomTypeComboBox.ItemsSource = roomTypeList;

            Title = res != null ? "Edit Reservation" : "Add Reservation";
        }

        public void FillData(Reservation? res = null, Guest? newGuest = null)
        {
            var guests = Hotel.GetInstance().Guests.Where(g => g.IsActive && g.ReservationId == 0);

            if (isEditing)
            {
                guests = Hotel.GetInstance().Guests?.Where(g => g.ReservationId == contextReservation.Id && g.IsActive) ?? Enumerable.Empty<Guest>();
            }

            if (newGuest != null)
            {
                guests = guests.Prepend(newGuest); // Temporarily add the new guest
            }

            var rooms = Hotel.GetInstance().Rooms.Where(room => room.IsActive).ToList();

            view = CollectionViewSource.GetDefaultView(guests);
            GuestsDataGrid.ItemsSource = null;
            GuestsDataGrid.ItemsSource = view;
            GuestsDataGrid.IsSynchronizedWithCurrentItem = true;
            GuestsDataGrid.SelectedItem = null;

            view = CollectionViewSource.GetDefaultView(rooms);
            view.Filter = DoFilter;
            RoomsDataGrid.ItemsSource = null;
            RoomsDataGrid.ItemsSource = view;
            RoomsDataGrid.IsSynchronizedWithCurrentItem = true;
            RoomsDataGrid.SelectedItem = null;
        }

        private void RoomsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void GuestsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("IsActive", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.Equals("ReservationId", StringComparison.OrdinalIgnoreCase))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the AddEditGuest window without passing contextReservation if it's not needed
            var addGuestWindow = new AddEditGuest();

            Hide();

            // ShowDialog to handle the addition
            if (addGuestWindow.ShowDialog() == true)
            {
                // Check if the guest was successfully saved
                var newGuest = addGuestWindow.SavedGuest;

                if (newGuest != null)
                {
                    // Add the new guest and refresh data
                    FillData(contextReservation, newGuest);
                }
                else
                {
                    MessageBox.Show("Failed to add guest. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            Show();
        }


        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenGuest = GuestsDataGrid.SelectedItem as Guest;
            if (chosenGuest == null)
            {
                MessageBox.Show("Please select a Guest.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editGuestWindow = new AddEditGuest(chosenGuest);
            Hide();
            if (editGuestWindow.ShowDialog() == true)
            {
                FillData(contextReservation);
            }
            Show();
        }

        private void DeleteGuestButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenGuest = GuestsDataGrid.SelectedItem as Guest;
            if (chosenGuest == null)
            {
                MessageBox.Show("Please select a Guest.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deleteGuestWindow = new DeleteGuest(chosenGuest);
            Hide();
            if (deleteGuestWindow.ShowDialog() == true)
            {
                FillData(contextReservation);
            }
            Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = RoomsDataGrid.SelectedItem as Room;
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            if (selectedRoom == null)
            {
                MessageBox.Show("Please select a room.");
                return;
            }

            if (!startDate.HasValue || !endDate.HasValue)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            if (startDate < DateTime.Today)
            {
                MessageBox.Show("Start date cannot be in the past.");
                return;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be equal to or earlier than end date.");
                return;
            }

            var reservations = Hotel.GetInstance().Reservations;
            foreach (Reservation r in reservations)
            {
                if (selectedRoom.RoomNumber == r.RoomNumber && !r.IsFinished && r.IsActive)
                {
                    if (AreDatesOverlapping(startDate, endDate, r.StartDateTime, r.EndDateTime))
                    {
                        MessageBox.Show("Try with different dates, these are overlapping: " + r.StartDateTime + " - " + r.EndDateTime);
                        return;
                    }
                }
            }

            reservationService.SaveReservation(contextReservation, selectedRoom);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CheckAvailableRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            view.Refresh();
        }
    }
}

