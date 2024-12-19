using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeleteUser : Window
    {
        public DeleteUser(User user)
        {
            InitializeComponent();

            var viewModel = new DeleteUserViewModel(user);
            DataContext = viewModel;

            viewModel.OnUserDeleted += (sender, deletedUser) =>
            {
                DialogResult = true;
                Close();
            };

            viewModel.OnCancelled += (sender, args) =>
            {
                DialogResult = false;
                Close();
            };
        }
    }
}