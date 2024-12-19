using HotelReservations.Model;
using HotelReservations.ViewModels;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class AddEditPrices : Window
    {
        private AddEditPricesViewModel _viewModel;

        public AddEditPrices(Price price = null)
        {
            InitializeComponent();

            _viewModel = new AddEditPricesViewModel(price);

            DataContext = _viewModel;

            _viewModel.RequestClose += ViewModel_RequestClose;

            Title = price == null ? "Add Price" : "Edit Price";
        }

        private void ViewModel_RequestClose(object sender, RequestCloseEventArgs e)
        {
            DialogResult = e.DialogResult;
            Close();
        }
    }
}