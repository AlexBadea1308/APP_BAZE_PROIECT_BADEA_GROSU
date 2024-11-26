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
            // Obținem toți oaspeții din baza de date
            var guests = Hotel.GetInstance().Guests.ToList();

            // Configurăm CollectionViewSource pentru filtrare
            view = CollectionViewSource.GetDefaultView(guests);
            view.Filter = DoFilter;

            // Setăm sursa de date pentru DataGrid
            GuestDataGrid.ItemsSource = view;
            GuestDataGrid.IsSynchronizedWithCurrentItem = true;

            // Deselează orice selecție anterioară
            GuestDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object guestObject)
        {
            var guest = guestObject as Guest;
            if (guest == null) 
                return false;

            // Obține parametrii din câmpul de căutare
            var guestNameSearchParam = GuestNameSearchTextBox.Text.Trim();

            // Verifică dacă numele se potrivește
            return string.IsNullOrEmpty(guestNameSearchParam) ||
                   guest.Name.IndexOf(guestNameSearchParam, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh(); // Reîmprospătează vizualizarea pentru aplicarea filtrului
        }

        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = (Guest)GuestDataGrid.SelectedItem;
            if (selectedGuest == null)
            {
                MessageBox.Show("Please select a guest to edit.", "Select Guest", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editGuestWindow = new AddEditGuest(selectedGuest); // Presupunem că ai o fereastră separată pentru editare
            if (editGuestWindow.ShowDialog() == true)
            {
                FillData(); // Reîncarcă datele după editare
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
