using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Linq;
using System.Windows;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for AddEditGuest.xaml
    /// </summary>
    public partial class AddEditGuest : Window
    {
        private GuestService guestService;
        private Guest contextGuest;
        public Guest? SavedGuest { get; private set; }

        private bool isEditing;
        public AddEditGuest(Guest? guest = null)
        {
            InitializeComponent();

            if (guest == null)
            {
                contextGuest = new Guest();
                isEditing = false;
                JMBGTextBox.IsReadOnly = false;
            }

            else
            {
                contextGuest = guest.Clone();
                isEditing = true;
                JMBGTextBox.IsReadOnly = true;
            }

            guestService = new GuestService();
            this.DataContext = contextGuest;
            AdjustWindow(guest);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validation for Name
            if (string.IsNullOrWhiteSpace(contextGuest.Name))
            {
                MessageBox.Show("Name can't be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Validation for Surname
            if (string.IsNullOrWhiteSpace(contextGuest.Surname))
            {
                MessageBox.Show("Surname can't be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Validation for JMBG
            if (string.IsNullOrEmpty(contextGuest.JMBG) || contextGuest.JMBG.Length != 13 || !contextGuest.JMBG.All(char.IsDigit))
            {
                MessageBox.Show("JMBG must be a 13-digit number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // If validation passed
            //if (!isEditing)
            //{
            //    guestService.SaveGuest(contextGuest);
            //}
            //else
            //{
            //    guestService.SaveGuest(contextGuest,true);
            //}

            // Set the SavedGuest property so it can be accessed by the parent window
            SavedGuest = contextGuest;

            // Signal success by closing the window
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void AdjustWindow(Guest? guest = null)
        { 
            if (guest != null)
            {
                Title = "Edit Guest";
            }
            else
            {
                Title = "Add Guest";
            }
        }

       
    }
}
