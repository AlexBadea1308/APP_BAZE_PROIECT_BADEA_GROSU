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
    public partial class DeleteUser : Window
    {
        private UserService userService;
        private User userToDelete;
        public DeleteUser(User user)
        {
            InitializeComponent();
            userService = new UserService();
            userToDelete = user;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            userService.DeleteUserFromDatabase(userToDelete);
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
