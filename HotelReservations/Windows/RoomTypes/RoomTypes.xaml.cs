using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RoomTypes.xaml
    /// </summary>
    public partial class RoomTypes : Window
    {
        private ICollectionView view;
        public RoomTypes()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            var roomTypeService = new RoomTypeService();
            var roomTypes = Hotel.GetInstance().RoomTypes.ToList();

            view = CollectionViewSource.GetDefaultView(roomTypes);
            view.Filter = DoFilter;

            RoomTypesDataGrid.ItemsSource = null;
            RoomTypesDataGrid.ItemsSource = roomTypes;
            RoomTypesDataGrid.IsSynchronizedWithCurrentItem = true;
            RoomTypesDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object roomObject)
        {
            var roomType = roomObject as RoomType;

            var roomTypeSearchParam = RoomTypeSearchTextBox.Text;

            if (roomType.Name.Contains(roomTypeSearchParam))
            {
                return true;
            }

            return false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addRoomTypeWindow = new AddEditRoomType();
            Hide();
            if (addRoomTypeWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var editRoomType = (RoomType)RoomTypesDataGrid.SelectedItem;
            if (editRoomType == null)
            {
                MessageBox.Show("Please select an RoomType.", "Select RoomType", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editRoomWindow = new AddEditRoomType(editRoomType);
            Hide();
            if (editRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteRoomType = (RoomType)RoomTypesDataGrid.SelectedItem;
            if (deleteRoomType == null)
            {
                MessageBox.Show("Please select an RoomType.", "Select RoomType", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deleteRoomWindow = new DeleteRoomType(deleteRoomType);
            Hide();
            if (deleteRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void RoomTypesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void RoomTypeSearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

    }
}
