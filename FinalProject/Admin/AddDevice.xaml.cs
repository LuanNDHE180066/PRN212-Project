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
    /// Interaction logic for AddDevice.xaml
    /// </summary>
    public partial class AddDevice : Window
    {
        public DeviceTypeService deviceTypeService = new DeviceTypeService();
        public DeviceService deviceService = new DeviceService();   
        public AddDevice()
        {
            InitializeComponent();
        }

        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dv = getDevice();
            if (dv != null)
            {
                var x = deviceService.getDeviceByID(dv.Did);
                if (x != null)
                {
                    clearForm();
                    MessageBox.Show("Device is existed");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(
                "Are you sure to add this device?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        deviceService.AddDevice(dv);
                        MessageBox.Show("Add successfully");
                    }
                }
            }

        }
        private void clearForm()
        {
            cbTypeid.SelectedIndex = -1; 
            txtHours.Text = string.Empty;
            cbRunning.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1; 
            txtPricePerHour.Text = string.Empty;
        }
        public void loadDeviceType()
        {
            var types = deviceTypeService.GetAllDeviceType();
            cbTypeid.ItemsSource = types;
            cbTypeid.SelectedIndex = 0;
        }

        public Device getDevice()
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(txtHours.Text) ||
                    string.IsNullOrWhiteSpace(txtPricePerHour.Text) ||
                    cbTypeid.SelectedItem == null ||
                    cbRunning.SelectedItem == null ||
                    cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please fill out the feilds");
                    return null;
                }

                int? typeid = cbTypeid.SelectedValue as int?;
                int? hours = int.Parse(txtHours.Text);
                decimal? priceperHour = decimal.Parse(txtPricePerHour.Text);
                int? running = cbRunning.SelectedIndex == 0 ? 1 : 0;
                int? status = cbStatus.SelectedIndex == 0 ? 1 : 0;
                return new Device
                {
                    
                    Typeid = typeid,
                    Hours = hours,
                    Running = running,
                    Status = status,
                    PricePerHour = priceperHour
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDeviceType();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DeviceManageScreen deviceManageScreen = new DeviceManageScreen();   
            deviceManageScreen.Show();
            this.Close();
        }
    }
}
