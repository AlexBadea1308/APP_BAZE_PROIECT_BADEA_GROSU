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
    public partial class AddEditRoomType : Window
    {
        private RoomTypeService roomTypeService;
        private RoomType contextRoomType;
        private bool isEditing;
        public AddEditRoomType(RoomType? roomType = null)
        {
            if (roomType == null)
            {
                contextRoomType = new RoomType();
                isEditing = false;
            }
            else
            {
                contextRoomType = roomType.Clone();
                isEditing = true;
            }
            InitializeComponent();
            roomTypeService = new RoomTypeService();
            AdjustWindow(roomType);
            this.DataContext = contextRoomType;
        }
        public void AdjustWindow(RoomType? roomType = null)
        {
            if (roomType != null)
            {
                Title = "Edit RoomType";
            }
            else
            {
                Title = "Add RoomType";
            }

        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextRoomType.Name == "")
            {
                MessageBox.Show("RoomType Name can't be empty string.", "RoomType Name Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(isEditing == false)
            {
                bool roomTypeExists = roomTypeService.GetAllRoomTypes().Any(roomType => roomType.Name == contextRoomType.Name);
                if (roomTypeExists == true)
                {
                    MessageBox.Show("RoomType Name already exists.", "RoomType Name Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            roomTypeService.SaveRoomType(contextRoomType);

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
