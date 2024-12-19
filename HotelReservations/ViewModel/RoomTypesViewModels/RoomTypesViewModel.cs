using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.ViewModel
{
    public class RoomTypesViewModel : INotifyPropertyChanged
    {
        private ICollectionView _view;
        private string _searchText;
        private RoomType _selectedRoomType;
        private List<RoomType> _roomTypes;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollectionView View
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged(nameof(View));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                View?.Refresh();
            }
        }

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public RoomTypesViewModel()
        {
            LoadData();
            AddCommand = new RelayCommand(AddRoomType);
            EditCommand = new RelayCommand(EditRoomType, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteRoomType, CanEditOrDelete);
        }

        private bool FilterRoomTypes(object obj)
        {
            if (obj is RoomType roomType)
            {
                return string.IsNullOrEmpty(SearchText) || roomType.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void AddRoomType(object parameter)
        {
            var addRoomTypeWindow = new AddEditRoomType();
            addRoomTypeWindow.ShowDialog();
           LoadData();
        }

        private void EditRoomType(object parameter)
        {
            if (SelectedRoomType == null)
            {
                MessageBox.Show("Please select a RoomType.", "Select RoomType", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editRoomWindow = new AddEditRoomType(SelectedRoomType);
            editRoomWindow.ShowDialog();
            LoadData();
        }


        private void DeleteRoomType(object parameter)
        {
            if (SelectedRoomType == null)
            {
                MessageBox.Show("Please select a RoomType.", "Select RoomType", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var deleteRoomWindow = new DeleteRoomType(SelectedRoomType);
            deleteRoomWindow.ShowDialog();
            LoadData();
        }

        private void LoadData()
        {
            _roomTypes = Hotel.GetInstance().RoomTypes.ToList(); 
            _roomTypes = _roomTypes.OrderBy(rt => rt.Id).ToList();

            View = CollectionViewSource.GetDefaultView(_roomTypes);
            View.Filter = FilterRoomTypes;

            View.Refresh();
        }

        private bool CanEditOrDelete(object parameter)
        {
            return SelectedRoomType != null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}