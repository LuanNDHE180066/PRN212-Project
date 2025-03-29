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
    /// Interaction logic for UpdateDeviceType.xaml
    /// </summary>
    public partial class UpdateDeviceType : Window
    {
        public DeviceType currenttype;
        private DeviceTypeService service = new DeviceTypeService();
        public UpdateDeviceType(DeviceType type)
        {
            InitializeComponent();
            currenttype = type;
            loadTypeData();
        }
        public void loadTypeData()
        {
            if (currenttype == null)
            {
                MessageBox.Show("Information of device is null");
                return;
            }
            txtDid.Text = currenttype.DtId.ToString();
            txtDtName.Text = currenttype.DtName;
            txtDetail.Text = currenttype.Detail;    
            cbStatus.SelectedIndex = currenttype.Status == 1 ? 0 : 1;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DeviceTypeManage deviceTypeManage = new DeviceTypeManage(); 
            deviceTypeManage.Show();    
            this.Close();
        }
        
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtDetail.Text) || string.IsNullOrEmpty(txtDtName.Text) ||
                cbStatus.SelectedItem == null)
                
            {
                MessageBox.Show("Please fill out the below fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                loadTypeData();
                return;
            }
            var x = service.getDeviceTypeByName(txtDtName.Text);
            if (x != null)
            {
                MessageBox.Show("Type is exsited!");
                loadTypeData();
                return;
            }
            string? Dtname = txtDtName.Text;
            string? Detail = txtDetail.Text;    
            int? Status = cbStatus.SelectedIndex == 0? 1: 0;

            MessageBoxResult result = MessageBox.Show(
                "Are you sure to update this Devicetype?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            var devicetype = currenttype;
            devicetype.Detail = Detail;
            devicetype.DtName = Dtname;
            devicetype.Status = Status;
            service.UpdateDeviceType(devicetype);
            MessageBox.Show("Update Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
