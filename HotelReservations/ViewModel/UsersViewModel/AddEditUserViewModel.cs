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
    public class AddEditUserViewModel : INotifyPropertyChanged
    {
        private UserService _userService;
        private User _currentUser;
        private ObservableCollection<UserType> _userTypes;
        private bool _isEditing;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserType> UserTypes
        {
            get => _userTypes;
            set
            {
                _userTypes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditUserViewModel(User? user = null)
        {
            _userService = new UserService();
            _userTypes = new ObservableCollection<UserType>(Hotel.GetInstance().UserTypes.ToList());

            if (user == null)
            {
                CurrentUser = new User();
                _isEditing = false;
                Title = "Add User";
            }
            else
            {
                CurrentUser = user.Clone();
                _isEditing = true;
                Title = "Edit User";
                IsUserTypeEnabled = false;
            }

            SaveCommand = new RelayCommand(SaveUser, CanSaveUser);
            CancelCommand = new RelayCommand(Cancel);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserTypeEnabled = true;
        public bool IsUserTypeEnabled
        {
            get => _isUserTypeEnabled;
            set
            {
                _isUserTypeEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool CanSaveUser(object parameter)
        {
            return !string.IsNullOrWhiteSpace(CurrentUser.Username) &&
                   !string.IsNullOrWhiteSpace(CurrentUser.Name) &&
                   !string.IsNullOrWhiteSpace(CurrentUser.Surname) &&
                   !string.IsNullOrWhiteSpace(CurrentUser.Password) &&
                   !string.IsNullOrEmpty(CurrentUser.CNP) &&
                   CurrentUser.CNP.Length == 13 &&
                   CurrentUser.CNP.All(char.IsDigit) &&
                   CurrentUser.UserType != null;
        }

        private void SaveUser(object parameter)
        {
            // Validate unique CNP and Username only for new users
            if (!_isEditing)
            {
                var allUsers = _userService.GetAllUsers();
                if (allUsers.Any(u => u.CNP == CurrentUser.CNP))
                {
                    MessageBox.Show("CNP already exists.", "CNP Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (allUsers.Any(u => u.Username == CurrentUser.Username))
                {
                    MessageBox.Show("Username already exists.", "Username Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            _userService.SaveUser(CurrentUser);
            OnUserSaved?.Invoke(this, CurrentUser);
        }

        private void Cancel(object parameter)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<User> OnUserSaved;
        public event EventHandler OnCancelled;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}