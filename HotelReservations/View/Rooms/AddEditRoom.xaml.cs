using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditRoom : Window
    {
        private readonly AddEditRoomViewModel _viewModel;

        public AddEditRoom(Room room = null)
        {
            InitializeComponent();
            _viewModel = new AddEditRoomViewModel(room);
            DataContext = _viewModel;
            Closed += (s, e) => _viewModel.Dispose();
        }
    }
}