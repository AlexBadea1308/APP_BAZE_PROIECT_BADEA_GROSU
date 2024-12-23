using System.Windows;
using System.Windows.Controls;
using HotelReservations.ViewModel;


namespace HotelReservations.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;

            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;

            Closing += MainWindow_Closing;

            ResetCredentials();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null && sender is PasswordBox passwordBox)
            {
                _viewModel.PassWord = passwordBox.Password;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void ClearPasswordBox()
        {
            PasswordBox.Password = string.Empty;
        }
        private void ResetCredentials()
        {
            if (_viewModel != null)
            {
                _viewModel.Username = string.Empty;
                _viewModel.PassWord = string.Empty;
                PasswordBox.Password = string.Empty;
            }
        }
    }
}