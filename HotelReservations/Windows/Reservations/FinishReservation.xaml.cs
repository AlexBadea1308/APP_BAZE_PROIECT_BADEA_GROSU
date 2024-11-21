using HotelReservations.Model;
using HotelReservations.Repositories;
using HotelReservations.Service;
using System.Windows;

namespace HotelReservations.Windows
{
    /// <summary>
    /// Interaction logic for FinishReservation.xaml
    /// </summary>
    public partial class FinishReservation : Window
    {
        private GuestRepositoryDB guestRepository;
        private ReservationService reservationService;
        private Reservation resToFinish;
        public FinishReservation(Reservation finishReservation)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            guestRepository = new GuestRepositoryDB();
            resToFinish = finishReservation;
        }

        private void FinishBtn_Click(object sender, RoutedEventArgs e)
        {
            var totalPrice = reservationService.FinishReservation(resToFinish);

            MessageBox.Show($"You must pay: {totalPrice}", "Payment Information", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach(Guest g in resToFinish.Guests)
            {
                g.IsActive = false;
                guestRepository.Update(g);
            }

            DialogResult = true;
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
