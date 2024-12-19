using HotelReservations.Model;
using HotelReservations.Service;
using HotelReservations.Views;
using HotelReservations.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelReservations.ViewModels
{
    public class ReservationsViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService;
        private ICollectionView _reservationsView;
        private Reservation _selectedReservation;
        private string _roomNumberFilter = "";
        private string _startDateFilter = "";
        private string _endDateFilter = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Reservation> Reservations { get; private set; }

        public ICommand AddReservationCommand { get; private set; }
        public ICommand DeleteReservationCommand { get; private set; }
        public ICommand FinishReservationCommand { get; private set; }

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public string RoomNumberFilter
        {
            get => _roomNumberFilter;
            set
            {
                _roomNumberFilter = value;
                OnPropertyChanged(nameof(RoomNumberFilter));
                _reservationsView?.Refresh();
            }
        }

        public string StartDateFilter
        {
            get => _startDateFilter;
            set
            {
                _startDateFilter = value;
                OnPropertyChanged(nameof(StartDateFilter));
                _reservationsView?.Refresh();
            }
        }

        public string EndDateFilter
        {
            get => _endDateFilter;
            set
            {
                _endDateFilter = value;
                OnPropertyChanged(nameof(EndDateFilter));
                _reservationsView?.Refresh();
            }
        }

        public ReservationsViewModel()
        {
            _reservationService = new ReservationService();
            Reservations = new ObservableCollection<Reservation>();

            InitializeCommands();
            LoadReservations();
        }

        private void InitializeCommands()
        {
            AddReservationCommand = new RelayCommand(ExecuteAddReservation);
            DeleteReservationCommand = new RelayCommand(ExecuteDeleteReservation, CanExecuteReservationAction);
            FinishReservationCommand = new RelayCommand(ExecuteFinishReservation, CanExecuteReservationAction);
        }

        private void LoadReservations()
        {
            var loadedReservations = _reservationService.reservationRepository.GetAll();
            if (loadedReservations != null)
            {
                Reservations.Clear();
                foreach (var reservation in loadedReservations)
                {
                    Reservations.Add(reservation);
                }

                _reservationsView = CollectionViewSource.GetDefaultView(Reservations);
                _reservationsView.Filter = FilterReservations;
            }
        }

        private bool FilterReservations(object item)
        {
            if (item is not Reservation reservation) return false;

            bool roomMatch = string.IsNullOrEmpty(RoomNumberFilter) ||
                           reservation.RoomNumber.IndexOf(RoomNumberFilter, StringComparison.OrdinalIgnoreCase) >= 0;

            bool startDateMatch = string.IsNullOrEmpty(StartDateFilter) ||
                                reservation.StartDateTime.ToShortDateString().Contains(StartDateFilter);

            bool endDateMatch = string.IsNullOrEmpty(EndDateFilter) ||
                              reservation.EndDateTime.ToShortDateString().Contains(EndDateFilter);

            return roomMatch && startDateMatch && endDateMatch;
        }

        private void ExecuteAddReservation(object parameter)
        {
            var addReservationWindow = new AddReservations();
            if (addReservationWindow.ShowDialog() == true)
            {
                LoadReservations();
            }
        }

        private void ExecuteDeleteReservation(object parameter)
        {
            if (SelectedReservation == null) return;

            var deleteReservationWindow = new DeleteReservation(SelectedReservation);
            if (deleteReservationWindow.ShowDialog() == true)
            {
                LoadReservations();
            }
        }

        private void ExecuteFinishReservation(object parameter)
        {
            if (SelectedReservation == null) return;

            var finishReservationWindow = new FinishReservation(SelectedReservation);
            if (finishReservationWindow.ShowDialog() == true)
            {
                LoadReservations();
            }
        }

        private bool CanExecuteReservationAction(object parameter)
        {
            return SelectedReservation != null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}