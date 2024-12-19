using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HotelReservations.Model;
using HotelReservations.Views;
using HotelReservations.Windows;

namespace HotelReservations.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private List<string> _imagePaths;
        private int _currentImageIndex;
        private string _currentImagePath;
        private Visibility _loginGridVisibility = Visibility.Visible;
        private Visibility _dashboardGridVisibility = Visibility.Hidden;
        private Visibility _roomsMenuVisibility = Visibility.Collapsed;
        private Visibility _roomTypeMenuVisibility = Visibility.Collapsed;
        private Visibility _usersMenuVisibility = Visibility.Collapsed;
        private Visibility _pricesMenuVisibility = Visibility.Collapsed;
        private Visibility _reservationsMenuVisibility = Visibility.Collapsed;
        private Visibility _guestsMenuVisibility = Visibility.Collapsed;

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PreviousImageCommand { get; }
        public ICommand OpenRoomsCommand { get; }
        public ICommand OpenRoomTypesCommand { get; }
        public ICommand OpenUsersCommand { get; }
        public ICommand OpenPricesCommand { get; }
        public ICommand OpenReservationsCommand { get; }
        public ICommand OpenGuestsCommand { get; }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string PassWord
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string CurrentImagePath
        {
            get => _currentImagePath;
            set
            {
                _currentImagePath = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoginGridVisibility
        {
            get => _loginGridVisibility;
            set
            {
                _loginGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility DashboardGridVisibility
        {
            get => _dashboardGridVisibility;
            set
            {
                _dashboardGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility RoomsMenuVisibility
        {
            get => _roomsMenuVisibility;
            set
            {
                _roomsMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility RoomTypeMenuVisibility
        {
            get => _roomTypeMenuVisibility;
            set
            {
                _roomTypeMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility UsersMenuVisibility
        {
            get => _usersMenuVisibility;
            set
            {
                _usersMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility PricesMenuVisibility
        {
            get => _pricesMenuVisibility;
            set
            {
                _pricesMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility ReservationsMenuVisibility
        {
            get => _reservationsMenuVisibility;
            set
            {
                _reservationsMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility GuestsMenuVisibility
        {
            get => _guestsMenuVisibility;
            set
            {
                _guestsMenuVisibility = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            LogoutCommand = new RelayCommand(ExecuteLogout);
            NextImageCommand = new RelayCommand(_ => NextImage());
            PreviousImageCommand = new RelayCommand(_ => PreviousImage());
            OpenRoomsCommand = new RelayCommand(_ => OpenRooms());
            OpenRoomTypesCommand = new RelayCommand(_ => OpenRoomTypes());
            OpenUsersCommand = new RelayCommand(_ => OpenUsers());
            OpenPricesCommand = new RelayCommand(_ => OpenPrices());
            OpenReservationsCommand = new RelayCommand(_ => OpenReservations());
            OpenGuestsCommand = new RelayCommand(_ => OpenGuests());

            InitializeImages();
        }

        private void InitializeImages()
        {
            _imagePaths = new List<string>
            {
                "/Images/1.jpg", "/Images/2.jpg", "/Images/3.jpg", "/Images/4.jpg",
                "/Images/6.jpg", "/Images/7.jpg", "/Images/9.jpg", "/Images/10.jpg",
                "/Images/11.jpg", "/Images/12.jpg", "/Images/13.jpg", "/Images/14.jpg",
                "/Images/15.jpg", "/Images/16.jpg", "/Images/17.jpg"
            };

            if (_imagePaths.Count > 0)
            {
                _currentImageIndex = 0;
                CurrentImagePath = _imagePaths[_currentImageIndex];
            }
        }

        private void ExecuteLogin(object parameter)
        {
            var findUser = Hotel.GetInstance().Users.Find(user =>
                user.Username == Username && user.Password == PassWord);

            if (findUser == null)
            {
                MessageBox.Show("Invalid credentials. Please try again.",
                    "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LoginGridVisibility = Visibility.Hidden;
            DashboardGridVisibility = Visibility.Visible;

            UpdateMenuVisibility(findUser.UserType);

            MessageBox.Show($"Logged in. Welcome {Username}.",
                "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);

            Hotel.GetInstance().loggedInUser = findUser;
        }

        private void UpdateMenuVisibility(UserType userType)
        {
            if (userType == UserType.Administrator)
            {
                RoomsMenuVisibility = Visibility.Visible;
                RoomTypeMenuVisibility = Visibility.Visible;
                UsersMenuVisibility = Visibility.Visible;
                PricesMenuVisibility = Visibility.Visible;
                ReservationsMenuVisibility = Visibility.Hidden;
                GuestsMenuVisibility = Visibility.Hidden;
            }
            else if (userType == UserType.Receptionist)
            {
                RoomsMenuVisibility = Visibility.Hidden;
                RoomTypeMenuVisibility = Visibility.Hidden;
                UsersMenuVisibility = Visibility.Hidden;
                PricesMenuVisibility = Visibility.Hidden;
                ReservationsMenuVisibility = Visibility.Visible;
                GuestsMenuVisibility = Visibility.Visible;
            }
        }

        public void ExecuteLogout(object parameter)
        {
            Hotel.GetInstance().loggedInUser = new User();

            LoginGridVisibility = Visibility.Visible;
            DashboardGridVisibility = Visibility.Hidden;

            Username = string.Empty;
            PassWord =string.Empty;

            (Application.Current.MainWindow as MainWindow)?.ClearPasswordBox();

            MessageBox.Show("Logged out", "Logout Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NextImage()
        {
            if (_imagePaths.Count > 0)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _imagePaths.Count;
                CurrentImagePath = _imagePaths[_currentImageIndex];
            }
        }

        private void PreviousImage()
        {
            if (_imagePaths.Count > 0)
            {
                _currentImageIndex = (_currentImageIndex - 1 + _imagePaths.Count) % _imagePaths.Count;
                CurrentImagePath = _imagePaths[_currentImageIndex];
            }
        }

        private void OpenRooms() => new Rooms().Show();
        private void OpenRoomTypes() => new RoomTypes().Show();
        private void OpenUsers() => new Users().Show();
        private void OpenPrices() => new Prices().Show();
        private void OpenReservations() => new Reservations().Show();
        private void OpenGuests() => new Guests().Show();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}