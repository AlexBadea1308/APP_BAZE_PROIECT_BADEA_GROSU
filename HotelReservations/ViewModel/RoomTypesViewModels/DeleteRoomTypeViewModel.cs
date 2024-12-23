using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModel;
using Microsoft.Win32;
using System.Linq;
using System.Windows;

namespace HotelReservations.ViewModels
{
    public class DeleteRoomTypeViewModel
    {
        private readonly RoomTypeService roomTypeService;
        private readonly PriceService priceService;
        public RoomType RoomTypeToDelete { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public DeleteRoomTypeViewModel(RoomType roomType)
        {
            roomTypeService = new RoomTypeService();
            priceService = new PriceService();
            RoomTypeToDelete = roomType;

            DeleteCommand = new RelayCommand(DeleteRoomType);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void DeleteRoomType(object parameter)
        {
            var check = roomTypeService.IsRoomTypeInUse(RoomTypeToDelete);
            if (!check)
            {
                var prices_to_delete = priceService.priceRepository.GetPricesByRoomTypeId(RoomTypeToDelete.Id);
                foreach (var price in prices_to_delete)
                {
                    priceService.DeletePriceFromDatabase(price);
                }
                roomTypeService.DeleteRoomTypeFromDatabase(RoomTypeToDelete);
                MessageBox.Show("RoomType deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("This RoomType is in use and cannot be deleted.", "Room In Use", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            CloseWindow();
        }

        private void Cancel(object parameter)
        {
            DialogResult = false;
            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<Window>().LastOrDefault(w => w.IsActive)?.Close();
        }

        public bool DialogResult { get; private set; }
    }
}
