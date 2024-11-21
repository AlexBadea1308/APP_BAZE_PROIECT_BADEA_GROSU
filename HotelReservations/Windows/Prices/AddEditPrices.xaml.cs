using HotelReservations.Model;
using HotelReservations.Service;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditPrices.xaml
    /// </summary>
    public partial class AddEditPrices : Window
    {
        private PriceService priceService;
        private RoomTypeService roomTypeService;
        private Price contextPrice;
        private bool isEditing;

        public AddEditPrices(Price? price = null)
        {
            if (price == null)
            {
                contextPrice = new Price();
                isEditing = false;
            }
            else
            {
                contextPrice = price.Clone();
                isEditing = true;
            }
            InitializeComponent();
            priceService = new PriceService();
            roomTypeService = new RoomTypeService();
            AdjustWindow(price);
            this.DataContext = contextPrice;
        }

        private void AdjustWindow(Price? price = null)
        {
            var roomTypeList = Hotel.GetInstance().RoomTypes.Where(roomType => roomType.IsActive).ToList();
            var reservationTypeList = Hotel.GetInstance().ReservationTypes.ToList();
            RoomTypeCB.ItemsSource = roomTypeList;
            ReservationTypeCB.ItemsSource = reservationTypeList;

            if (price != null)
            {
                Title = "Edit price";
                RoomTypeCB.SelectedValue = roomTypeService.GetRoomTypeByName(price.RoomType.Name);
                RoomTypeCB.IsEnabled = false;
                ReservationTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add price";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // basic validations
            if (contextPrice.RoomType == null)
            {
                MessageBox.Show("Please select RoomType.", "RoomType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextPrice.ReservationType == null)
            {
                MessageBox.Show("Please select ReservationType.", "ReservationType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!double.TryParse(PriceValueTextBox.Text, out double priceValue) || priceValue < 0)
            {
                MessageBox.Show("Please enter a valid positive Price", "Price Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // this will be only if editing is false because while editing i will overwrite existing one
            if(!isEditing)
            {
                var allPrices = priceService.GetAllPrices().Where(price => price.IsActive == true);
                foreach (var currentPrice in allPrices)
                {
                    if (currentPrice.ReservationType.ToString() == contextPrice.ReservationType.ToString() && currentPrice.RoomType.ToString() == contextPrice.RoomType.ToString())
                    {
                        MessageBox.Show("Price Combination for this RoomType and ReservationType already exist!", "Price Exist Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            }

            // validation passed
            priceService.SavePrice(contextPrice);
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
