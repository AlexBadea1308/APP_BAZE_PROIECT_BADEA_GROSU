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
    /// Interaction logic for DeleteRoomType.xaml
    /// </summary>
    public partial class DeleteRoomType : Window
    {
        private RoomTypeService roomTypeService;
        private RoomType roomTypeToDelete;
        public DeleteRoomType(RoomType roomType)
        {
            InitializeComponent();
            roomTypeService = new RoomTypeService();
            roomTypeToDelete = roomType;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var check = roomTypeService.IsRoomTypeInUse(roomTypeToDelete);
            if (!check)
            {
                roomTypeService.MakeRoomTypeInactive(roomTypeToDelete);
            } else
            {
                MessageBox.Show("This RoomType is in use and cannot be deleted.", "Room In Use", MessageBoxButton.OK, MessageBoxImage.Information);
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
