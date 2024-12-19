using HotelReservations.Model;
using HotelReservations.ViewModel;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class Users : Window
    {
        private UsersViewModel _viewModel;

        public Users()
        {
            InitializeComponent();

            // Explicitly create and set the DataContext
            _viewModel = new UsersViewModel();
            DataContext = _viewModel;

            // Subscribe to events after setting DataContext
            _viewModel.OnOpenAddEditUserWindow += HandleOpenAddEditUserWindow;
            _viewModel.OnOpenDeleteUserWindow += HandleOpenDeleteUserWindow;
        }

        private void HandleOpenAddEditUserWindow(object sender, User user)
        {
            var addEditUserWindow = user == null
                ? new AddEditUser()
                : new AddEditUser(user);

            Hide();
            if (addEditUserWindow.ShowDialog() == true)
            {
                _viewModel.LoadUsers(); // Ensure this method is added to the ViewModel
            }
            Show();
        }

        private void HandleOpenDeleteUserWindow(object sender, User user)
        {
            var deleteUserWindow = new DeleteUser(user);
            Hide();
            if (deleteUserWindow.ShowDialog() == true)
            {
                _viewModel.LoadUsers(); // Ensure this method is added to the ViewModel
            }
            Show();
        }
    }
}