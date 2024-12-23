using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        private ObservableCollection<User> _users;
        private ICollectionView _usersView;
        private string _usernameSearchText = string.Empty;
        private User _selectedUser;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICollectionView UsersView
        {
            get => _usersView;
            set
            {
                _usersView = value;
                OnPropertyChanged(nameof(UsersView));
            }
        }

        public string UsernameSearchText
        {
            get => _usernameSearchText;
            set
            {
                _usernameSearchText = value;
                OnPropertyChanged(nameof(UsernameSearchText));
                ApplyFilter();
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event EventHandler<User> OnOpenAddEditUserWindow;
        public event EventHandler<User> OnOpenDeleteUserWindow;
        public UsersViewModel()
        {
            InitializeCommands();
            LoadUsers();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit,CanExecuteEditDelete);
            DeleteCommand = new RelayCommand(ExecuteDelete,CanExecuteEditDelete);
        }

        public void LoadUsers()
        {
            using (var context = new HotelDbContext()) 
            {
                Users = new ObservableCollection<User>(context.Users.ToList());
            }
            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = DoFilter;
        }

        private bool DoFilter(object userObject)
        {
            if (string.IsNullOrEmpty(UsernameSearchText))
                return true;

            var user = userObject as User;
            return user != null && user.Username.Contains(UsernameSearchText, StringComparison.OrdinalIgnoreCase);
        }

        private void ApplyFilter()
        {
            UsersView?.Refresh();
        }

        private bool CanExecuteEditDelete(object parameter)
        {
            return SelectedUser != null;
        }

        private void ExecuteAdd(object parameter)
        {
            OnOpenAddEditUserWindow?.Invoke(this, null);
        }

        private void ExecuteEdit(object parameter)
        {
            OnOpenAddEditUserWindow?.Invoke(this, SelectedUser);
        }

        private void ExecuteDelete(object parameter)
        {
            OnOpenDeleteUserWindow?.Invoke(this, SelectedUser);
        }


    }
}