using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Repositories.Models;
using Services;
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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen_Admin : Window
    {
        private RoleService roleService= new RoleService();
        private StaffService staffService= new StaffService();
        public LoginScreen_Admin()
        {
            InitializeComponent();
            LoadRole();
        }
        public void LoadRole()
        {
            List<Role> list = roleService.GetAll();
            cbRole.ItemsSource = list;
            cbRole.SelectedValuePath = "Rid";
            cbRole.DisplayMemberPath = "RName";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? roleId = cbRole.SelectedValue as int?;
            if(roleId != null)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                Staff staff = staffService.GetByRoleUsernamePassword(username, password, roleId.Value);
                if(staff != null)
                {
                    Application.Current.Properties["StaffId"] = staff.Sid.ToString();
                    Admin.AdminScreen adminScreen = new Admin.AdminScreen();
                    this.Close();
                    adminScreen.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Sai password or username");
                }
            }
            else if(customer != null){
                Application.Current.Properties["customerId"] = customer.Cid.ToString();
                int invoiceId = createInvoice(customer.Cid);
                Application.Current.Properties["invoiceId"] = invoiceId.ToString();
                createUsedDevice(invoiceId);
                OrderFood orderFood = new OrderFood();
                this.Hide();
                orderFood.ShowDialog();
            }
            else
            {
                MessageBox.Show($"Sai password or username");
            }
        }
        private int createInvoice(int cId)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            Invoice invoice = new Invoice() { InvoiceDate = date, CustomerId = cId, StaffId = 1 };
            invoiceService.AddNewInvoice(invoice);
            return invoiceService.GetAll().Last().IId;
        }
        private static int deviceId = 5;
        public void createUsedDevice(int invoiceId)
        {
            Device device = deviceService.getDeviceByID(deviceId);
            device.Status = 2;
            deviceService.UpdateDevice(device);
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly start = TimeOnly.FromDateTime(DateTime.Now);
            HistoryUsedDevice history = new HistoryUsedDevice() { InvoiceId = invoiceId, DeviceId = deviceId, Date = date, Start = start };
            historyUsedDeviceService.AddHistoryUsedDevice(history);
        }
    }
}
