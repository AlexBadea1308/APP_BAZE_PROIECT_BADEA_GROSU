using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Views
{
    public partial class FinishReservation : Window
    {
        private readonly FinishReservationViewModel _viewModel;

        public FinishReservation(Reservation reservation)
        {
            InitializeComponent();
            _viewModel = new FinishReservationViewModel(reservation);
            DataContext = _viewModel;

            _viewModel.RequestClose += (sender, result) =>
            {
                DialogResult = result;
                Close();
            };
        }
    }
}