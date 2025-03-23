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
using Repositories.Models;
using Services;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for DeviceTypeManage.xaml
    /// </summary>
    public partial class DeviceTypeManage : Window
    {
        private DeviceTypeService typeService = new DeviceTypeService();
        public DeviceTypeManage()
        {
            InitializeComponent();
        }
        public void loadDeviceType()
        {
            var types = typeService.GetAllDeviceType();
            dgvDisplay.ItemsSource = types;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDeviceType();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDeviceType addDeviceType = new AddDeviceType();
            addDeviceType.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgvDisplay.SelectedItem is DeviceType selectedType)
            {
                UpdateDeviceType updatePage = new UpdateDeviceType(selectedType);
                updatePage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một loại máy để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DeviceManageScreen deviceManageScreen = new DeviceManageScreen();   
            deviceManageScreen.Show();
            this.Close();
        }
    }
}
