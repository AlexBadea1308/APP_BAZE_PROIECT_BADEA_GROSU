using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelReservations.Windows
{
    public partial class AddEditRoomType : Window
    {
        public AddEditRoomType(RoomType? roomType = null)
        {
            var viewModel = new AddEditRoomTypeViewModel(roomType);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
