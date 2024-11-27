using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using System;
using System.Linq;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class EditGuest : Window
    {
        private GuestService guestService;
        private Guest contextGuest;
        public Guest? SavedGuest { get; private set; }

        private bool isEditing;
        public EditGuest(Guest? guest = null)
        {
            InitializeComponent();
            //daca nu am inserat niciun guest
            if (guest == null)
            {
                contextGuest = new Guest();
                isEditing = false;
                CNPTextBox.IsReadOnly = false;
            }
            //daca vrem sa l modificam (editguest)
            else
            {
                contextGuest = guest;
                isEditing = true;
                CNPTextBox.IsReadOnly = true;
            }
            this.DataContext = contextGuest;
            guestService = new GuestService();
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

            // Validation for CNP
            if (string.IsNullOrEmpty(contextGuest.CNP) || contextGuest.CNP.Length != 13 || !contextGuest.CNP.All(char.IsDigit))
            {
                MessageBox.Show("CNP must be a 13-digit number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            //daca e false inseamna ca lucram direct cu guest si nu mai trebuie sa l salvam
            if(isEditing==false)
            {
             SavedGuest = contextGuest;
            }
            guestService.SaveGuest(contextGuest);
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
            Title = "Edit Guest";
        }
    }
}
