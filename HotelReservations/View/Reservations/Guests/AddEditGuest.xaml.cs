using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditGuest : Window
    {
        private readonly AddEditGuestViewModel _viewModel;

        public Guest? SavedGuest => _viewModel.SavedGuest;

        public AddEditGuest(Guest? guest = null)
        {
            InitializeComponent();
            _viewModel = new AddEditGuestViewModel(guest);
            DataContext = _viewModel;

            _viewModel.RequestClose += (sender, result) =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}
