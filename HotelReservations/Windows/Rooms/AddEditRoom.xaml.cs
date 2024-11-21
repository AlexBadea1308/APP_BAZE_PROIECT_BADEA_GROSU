using HotelReservations.Service;
using HotelReservations.Model;
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
    /// Interaction logic for AddEditRoom.xaml
    /// </summary>
    public partial class AddEditRoom : Window
    {
        private RoomTypeService roomTypeService;
        private RoomService roomService;
        private Room contextRoom;
        private bool isEditing;
        public AddEditRoom(Room? room = null)
        {
            if (room == null)
            {
                contextRoom = new Room();
                isEditing = false;
            }
            else
            {
                contextRoom = room.Clone();
                isEditing = true;
            }
            InitializeComponent();
            roomTypeService = new RoomTypeService();
            roomService = new RoomService();
            AdjustWindow(room);
            this.DataContext = contextRoom;
        }

        public void AdjustWindow(Room? room = null)
        {
            var roomTypeList = Hotel.GetInstance().RoomTypes.Where(roomType => roomType.IsActive).ToList();
            RoomTypeComboBox.ItemsSource = roomTypeList;

            if (room != null)
            {
                Title = "Edit Room";
                RoomTypeComboBox.SelectedValue = roomTypeService.GetRoomTypeByName(room.RoomType.Name);
            }
            else
            {
                Title = "Add Room";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextRoom.RoomNumber == "")
            {
                MessageBox.Show("RoomNumber can't be empty string.", "RoomNumber Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextRoom.RoomType == null)
            {
                MessageBox.Show("Please select RoomType.", "RoomType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(isEditing == false)
            {
                bool roomExists = roomService.GetAllRooms().Where(room => room.IsActive == true).Any(room => room.RoomNumber == contextRoom.RoomNumber);
                if (roomExists == true)
                {
                    MessageBox.Show("RoomNumber already exists.", "RoomNumber Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            // validation passed
            roomService.SaveRoom(contextRoom);
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
