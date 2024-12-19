using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeleteRoomType : Window
    {
        public DeleteRoomType(RoomType roomType)
        {
            InitializeComponent();
            DataContext = new DeleteRoomTypeViewModel(roomType);
        }
    }
}
