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
    /// Interaction logic for DeletePrices.xaml
    /// </summary>
    public partial class DeletePrices : Window
    {
        private PriceService priceService;
        private Price priceToDelete;
        public DeletePrices(Price price)
        {
            InitializeComponent();
            priceService = new PriceService();
            priceToDelete = price;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // add modal
            priceService.MakePriceInactive(priceToDelete);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
