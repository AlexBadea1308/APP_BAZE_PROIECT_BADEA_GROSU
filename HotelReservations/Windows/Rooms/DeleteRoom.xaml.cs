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
            var check = roomService.IsRoomInUse(roomToDelete);
            if (!check)
            {
                roomService.DeleteRoomFromDatabase(roomToDelete);
            }
            else
            {
                MessageBox.Show("This Room is in use and cannot be deleted.", "Room In Use", MessageBoxButton.OK, MessageBoxImage.Information);
            }

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