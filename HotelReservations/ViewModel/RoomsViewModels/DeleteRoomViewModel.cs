using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HotelReservations.Model;
using HotelReservations.Service;

namespace HotelReservations.ViewModels
{
    public class DeleteRoomViewModel : ViewModelBase
    {
        private readonly RoomService _roomService;
        private readonly Room _roomToDelete;
        private readonly Action<bool?> _closeAction;

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteRoomViewModel(Room room, Action<bool?> closeAction)
        {
            _roomService = new RoomService();
            _roomToDelete = room;
            _closeAction = closeAction;

            DeleteCommand = new RelayCommand(ExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteDelete(object parameter)
        {
            var check = _roomService.IsRoomInUse(_roomToDelete);
            if (!check)
            {
                _roomService.DeleteRoomFromDatabase(_roomToDelete);
            }
            else
            {
                MessageBox.Show("This Room is in use and cannot be deleted.",
                    "Room In Use",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            _closeAction(true);
        }

        private void ExecuteCancel(object parameter)
        {
            _closeAction(false);
        }
    }

    // ViewModelBase.cs - Base class for implementing INotifyPropertyChanged
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}