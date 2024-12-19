using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeleteReservation : Window
    {
        private readonly DeleteReservationViewModel _viewModel;

        public DeleteReservation(Reservation reservation)
        {
            InitializeComponent();
            _viewModel = new DeleteReservationViewModel(reservation);
            DataContext = _viewModel;

            _viewModel.RequestClose += (sender, result) =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}
