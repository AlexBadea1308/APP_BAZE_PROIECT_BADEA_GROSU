using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using ServiceStack;
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
    public partial class DeleteRoomType : Window
    {
        private RoomTypeService roomTypeService;
        private RoomType roomTypeToDelete;
        private PriceService priceService;
        public DeleteRoomType(RoomType roomType)
        {
            InitializeComponent();
            roomTypeService = new RoomTypeService();
            priceService= new PriceService();
            roomTypeToDelete = roomType;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var check = roomTypeService.IsRoomTypeInUse(roomTypeToDelete);
            if (!check)
            {
                var prices_to_delete=priceService.priceRepository.GetPricesByRoomTypesID(roomTypeToDelete.Id);
                foreach(Price price in prices_to_delete)
                {
                    priceService.DeletePriceFromDatabase(price);
                }
                roomTypeService.DeleteRoomTypeFromDatabase(roomTypeToDelete);
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
