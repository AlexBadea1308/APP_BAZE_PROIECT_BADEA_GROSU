using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModel;
using HotelReservations.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class PricesViewModel : INotifyPropertyChanged
    {
        private readonly PriceService _priceService;
        private ObservableCollection<Price> _prices;
        private ICollectionView _view;

        public ObservableCollection<Price> Prices
        {
            get => _prices;
            set
            {
                _prices = value;
                OnPropertyChanged(nameof(Prices));
            }
        }

        public ICollectionView View
        {
            get => _view;
            set
            {
                _view = value;
                OnPropertyChanged(nameof(View));
            }
        }

        public Price SelectedPrice => View?.CurrentItem as Price;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public PricesViewModel()
        {
            _priceService = new PriceService();
            LoadPrices();

            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEditDelete);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteEditDelete);
        }

        private void LoadPrices()
        {
            var loadedPriceRepository = _priceService.priceRepository.GetAll();
            if (loadedPriceRepository != null)
            {
                Hotel.GetInstance().Prices = loadedPriceRepository;
                var prices = Hotel.GetInstance().Prices.ToList();
                View = CollectionViewSource.GetDefaultView(prices);
                Prices = new ObservableCollection<Price>(prices);
            }
        }

        private void ExecuteAdd(object obj)
        {
            var addPricesWindow = new AddEditPrices();
            if (addPricesWindow.ShowDialog() == true)
            {
                LoadPrices();
            }
        }

        private void ExecuteEdit(object obj)
        {
            var selectedPrice = SelectedPrice;
            if (selectedPrice == null)
            {
                return;
            }

            var editPricesWindow = new AddEditPrices(selectedPrice);
            if (editPricesWindow.ShowDialog() == true)
            {
                LoadPrices();
            }
        }

        private void ExecuteDelete(object obj)
        {
            var selectedPrice = SelectedPrice;
            if (selectedPrice == null)
            {
                return;
            }

            var deletePricesWindow = new DeletePrices(selectedPrice);
            if (deletePricesWindow.ShowDialog() == true)
            {
                LoadPrices();
            }
        }

        private bool CanExecuteEditDelete(object obj)
        {
            return SelectedPrice != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}