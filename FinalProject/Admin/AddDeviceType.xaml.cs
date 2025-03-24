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
    /// Interaction logic for AddDeviceType.xaml
    /// </summary>
    public partial class AddDeviceType : Window
    {
        private DeviceTypeService _typeService = new DeviceTypeService();
        public AddDeviceType()
        {
            InitializeComponent();
        }

        public DeviceType getDeviceType()
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(txtDtName.Text) ||
                    string.IsNullOrWhiteSpace(txtDetail.Text) ||
                    cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please fill out the fields");
                    return null;
                }
                string DtName = txtDtName.Text;
                string Detail = txtDetail.Text;
                int? Status = cbStatus.SelectedIndex == 0 ? 1 : 0;
                return new DeviceType
                {
                    DtName = DtName,
                    Detail = Detail,
                    Status = Status
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void clearForm()
        {
            txtDtName.Clear();
            txtDetail.Clear();
            cbStatus.SelectedIndex = -1;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dvtype = getDeviceType();
            if (dvtype != null)
            {
                var x = _typeService.getDeviceTypeByName(dvtype.DtName);
                if (x != null)
                {
                    clearForm();
                    MessageBox.Show("Deivcetype is existed");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(
                "Are you sure to add this devicetype?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _typeService.AddDeviceType(dvtype);
                        MessageBox.Show("Add sucessfully");
                    }
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DeviceTypeManage deviceTypeManage = new DeviceTypeManage();
            deviceTypeManage.Show();
            this.Close();
        }
    }
}

