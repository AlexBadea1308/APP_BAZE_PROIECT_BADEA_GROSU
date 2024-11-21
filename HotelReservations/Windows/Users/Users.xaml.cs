using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        private UserService userService;
        private ICollectionView view;
        public Users()
        {
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            userService = new UserService();
            var users = Hotel.GetInstance().Users.Where(user => user.IsActive).ToList();

            view = CollectionViewSource.GetDefaultView(users);
            view.Filter = DoFilter;

            UsersDataGrid.ItemsSource = null;
            UsersDataGrid.ItemsSource = view;
            UsersDataGrid.IsSynchronizedWithCurrentItem = true;
            UsersDataGrid.SelectedItem = null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addUsersWindow = new AddEditUser();
            Hide();
            if (addUsersWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = view.CurrentItem as User;

            if (selectedUser == null)
            {
                MessageBox.Show("Please select a User.", "Select User", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editUsersWindow = new AddEditUser(selectedUser);
            Hide();
            if (editUsersWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = view.CurrentItem as User;

            if (selectedUser == null)
            {
                MessageBox.Show("Please select a User.", "Select User", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deleteUsersWindow = new DeleteUser(selectedUser);
            Hide();
            if (deleteUsersWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void UsersDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private bool DoFilter(object userObject)
        {
            var user = userObject as User;

            var userSearchParam = UsernameSearchTextBox.Text;

            if (user.Username.Contains(userSearchParam))
            {
                return true;
            }

            return false;
        }

        private void UsernameSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
