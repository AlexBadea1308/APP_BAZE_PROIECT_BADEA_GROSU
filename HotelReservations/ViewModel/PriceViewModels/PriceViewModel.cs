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

        public Price SelectedPrice => View.CurrentItem as Price;

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
            using (var context = new HotelDbContext())
            {
                var prices = context.Prices.ToList();
                var roomTypes = context.RoomTypes.ToList();

                foreach (var price in prices)
                {
                    price.RoomType = roomTypes.FirstOrDefault(rt => rt.Id == price.RoomTypeId);
                }

                Prices = new ObservableCollection<Price>(prices);
                View = CollectionViewSource.GetDefaultView(Prices);
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

            _priceService.DeletePriceFromDatabase(selectedPrice);
            LoadPrices();
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
