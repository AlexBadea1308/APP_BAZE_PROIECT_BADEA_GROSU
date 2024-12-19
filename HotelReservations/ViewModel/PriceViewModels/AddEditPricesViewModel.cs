using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class AddEditPricesViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly PriceService _priceService;
        private readonly RoomTypeService _roomTypeService;
        private Price _contextPrice;
        private bool _isEditing;

        public Price ContextPrice
        {
            get => _contextPrice;
            set
            {
                _contextPrice = value;
                OnPropertyChanged(nameof(ContextPrice));
            }
        }

        public ObservableCollection<RoomType> RoomTypes { get; }
        public ObservableCollection<ReservationType> ReservationTypes { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditPricesViewModel(Price price = null)
        {
            _priceService = new PriceService();
            _roomTypeService = new RoomTypeService();

            // Initialize collections
            RoomTypes = new ObservableCollection<RoomType>(Hotel.GetInstance().RoomTypes);
            ReservationTypes = new ObservableCollection<ReservationType>(Hotel.GetInstance().ReservationTypes);

            // Initialize context price
            if (price == null)
            {
                ContextPrice = new Price();
                _isEditing = false;
            }
            else
            {
                ContextPrice = price.Clone();
                _isEditing = true;
            }

            // Initialize commands
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanExecuteSave(object obj)
        {
            return ValidatePrice();
        }

        private void ExecuteSave(object obj)
        {
            if (ValidatePrice())
            {
                // Check if price combination already exists
                if (!_isEditing)
                {
                    var allPrices = _priceService.GetAllPrices().ToList();
                    if (allPrices.Any(p =>
                        p.ReservationType.ToString() == ContextPrice.ReservationType.ToString() &&
                        p.RoomType.Id == ContextPrice.RoomType.Id))
                    {
                        MessageBox.Show("Price Combination for this RoomType and ReservationType already exists!","Try another!", MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                    else
                    {
                        _priceService.SavePrice(ContextPrice);
                        OnRequestClose(true);
                    }
                }
                else
               {
                    _priceService.SavePrice(ContextPrice);
                    OnRequestClose(true);
               }
            }
        }

        private void ExecuteCancel(object obj)
        {
            OnRequestClose(false);
        }

        private bool ValidatePrice()
        {
            return ContextPrice.RoomType != null &&
                   ContextPrice.ReservationType != null &&
                   ContextPrice.PriceValue > 0;
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public event RequestCloseEventHandler RequestClose;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnRequestClose(bool dialogResult)
        {
            RequestClose?.Invoke(this, new RequestCloseEventArgs(dialogResult));
        }

        // IDataErrorInfo Implementation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ContextPrice.PriceValue):
                        if (ContextPrice.PriceValue <= 0)
                            return "Price must be a positive number";
                        break;
                    case nameof(ContextPrice.RoomType):
                        if (ContextPrice.RoomType == null)
                            return "Room Type must be selected";
                        break;
                    case nameof(ContextPrice.ReservationType):
                        if (ContextPrice.ReservationType == null)
                            return "Reservation Type must be selected";
                        break;
                }
                return null;
            }
        }
    }

    // Custom Event Args for closing the window
    public class RequestCloseEventArgs : EventArgs
    {
        public bool DialogResult { get; }

        public RequestCloseEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }

    public delegate void RequestCloseEventHandler(object sender, RequestCloseEventArgs e);
}