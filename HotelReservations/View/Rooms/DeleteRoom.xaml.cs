using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModels;
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
    public partial class DeleteRoom : Window
    {
        public DeleteRoom(Room room)
        {
            InitializeComponent();
            DataContext = new DeleteRoomViewModel(room, result =>
            {
                DialogResult = result;
                Close();
            });
        }
    }
}