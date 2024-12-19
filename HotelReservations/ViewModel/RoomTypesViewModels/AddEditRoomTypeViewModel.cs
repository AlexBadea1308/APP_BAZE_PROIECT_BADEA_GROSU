using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelReservations.ViewModel
{
    public class AddEditRoomTypeViewModel : INotifyPropertyChanged
    {
        private RoomTypeService _roomTypeService;
        private RoomType _roomType;
        private bool _isEditing;

        public event PropertyChangedEventHandler PropertyChanged;

        public RoomType RoomType
        {
            get => _roomType;
            set
            {
                _roomType = value;
                OnPropertyChanged(nameof(RoomType));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditRoomTypeViewModel(RoomType roomType = null)
        {
            _roomTypeService = new RoomTypeService();
            _isEditing = roomType != null;

            if (_isEditing)
            {
                RoomType = roomType.Clone(); 
            }
            else
            {
                RoomType = new RoomType();
            }

            SaveCommand = new RelayCommand(SaveRoomType);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void SaveRoomType(object parameter)
        {
            if (string.IsNullOrEmpty(RoomType.Name))
            {
                MessageBox.Show("RoomType Name can't be empty.", "RoomType Name Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!_isEditing)
            {
                // Verifică dacă RoomType există deja
                var roomTypeExists = _roomTypeService.GetAllRoomTypes().Any(rt => rt.Name.Equals(RoomType.Name, StringComparison.OrdinalIgnoreCase));
                if (roomTypeExists)
                {
                    MessageBox.Show("RoomType Name already exists.", "RoomType Name Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            _roomTypeService.SaveRoomType(RoomType);
            DialogResult = true;
            CloseWindow();
        }

        private void Cancel(object parameter)
        {
            DialogResult = false;
            CloseWindow();
        }

        private void CloseWindow()
        {
            // Codul pentru închiderea ferestrei
            Application.Current.Windows.OfType<Window>().LastOrDefault(w => w.IsActive)?.Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool DialogResult { get; private set; }
    }
}
