using HotelReservations.Model;
using HotelReservations.ViewModel;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class DeletePrices : Window
    {
        public DeletePrices(Price price)
        {
            InitializeComponent();

            // Create ViewModel and set DataContext
            var viewModel = new DeletePricesViewModel(price);
            DataContext = viewModel;

            // Subscribe to close event
            viewModel.OnCloseRequested += OnCloseRequested;
        }

        private void OnCloseRequested(object sender, bool dialogResult)
        {
            DialogResult = dialogResult;
            Close();
        }
    }
}