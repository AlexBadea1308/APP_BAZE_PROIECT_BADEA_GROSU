using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class AddReservationsViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService;
        private readonly GuestService _guestService;
        private readonly Reservation _contextReservation;
        public bool IsEditing { get; private set; }

        private ObservableCollection<Room> _availableRooms;
        private ObservableCollection<Guest> _guests;
        private Room _selectedRoom;
        private Guest _selectedGuest;
        private string _selectedRoomType;
        private DateTime? _startDate;
        private DateTime? _endDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<RoomType> RoomTypes { get; private set; }

        public ObservableCollection<Room> AvailableRooms
        {
            get => _availableRooms;
            set
            {
                _availableRooms = value;
                OnPropertyChanged(nameof(AvailableRooms));
            }
        }

        public ObservableCollection<Guest> Guests
        {
            get => _guests;
            set
            {
                _guests = value;
                OnPropertyChanged(nameof(Guests));
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public Guest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                _selectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
                FilterRooms();
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand CheckAvailableRoomsCommand { get; private set; }
        public ICommand AddGuestCommand { get; private set; }
        public ICommand EditGuestCommand { get; private set; }
        public ICommand DeleteGuestCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public AddReservationsViewModel(Reservation reservation = null)
        {
            _reservationService = new ReservationService();
            _guestService = new GuestService();
            _contextReservation = reservation?.Clone() ?? new Reservation();
            IsEditing = reservation != null;

            InitializeCollections();
            InitializeCommands();
            LoadData();
        }

        private void InitializeCollections()
        {
            AvailableRooms = new ObservableCollection<Room>();
            Guests = new ObservableCollection<Guest>();
            RoomTypes = new ObservableCollection<RoomType>(Hotel.GetInstance().RoomTypes.Where(r => r.Id !=0));
        }

        private void InitializeCommands()
        {
            CheckAvailableRoomsCommand = new RelayCommand(_ => FilterRooms());
            AddGuestCommand = new RelayCommand(_ => AddGuest(), _ => !IsEditing);
            EditGuestCommand = new RelayCommand(_ => EditGuest(), _ => SelectedGuest != null);
            DeleteGuestCommand = new RelayCommand(_ => DeleteGuest(), _ => SelectedGuest != null);
            SaveCommand = new RelayCommand(_ => Save(), _ => ValidateReservation());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void LoadData()
        {
            LoadRooms();
            LoadGuests();
        }

        private void LoadRooms()
        {
            var rooms = Hotel.GetInstance().Rooms.ToList();
            AvailableRooms = new ObservableCollection<Room>(rooms);
        }

        private void LoadGuests()
        {
            IEnumerable<Guest> guests;
            if (IsEditing)
            {
                guests = Hotel.GetInstance().Guests?.Where(g => g.ReservationId == _contextReservation.Id)
                        ?? Enumerable.Empty<Guest>();
            }
            else
            {
                guests = Hotel.GetInstance().Guests.Where(g => g.ReservationId == 0);
            }
            Guests = new ObservableCollection<Guest>(guests);
        }

        private void FilterRooms()
        {
            var rooms = Hotel.GetInstance().Rooms.Where(room =>
            {
                if (SelectedRoomType != null && room.RoomType.ToString() != SelectedRoomType)
                    return false;

                var reservations = Hotel.GetInstance().Reservations
                    .Where(res => res.RoomNumber == room.RoomNumber);

                foreach (var reservation in reservations)
                {
                    if (AreDatesOverlapping(StartDate, EndDate,
                        reservation.StartDateTime, reservation.EndDateTime))
                    {
                        return false;
                    }
                }
                return true;
            });

            AvailableRooms = new ObservableCollection<Room>(rooms);
        }

        private bool AreDatesOverlapping(DateTime? start1, DateTime? end1,
                                       DateTime? start2, DateTime? end2)
        {
            if (!start1.HasValue || !end1.HasValue ||
                !start2.HasValue || !end2.HasValue)
                return false;

            return (start1 >= start2 && start1 <= end2) ||
                   (end1 >= start2 && end1 <= end2);
        }

        private void AddGuest()
        {
            var addGuestWindow = new AddEditGuest();
            if (addGuestWindow.ShowDialog() == true)
            {
                var newGuest = addGuestWindow.SavedGuest;
                if (newGuest != null)
                {
                    Guests.Add(newGuest);
                }
            }
        }

        private void EditGuest()
        {
            if (SelectedGuest == null) return;

            var editGuestWindow = new AddEditGuest(SelectedGuest);
            if (editGuestWindow.ShowDialog() == true)
            {
                LoadGuests();
            }
        }

        private void DeleteGuest()
        {
            if (SelectedGuest == null) return;

            var deleteGuestWindow = new DeleteGuest(SelectedGuest,
                new ObservableCollection<Guest>(Hotel.GetInstance().Guests.Where(g => g.ReservationId == 0)));

            if (deleteGuestWindow.ShowDialog() == true)
            {
                LoadGuests();
            }
        }

        private bool ValidateReservation()
        {
            if (SelectedRoom == null || !StartDate.HasValue || !EndDate.HasValue)
                return false;

            if (StartDate < DateTime.Today)
                return false;

            if (StartDate > EndDate)
                return false;

            return true;
        }

        private void Save()
        {
            if (!ValidateReservation())
                return;

            _contextReservation.RoomNumber = SelectedRoom.RoomNumber;
            _contextReservation.StartDateTime = StartDate.Value;
            _contextReservation.EndDateTime = EndDate.Value;

            _reservationService.SaveReservation(_contextReservation, SelectedRoom);
            int reservationId = _reservationService.GetReservationRepository().Insert(_contextReservation);

            foreach (var guest in Guests)
            {
                guest.ReservationId = reservationId;
                _guestService.SaveGuest(guest);
            }

            CloseWindow(true);
        }

        private void Cancel()
        {
            CloseWindow(false);
        }

        private void CloseWindow(bool result)
        {
            if (Application.Current.Windows.OfType<AddReservations>().FirstOrDefault()
                is AddReservations window)
            {
                window.DialogResult = result;
                window.Close();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}