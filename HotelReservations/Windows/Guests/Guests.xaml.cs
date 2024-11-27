using HotelReservations.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.Windows
{
    public partial class Guests : Window
    {
        private ICollectionView view;

        public Guests()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            // Obtinem toti guests din baza de date
            var guests = Hotel.GetInstance().Guests.ToList();

            // Configurăm CollectionViewSource pentru filtrare
            view = CollectionViewSource.GetDefaultView(guests);
            view.Filter = DoFilter;

            // Setam sursa de date pentru DataGrid
            GuestDataGrid.ItemsSource = view;
            GuestDataGrid.IsSynchronizedWithCurrentItem = true;

            // Deselectam orice selectie anterioara
            GuestDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object guestObject)
        {
            var guest = guestObject as Guest;
            if (guest == null) 
                return false;

            var guestNameSearchParam = GuestNameSearchTextBox.Text.Trim();

           //verificam daca numele se potriveste
            return string.IsNullOrEmpty(guestNameSearchParam) ||
                   guest.Name.IndexOf(guestNameSearchParam, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh(); // Refresh filtru de cautare
        }

        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest)GuestDataGrid.SelectedItem;
            if (selectedGuest == null)
            {
                MessageBox.Show("Please select a guest to edit.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editGuestWindow = new EditGuest(selectedGuest); 
            if (editGuestWindow.ShowDialog() == true)
            {
                FillData(); // Reincarca datele dupa editare
            }
        }

        private void GuestDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Ascunde coloanele care nu sunt relevante
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
