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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        private UserService userService;
        private User contextUser;
        private bool isEditing;
        public AddEditUser(User? user = null)
        {
            if (user == null)
            {
                contextUser = new User();
                isEditing = false;
            }
            else
            {
                contextUser = user.Clone();
                isEditing = true;
            }
            InitializeComponent();
            userService = new UserService();
            this.DataContext = contextUser;
            AdjustWindow(user);
        }

        private void AdjustWindow(User? user = null)
        {
            var userTypesList = Hotel.GetInstance().UserTypes.ToList();
            UserTypeCB.ItemsSource = userTypesList;

            if (user != null)
            {
                Title = "Edit User";
                UserTypeCB.SelectedItem = user.UserType;
                UserTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add User";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // basic validation
            if (contextUser.Username == "")
            {
                MessageBox.Show("Username can't be empty string.", "Username Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextUser.Name == "")
            {
                MessageBox.Show("Name can't be empty string.", "Name Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextUser.Surname == "")
            {
                MessageBox.Show("Surname can't be empty string.", "Surname Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextUser.Password == "")
            {
                MessageBox.Show("Password can't be empty string.", "Password Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrEmpty(contextUser.CNP) || contextUser.CNP.Length != 13 || !contextUser.CNP.All(char.IsDigit))
            {
                MessageBox.Show("Wrong format for CNP", "CNP Format", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextUser.UserType == null)
            {
                MessageBox.Show("Please select UserType.", "UserType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // if editing i need to avoid these validations because it will overwrite
            if(isEditing == false)
            {
                bool cnpExists = userService.GetAllUsers().Any(user => user.CNP == contextUser.CNP);
                if (cnpExists == true)
                {
                    MessageBox.Show("CNP already exists.", "CNP Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                bool usernameExists = userService.GetAllUsers().Any(user => user.Username == contextUser.Username);
                if (usernameExists == true)
                {
                    MessageBox.Show("Username already exists.", "Username Exists", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            // validation passed
            userService.SaveUser(contextUser);
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
