using HotelReservations.Model;
using HotelReservations.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HotelReservations
{
    public partial class MainWindow : Window
    {
        private List<string> imagePaths = new List<string>();
        private int currentImageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            PopulateImages();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // verific daca apas enter
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void RoomsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var roomsWindow = new Rooms();
            roomsWindow.Show();
        }

        private void RoomTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var roomTypeWindow = new RoomTypes();
            roomTypeWindow.Show();
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new Users();
            usersWindow.Show();
        }

        private void PricesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var pricesWindow = new Prices();
            pricesWindow.Show();
        }

        private void ReservationsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var reservationsWindow = new Reservations();
            reservationsWindow.Show();
        }

        private void GuestsItem_Click(object sender, RoutedEventArgs e)
        {
            var reservationsWindow = new Guests();
            reservationsWindow.Show();
        }
        private void PopulateImages()
        {
            // adaug imaginile disponibile in lista
            imagePaths.Add("/Images/1.jpg");
            imagePaths.Add("/Images/2.jpg");
            imagePaths.Add("/Images/3.jpg");
            imagePaths.Add("/Images/4.jpg");
            imagePaths.Add("/Images/6.jpg");
            imagePaths.Add("/Images/7.jpg");
            imagePaths.Add("/Images/9.jpg");
            imagePaths.Add("/Images/10.jpg");
            imagePaths.Add("/Images/11.jpg");
            imagePaths.Add("/Images/12.jpg");
            imagePaths.Add("/Images/13.jpg");
            imagePaths.Add("/Images/14.jpg");
            imagePaths.Add("/Images/15.jpg");
            imagePaths.Add("/Images/16.jpg");
            imagePaths.Add("/Images/17.jpg");
            // setez prima imagine
            if (imagePaths.Count > 0)
            {
                currentImageIndex = 0;
                SetCarouselImage();
            }
        }

        private void SetCarouselImage()
        {
            if (imagePaths.Count > 0 && currentImageIndex >= 0 && currentImageIndex < imagePaths.Count)
            {
                var imageUri = new Uri(imagePaths[currentImageIndex], UriKind.Relative);
                CarouselImage.Source = new BitmapImage(imageUri);
            }
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths.Count > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % imagePaths.Count; // rotire circulara
                SetCarouselImage();
            }
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths.Count > 0)
            {
                currentImageIndex = (currentImageIndex - 1 + imagePaths.Count) % imagePaths.Count; //rotire circulara
                SetCarouselImage();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var findUser = Hotel.GetInstance().Users.Find(user => user.Username == username && user.Password == password);
            if (findUser == null)
            {
                MessageBox.Show("Invalid credentials. Please try again.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // ascunde login-ul si apare dashboard
                LoginGrid.Visibility = Visibility.Hidden;
                DashboardGrid.Visibility = Visibility.Visible;

                // verifica ce tip de meniu sa afiseze in fct de user
                if (findUser.UserType == UserType.Administrator)
                {
                    RoomsMenuItem.Visibility = Visibility.Visible;
                    RoomTypeMenuItem.Visibility = Visibility.Visible;
                    UsersMenuItem.Visibility = Visibility.Visible;
                    PricesMenuItem.Visibility = Visibility.Visible;
                    ReservationsMenuItem.Visibility = Visibility.Hidden;
                    GuestsItem.Visibility = Visibility.Hidden;
                }
                else if (findUser.UserType == UserType.Receptionist)
                {
                    RoomsMenuItem.Visibility = Visibility.Hidden;
                    RoomTypeMenuItem.Visibility = Visibility.Hidden;
                    UsersMenuItem.Visibility = Visibility.Hidden;
                    PricesMenuItem.Visibility = Visibility.Hidden;
                    ReservationsMenuItem.Visibility = Visibility.Visible;
                    GuestsItem.Visibility = Visibility.Visible;
                }

                MessageBox.Show("Logged in. Welcome " + username + ".", "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);

                //seteaza user
                Hotel.GetInstance().loggedInUser = findUser;
            }
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //clear
            Hotel.GetInstance().loggedInUser = new User();

          
            LoginGrid.Visibility = Visibility.Visible;
            DashboardGrid.Visibility = Visibility.Hidden;

            UsernameTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;

            MessageBox.Show("Logged out", "Logout Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
