using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Prices : Window
    {
        private PriceService priceService;
        private ICollectionView view;
        public Prices()
        {  
            InitializeComponent();
            FillData();
        }
        private void FillData()
        {
            priceService = new PriceService();
            var prices = Hotel.GetInstance().Prices.ToList();

            view = CollectionViewSource.GetDefaultView(prices);

            PricesDataGrid.ItemsSource = null;
            PricesDataGrid.ItemsSource = view;
            PricesDataGrid.IsSynchronizedWithCurrentItem = true;
            PricesDataGrid.SelectedItem = null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addPricesWindow = new AddEditPrices();
            Hide();
            if (addPricesWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = view.CurrentItem as Price;

            if (selectedPrice == null)
            {
                MessageBox.Show("Please select an Price Item.", "Select Price Item", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editPricesWindow = new AddEditPrices(selectedPrice);
            Hide();
            if (editPricesWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPrice = view.CurrentItem as Price;

            if (selectedPrice == null)
            {
                MessageBox.Show("Please select an Price Item.", "Select Price Item", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deletePricesWindow = new DeletePrices(selectedPrice);
            Hide();
            if (deletePricesWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void PricesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

    }
}
