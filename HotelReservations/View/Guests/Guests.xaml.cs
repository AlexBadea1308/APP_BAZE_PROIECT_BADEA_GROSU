using System.Windows;
using System.Windows.Controls;
using HotelReservations.ViewModels;

namespace HotelReservations.Windows
{
    public partial class Guests : Window
    {
        public Guests()
        {
            InitializeComponent();
            DataContext = new GuestsViewModel();
        }
    }
}