using HotelReservations.Model;
using HotelReservations.ViewModel;
using System.Windows;

namespace HotelReservations.Windows
{
    public partial class EditGuest : Window
    {
        public Guest? SavedGuest { get; private set; }

        public EditGuest(Guest? guest = null)
        {
            InitializeComponent();
            var viewModel = new EditGuestViewModel(guest);
            DataContext = viewModel;

            viewModel.OnCloseRequested += OnCloseRequested;
        }

        private void OnCloseRequested(object sender, bool dialogResult)
        {
            if (dialogResult)
            { 
                SavedGuest = ((EditGuestViewModel)DataContext).ContextGuest;
            }
            DialogResult = dialogResult;
            Close();
        }
    }
}