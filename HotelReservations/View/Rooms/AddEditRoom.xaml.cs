using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditRoom : Window
    {
        public AddEditRoom(Room? room = null)
        {
            InitializeComponent();
            DataContext = new AddEditRoomViewModel(room);
        }
    }
}
