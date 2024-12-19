using HotelReservations.Model;
using HotelReservations.ViewModel;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditUser : Window
    {
        public AddEditUser(User? user = null)
        {
            InitializeComponent();

            var viewModel = new AddEditUserViewModel(user);
            DataContext = viewModel;

            viewModel.OnUserSaved += (sender, savedUser) =>
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