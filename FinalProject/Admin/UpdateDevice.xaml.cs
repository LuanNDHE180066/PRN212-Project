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
    /// Interaction logic for UpdateDevice.xaml
    /// </summary>
    public partial class UpdateDevice : Window
    {
        private Device currentDevice;
        private DeviceTypeService deviceTypeService = new DeviceTypeService();  
        private DeviceService deviceService = new DeviceService();  
        public UpdateDevice(Device device)
        {
            InitializeComponent();
            currentDevice = device;
            LoadDeviceData();
        }
        public void loadDeviceType()
        {
            var types = deviceTypeService.GetAllDeviceType();
            cbTypeid.ItemsSource = types;
            cbTypeid.SelectedIndex = 0;
        }
        private void LoadDeviceData()
        {
          
            txtHours.Text = currentDevice.Hours.ToString();
            txtPricePerHour.Text = currentDevice.PricePerHour.ToString();
            cbRunning.SelectedIndex = currentDevice.Running == 1 ? 0 : 1;
            cbStatus.SelectedIndex = currentDevice.Status == 1 ? 0 : 1;
            cbTypeid.SelectedValue = currentDevice.Typeid;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DeviceManageScreen deviceManageScreen = new DeviceManageScreen();
            deviceManageScreen.Show();
            this.Close();
        }
        private void clearForm()
        {
            cbTypeid.SelectedIndex = -1;
            txtHours.Text = string.Empty;
            cbRunning.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtPricePerHour.Text = string.Empty;
        }
        public Device getDevice()
        {
            return currentDevice;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            if (cbTypeid.SelectedValue == null || string.IsNullOrEmpty(txtHours.Text) ||
                cbRunning.SelectedItem == null || cbStatus.SelectedItem == null ||
                string.IsNullOrEmpty(txtPricePerHour.Text))
            {
                MessageBox.Show("Please fill out the below fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            int typeId = (int)cbTypeid.SelectedValue;
            int hours = int.Parse(txtHours.Text);
            int? running = cbRunning.SelectedIndex == 0 ? 1 : 0;
            int? status = cbStatus.SelectedIndex == 0 ? 1 : 0; 
            decimal pricePerHour = decimal.Parse(txtPricePerHour.Text);

            
            
            
            MessageBoxResult result = MessageBox.Show(
                "Are you sure to Update this device?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return; 
            }

            
            var device = getDevice();
            

            device.Typeid = typeId;
            device.Hours = hours;
            device.Running = running;
            device.Status = status;
            device.PricePerHour = pricePerHour;

            deviceService.UpdateDevice(device);
            MessageBox.Show("Update Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDeviceType();
        }
    }
}
