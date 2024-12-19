using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class AddEditRoomViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly RoomTypeService _roomTypeService;
        private readonly RoomService _roomService;
        private Room _room;
        private bool _isEditing;
        private ObservableCollection<RoomType> _roomTypes;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RoomSaved;

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged();
            }
        }

        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }

        public string WindowTitle { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // IDataErrorInfo implementation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Room.RoomNumber):
                        if (string.IsNullOrWhiteSpace(Room.RoomNumber))
                            return "Room Number cannot be empty";

                        // Check for duplicate room number when adding new room
                        if (!_isEditing && _roomService.GetAllRooms().Any(r => r.RoomNumber == Room.RoomNumber))
                            return "Room Number already exists";

                        break;
                    case nameof(Room.RoomType):
                        if (Room.RoomType == null)
                            return "Room Type must be selected";
                        break;
                }
                return null;
            }
        }

        public AddEditRoomViewModel(Room room = null)
        {
            _roomTypeService = new RoomTypeService();
            _roomService = new RoomService();

            // Initialize RoomTypes from Hotel singleton
            RoomTypes = new ObservableCollection<RoomType>(Hotel.GetInstance().RoomTypes);

            // Determine if editing or adding new room
            if (room == null)
            {
                Room = new Room();
                _isEditing = false;
                WindowTitle = "Add Room";
            }
            else
            {
                Room = room.Clone();
                _isEditing = true;
                WindowTitle = "Edit Room";

                // Set the initial room type if editing
                if (room.RoomType != null)
                {
                    Room.RoomType = RoomTypes.FirstOrDefault(rt => rt.Id == room.RoomType.Id);
                }
            }

            // Initialize Commands
            SaveCommand = new RelayCommand(SaveRoom, CanSaveRoom);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanSaveRoom(object parameter)
        {
            // Validate room number and room type
            return !string.IsNullOrWhiteSpace(Room.RoomNumber)
                   && Room.RoomType != null
                   && string.IsNullOrEmpty(this[nameof(Room.RoomNumber)])
                   && string.IsNullOrEmpty(this[nameof(Room.RoomType)]);
        }

        private void SaveRoom(object parameter)
        {
            try
            {
                // Save the room
                _roomService.SaveRoom(Room);

                // Trigger the RoomSaved event to notify listeners
                RoomSaved?.Invoke(this, EventArgs.Empty);

                // Close the window if parameter is a Window
                if (parameter is Window window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving room: {ex.Message}", "Save Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}