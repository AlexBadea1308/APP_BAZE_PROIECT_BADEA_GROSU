using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using HotelReservations.ViewModel;

namespace HotelReservations.ViewModel
{
    public class RoomsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Room> _rooms;
        private ICollectionView _roomsView;
        private string _roomNumberSearchParam = "";
        private Room _selectedRoom;

        // Integrarea RoomTypesViewModel
        private RoomTypesViewModel _roomTypesViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
                UpdateView();
            }
        }

        public ICollectionView RoomsView
        {
            get => _roomsView;
            private set
            {
                _roomsView = value;
                OnPropertyChanged();
            }
        }

        public string RoomNumberSearchParam
        {
            get => _roomNumberSearchParam;
            set
            {
                _roomNumberSearchParam = value;
                OnPropertyChanged();
                RoomsView?.Refresh();
            }
        }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // Constructor
        public RoomsViewModel()
        { 
            _roomTypesViewModel = new RoomTypesViewModel();

            // Inițializare comenzi
            AddCommand = new RelayCommand(AddRoom);
            EditCommand = new RelayCommand(EditRoom);
            DeleteCommand = new RelayCommand(DeleteRoom);

            LoadRooms();
        }

        private void LoadRooms()
        {
            using (var context = new HotelDbContext())
            {
                var rooms = context.Rooms.ToList();
                var roomTypes = context.RoomTypes.ToList();
                foreach (var room in rooms)
                {
                    room.RoomType = roomTypes.FirstOrDefault(rt => rt.Id == room.RoomTypeId);
                }
                Rooms = new ObservableCollection<Room>(rooms);
                UpdateView();
            }
        }



        private void UpdateView()
        {
            RoomsView = CollectionViewSource.GetDefaultView(Rooms);
            RoomsView.Filter = DoFilter;
        }

        private bool DoFilter(object roomObject)
        {
            var room = roomObject as Room;

            var roomTypeName = room.RoomType?.Name ?? "";

            bool isRoomNumberMatch = string.IsNullOrEmpty(RoomNumberSearchParam) ||
                room.RoomNumber.Contains(RoomNumberSearchParam, StringComparison.OrdinalIgnoreCase);

            return isRoomNumberMatch;
        }

        private void AddRoom(object parameter)
        {
            var addRoomWindow = new AddEditRoom();
            if (addRoomWindow.ShowDialog() == true)
            {
                LoadRooms();
            }
        }

        private void EditRoom(object parameter)
        {
            if (SelectedRoom == null)
            {
                MessageBox.Show("Please select a Room.", "Select Room", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Asigură sincronizarea RoomType cu lista actuală de RoomTypes
            SelectedRoom.RoomType = _roomTypesViewModel.View
                .OfType<RoomType>()
                .FirstOrDefault(rt => rt.Id == SelectedRoom.RoomType.Id);

            var editRoomWindow = new AddEditRoom(SelectedRoom);
            if (editRoomWindow.ShowDialog() == true)
            {
                LoadRooms();
            }
        }

        private void DeleteRoom(object parameter)
        {
            if (SelectedRoom == null)
            {
                MessageBox.Show("Please select a Room.", "Select Room", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var deleteRoomWindow = new DeleteRoom(SelectedRoom);
            if (deleteRoomWindow.ShowDialog() == true)
            {
                LoadRooms();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
