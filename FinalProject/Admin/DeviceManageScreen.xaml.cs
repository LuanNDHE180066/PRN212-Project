using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for DeviceManageScreen.xaml
    /// </summary>
    public partial class DeviceManageScreen : Window
    {
        public DeviceService devviceService = new DeviceService();
        public DeviceTypeService typeService = new DeviceTypeService();
        public DeviceManageScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDevice();
            loadDeviceType();
           
            
        }
        
        
        public void loadDevice()
            {
            var devices = devviceService.GetAllDevice();
            listDevice.ItemsSource = devices;   
            }
        public void loadDeviceType()
        {
            var devicetype = typeService.GetAllDeviceType();
            cbxCategory.ItemsSource = devicetype;
        }

        
       
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDevice addDevice = new AddDevice();  
            addDevice.Show();
            this.Close();
        }

        private void btnDeviceType_Click(object sender, RoutedEventArgs e)
        {
            DeviceTypeManage deviceTypeManage = new DeviceTypeManage();
            deviceTypeManage.Show();
            this.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
           
            int? selectedTypeId = cbxCategory.SelectedValue as int?;
            

            var filteredDevice = PrnFinalProjectContext.Ins.Devices
                .Include(d => d.Type)
                .Where(d =>
                    (!selectedTypeId.HasValue || d.Typeid == selectedTypeId)    
                )
                .ToList();

            listDevice.ItemsSource = filteredDevice;
        }

        

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Device selectedDevice)
            {
                
                UpdateDevice updateWindow = new UpdateDevice(selectedDevice);
                updateWindow.ShowDialog();
                this.Close();
            }
        }

        
    }
}
