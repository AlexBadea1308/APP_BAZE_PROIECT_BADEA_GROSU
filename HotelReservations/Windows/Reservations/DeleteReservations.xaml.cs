using HotelReservations.Model;
using HotelReservations.Repositories;
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
    /// Interaction logic for DeleteReservations.xaml
    /// </summary>
    public partial class DeleteReservations : Window
    {
        private GuestRepositoryDB guestRepository;
        private ReservationService reservationService;
        private Reservation resToDelete;
        public DeleteReservations(Reservation res)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            guestRepository = new GuestRepositoryDB();
            resToDelete = res;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // add modal
            reservationService.MakeReservationInactive(resToDelete);
            foreach (Guest g in resToDelete.Guests)
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
