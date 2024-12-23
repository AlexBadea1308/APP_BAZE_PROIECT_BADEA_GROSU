using HotelReservations.Model;
using HotelReservations.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows;
using System;
using System.Linq;

namespace HotelReservations.ViewModels
{
    public class AddReservationsViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly HotelDbContext _context;
        private readonly Reservation _contextReservation;
        public bool IsEditing { get; private set; }

        private ObservableCollection<Room> _availableRooms;
        private Guest _savedGuest;
        private Room _selectedRoom;
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

        public Guest SavedGuest
        {
            get => _savedGuest;
            set
            {
                _savedGuest = value;
                OnPropertyChanged(nameof(SavedGuest));
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
            _context = new HotelDbContext();
            _contextReservation = reservation?.Clone() ?? new Reservation();
            IsEditing = reservation != null;

            InitializeCommands();
            LoadData();
        }

        private void InitializeCommands()
        {
            CheckAvailableRoomsCommand = new RelayCommand(_ => FilterRooms());
            AddGuestCommand = new RelayCommand(_ => AddGuest(), _ => SavedGuest == null);
            EditGuestCommand = new RelayCommand(_ => EditGuest(), _ => SavedGuest != null);
            DeleteGuestCommand = new RelayCommand(_ => DeleteGuest(), _ => SavedGuest != null);
            SaveCommand = new RelayCommand(_ => Save(), _ => ValidateReservation());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void LoadData()
        {
            LoadRooms();
            LoadRoomTypes();
        }

        private void LoadRooms()
        {
            var rooms = _context.Rooms.Include(r => r.RoomType).ToList();
            AvailableRooms = new ObservableCollection<Room>(rooms);
        }

        private void LoadRoomTypes()
        {
            RoomTypes = new ObservableCollection<RoomType>(_context.RoomTypes.ToList());
        }

        private void FilterRooms()
        {
            var query = _context.Rooms.Include(r => r.RoomType);

            if (!string.IsNullOrEmpty(SelectedRoomType))
            {
                query = query.Where(r => r.RoomType.Name == SelectedRoomType);
            }

            var rooms = query.ToList();

            var filteredRooms = rooms.Where(room =>
            {
                var reservations = _context.Reservations
                    .Where(res => res.RoomNumber == room.RoomNumber)
                    .ToList();

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

            AvailableRooms = new ObservableCollection<Room>(filteredRooms);
        }

        private bool AreDatesOverlapping(DateTime? start1, DateTime? end1,
                                       DateTime? start2, DateTime? end2)
        {
            if (!start1.HasValue || !end1.HasValue ||
                !start2.HasValue || !end2.HasValue)
                return false;

            return (start1 >= start2 && start1 <= end2) ||
                   (end1 >= start2 && end1 <= end2) ||
                   (start1 <= start2 && end1 >= end2);
        }

        private void AddGuest()
        {
            var addGuestWindow = new AddEditGuest();
            if (addGuestWindow.ShowDialog() == true && addGuestWindow.SavedGuest != null)
            {
                SavedGuest = addGuestWindow.SavedGuest;
            }
        }

        private void EditGuest()
        {
            if (SavedGuest == null) return;

            var addGuestWindow = new AddEditGuest(SavedGuest);
            if (addGuestWindow.ShowDialog() == true && addGuestWindow.SavedGuest != null)
            {
                SavedGuest = addGuestWindow.SavedGuest;
            }
        }

        private void DeleteGuest()
        {
            if (SavedGuest == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove this guest?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SavedGuest = null;
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

            return SavedGuest != null;
        }

        private int GetDateDifference(DateTime start, DateTime end)
        {
            if (start.Date == end.Date)
            {
                return 0;
            }
            TimeSpan difference = end.Date - start.Date;
            return (int)difference.TotalDays;
        }

        private void CalculateReservationDetails()
        {
            if (!StartDate.HasValue || !EndDate.HasValue || SelectedRoom == null)
                throw new InvalidOperationException("Start date, end date, and room must be selected.");

            int dateDifference = GetDateDifference(StartDate.Value, EndDate.Value);

            // Set reservation type based on date difference
            _contextReservation.ReservationType = dateDifference == 0 ? "Day" : "Night";

            // Get the price for the selected room type and reservation type
            var price = _context.Prices
                .FirstOrDefault(p =>
                    p.RoomTypeId == SelectedRoom.RoomTypeId &&
                    p.ReservationType.ToString() == _contextReservation.ReservationType.ToString());

            if (price == null)
            {
                throw new InvalidOperationException(
                    $"Price not found for RoomType: {SelectedRoom.RoomType.Name} and " +
                    $"ReservationType: {_contextReservation.ReservationType}.");
            }

            // Calculate total price
            _contextReservation.TotalPrice = dateDifference == 0
                ? price.PriceValue
                : dateDifference * price.PriceValue;
        }

        private void Save()
        {
            if (!ValidateReservation())
                return;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Calculate reservation type and total price
                    CalculateReservationDetails();

                    // Set basic reservation details
                    _contextReservation.RoomNumber = SelectedRoom.RoomNumber;
                    _contextReservation.StartDateTime = StartDate.Value;
                    _contextReservation.EndDateTime = EndDate.Value;

                    if (IsEditing)
                    {
                        _context.Entry(_contextReservation).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Reservations.Add(_contextReservation);
                    }

                    _context.SaveChanges();

                    // Associate guest with reservation
                    SavedGuest.ReservationId = _contextReservation.Id;
                    _context.Guests.Add(SavedGuest);

                    _context.SaveChanges();
                    transaction.Commit();
                    CloseWindow(true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(
                        $"An error occurred while saving the reservation: {ex.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}