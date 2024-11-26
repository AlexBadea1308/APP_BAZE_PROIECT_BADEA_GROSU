using HotelReservations.Model;
using HotelReservations.Service;
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
    /// <summary>
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Window
    {
        private RoomService roomService;
        private Room roomToDelete;
        public DeleteRoom(Room room)
        {
            InitializeComponent();
            roomService = new RoomService();
            roomToDelete = room;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // add modal
            roomService.DeleteRoomFromDatabase(roomToDelete);
            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}