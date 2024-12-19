using HotelReservations.Model;
using HotelReservations.Service;
using System.Windows.Input;

namespace HotelReservations.ViewModel
{
    public class DeletePricesViewModel :ViewModelBase
    {
        private Price _priceToDelete;
        private PriceService _priceService;

        public Price PriceToDelete
        {
            get => _priceToDelete;
            set
            {
                _priceToDelete = value;
                OnPropertyChanged(nameof(PriceToDelete));
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public DeletePricesViewModel(Price price)
        {
            _priceService = new PriceService();
            PriceToDelete = price;

            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return PriceToDelete != null;
        }

        private void ExecuteDelete(object parameter)
        {
            _priceService.DeletePriceFromDatabase(PriceToDelete);
            CloseWindow(true);
        }

        private void ExecuteCancel(object parameter)
        {
            CloseWindow(false);
        }

        private void CloseWindow(bool result)
        {
            OnCloseRequested?.Invoke(this, result);
        }

        public event CloseRequestedEventHandler OnCloseRequested;
    }

    public delegate void CloseRequestedEventHandler(object sender, bool dialogResult);
}