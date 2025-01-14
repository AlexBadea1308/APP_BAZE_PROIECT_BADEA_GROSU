using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class DeleteUserViewModel : INotifyPropertyChanged
    {
        private UserService _userService;
        private User _userToDelete;

        public User UserToDelete
        {
            get => _userToDelete;
            private set
            {
                _userToDelete = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeleteUserViewModel(User user)
        {
            _userService = new UserService();
            UserToDelete = user;

            DeleteCommand = new RelayCommand(DeleteUser);
            CancelCommand = new RelayCommand(Cancel);
        }

      private void DeleteUser(object parameter)
        {
            if (UserToDelete.UserType == "Administrator")
            {
                MessageBox.Show("Nu poti sterge un utilizator de tip Administrator.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                _userService.DeleteUserFromDatabase(UserToDelete);
                OnUserDeleted?.Invoke(this, UserToDelete);
            }
        }


        private void Cancel(object parameter)
        {
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<User> OnUserDeleted;
        public event EventHandler OnCancelled;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
