

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
    public partial class AddReservations : Window
    {
        private ReservationService reservationService;
        private Reservation contextReservation;
        private GuestService guestService;
        private ICollectionView view;
        public bool isEditing;

        public AddReservations(Reservation? res = null)
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
            guestService=new GuestService();
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

            if (roomTypeValue != null)
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
                        return false; // Daca datele se suprapun returnam false
                    }
                }
            }

            return true;
        }

        private bool AreDatesOverlapping(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2)
        {
            if (!start1.HasValue || !end1.HasValue || !start2.HasValue || !end2.HasValue)
            {
                return false; 
            }

            return (start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2);
        }

        private void AdjustWindow(Reservation? res = null)
        {
            var roomTypeList = Hotel.GetInstance().RoomTypes.ToList();
            RoomTypeComboBox.ItemsSource = roomTypeList;

            Title = "Add Reservation";
        }

        public void FillData(Reservation? res = null, Guest? newGuest = null)
        {
            //Luam lista de guests din baza de date
            var guests = Hotel.GetInstance().Guests.Where(g => g.ReservationId == 0);

            if (isEditing)
            {
                guests = Hotel.GetInstance().Guests?.Where(g => g.ReservationId == contextReservation.Id)
                          ?? Enumerable.Empty<Guest>();
            }

            // Verifica daca guestul nou exista deja in lista actuala
            if (newGuest != null && !guests.Any(g => g.Id == newGuest.Id))
            {
                guests = guests.Prepend(newGuest); // Adauga temporar noul oaspete
            }

            // Reimprospatam DataGrid-ul pentru guests
            view = CollectionViewSource.GetDefaultView(guests.ToList());
            GuestsDataGrid.ItemsSource = view;
            GuestsDataGrid.IsSynchronizedWithCurrentItem = true;

            // Reimprospatam DataGrid-ul pentru camere
            var rooms = Hotel.GetInstance().Rooms.ToList();
            view = CollectionViewSource.GetDefaultView(rooms);
            view.Filter = DoFilter;
            RoomsDataGrid.ItemsSource = view;
            RoomsDataGrid.IsSynchronizedWithCurrentItem = true;
            RoomsDataGrid.SelectedItem = null;
        } 

        private void GuestsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                e.PropertyName.Equals("ReservationId", StringComparison.OrdinalIgnoreCase))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void AddGuestButton_Click(object sender, RoutedEventArgs e)
        {
            
            var addGuestWindow = new AddEditGuest();

            Hide();

            if (addGuestWindow.ShowDialog() == true)
            {
              
                var newGuest = addGuestWindow.SavedGuest;

                if (newGuest != null)
                {
                    FillData(contextReservation, newGuest);
                }
                else
                {
                    MessageBox.Show("Failed to add guest. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ShowDialog();
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
                FillData(contextReservation,chosenGuest);
            }
            ShowDialog();
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
            ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = RoomsDataGrid.SelectedItem as Room;
            DateTime? startDate = CheckStartDate.SelectedDate;
            DateTime? endDate = CheckEndDate.SelectedDate;

            // Veriificam daca am selectat o camera
            if (selectedRoom == null)
            {
                MessageBox.Show("Please select a room.");
                return;
            }

            //Verifica daca am selectat cele doua dati
            if (!startDate.HasValue || !endDate.HasValue)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            // Verifica daca start date este in trecut
            if (startDate < DateTime.Today)
            {
                MessageBox.Show("Start date cannot be in the past.");
                return;
            }

            // Verifica daca start date este inainte de end date
            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be equal to or earlier than the end date.");
                return;
            }

            // Verifica daca datile se suprapun cu celelalte rezervari
            var reservations = Hotel.GetInstance().Reservations;
            foreach (Reservation r in reservations)
            {
                if (selectedRoom.RoomNumber == r.RoomNumber)
                {
                    if (AreDatesOverlapping(startDate, endDate, r.StartDateTime, r.EndDateTime))
                    {
                        MessageBox.Show($"Try with different dates, these are overlapping: {r.StartDateTime} - {r.EndDateTime}");
                        return;
                    }
                }
            }

            //Salvam rezervarea si adaugam guests in baza de date atribuindu le rezervationID ul curent

            contextReservation.RoomNumber = selectedRoom.RoomNumber;
            contextReservation.StartDateTime= (DateTime)startDate;
            contextReservation.EndDateTime = (DateTime)endDate;
            reservationService.SaveReservation(contextReservation, selectedRoom);
            int rezID = reservationService.GetReservationRepository().Insert(contextReservation);

            var selectedGuests = GuestsDataGrid.SelectedItems.Cast<Guest>().ToList();
        
            foreach (var guest in selectedGuests)
            {
                guest.ReservationId = rezID;
                guestService.SaveGuest(guest);
            }


            DialogResult = true ;
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

