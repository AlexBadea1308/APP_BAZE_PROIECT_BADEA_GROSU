using HotelReservations.Model;
using HotelReservations.Service;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservations.Windows
{
    public partial class Prices : Window
    {
        public Prices()
        {
            InitializeComponent();
        }

        private void PricesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "id")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}