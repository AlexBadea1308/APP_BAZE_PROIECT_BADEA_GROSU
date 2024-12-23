using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.ViewModel;
using HotelReservations.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class AddEditPricesViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
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

        private ObservableCollection<RoomType> _roomTypes;
        private ObservableCollection<string> _reservationTypes;

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged2();
            }
        }

        public ObservableCollection<string> ReservationTypes
        {
            get => _reservationTypes;
            set
            {
                _reservationTypes=value;
                OnPropertyChanged2();
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditPricesViewModel(Price price = null)
        {
            using (var context = new HotelDbContext())
            {
                // Se preiau tipurile de camere
                RoomTypes = new ObservableCollection<RoomType>(context.RoomTypes.ToList());

                // Convertește enum-ul ReservationType în lista de șiruri
                ReservationTypes = new ObservableCollection<string>(Enum.GetNames(typeof(ReservationType)));
            }

            // Se inițializează prețul contextului
            if (price == null)
            {
                ContextPrice = new Price();
                _isEditing = false;
            }
            else
            {
                ContextPrice = price.Clone();
                _isEditing = true;

                ContextPrice.RoomType = RoomTypes.FirstOrDefault(rt => rt.Id == ContextPrice.RoomType.Id);
            }

            // Inițializarea comenzilor
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
                using (var context = new HotelDbContext())
                {
                    if (!_isEditing)
                    {
                        ContextPrice.RoomTypeId=ContextPrice.RoomType.Id;
                        
                        // Adaugă un preț nou
                        if (context.Prices.Any(p =>
                            p.ReservationType == ContextPrice.ReservationType &&
                            p.RoomType.Id == ContextPrice.RoomType.Id))
                        {
                            MessageBox.Show("Price combination for this RoomType and ReservationType already exists!", "Try another!", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        ContextPrice.RoomType = null;
                        context.Prices.Add(ContextPrice);
                    }
                    else
                    {
                        // Editare preț existent
                        var duplicatePrice = context.Prices
                            .Include(p => p.RoomType)
                            .FirstOrDefault(p =>
                                p.ReservationType == ContextPrice.ReservationType &&
                                p.RoomType.Id == ContextPrice.RoomType.Id &&
                                p.Id != ContextPrice.Id);

                        if (duplicatePrice != null)
                        {
                            MessageBox.Show("Price combination for this RoomType and ReservationType already exists!", "Try another!", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        var existingPrice = context.Prices.FirstOrDefault(p => p.Id == ContextPrice.Id);

                        if (existingPrice != null)
                        {
                            // Actualizează câmpurile prețului existent
                            existingPrice.RoomType = context.RoomTypes.FirstOrDefault(rt => rt.Id == ContextPrice.RoomType.Id);
                            existingPrice.ReservationType = ContextPrice.ReservationType;
                            existingPrice.PriceValue = ContextPrice.PriceValue;
                        }
                        else
                        {
                            MessageBox.Show("The price could not be found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    context.SaveChanges();
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

        protected virtual void OnPropertyChanged2([CallerMemberName] string propertyName = null)
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