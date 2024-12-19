using HotelReservations.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservations.Windows
{
    public partial class RoomTypes : Window
    {
        public RoomTypes()
        {
            InitializeComponent();
            DataContext = new RoomTypesViewModel(); 
        }
    }
}
