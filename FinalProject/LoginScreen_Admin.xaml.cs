using FinalProject.Admin;
using FinalProject.Cashier;
using FinalProject.CustomerManager;
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
        private RoleService roleService = new RoleService();
        private StaffService staffService = new StaffService();
        private CustomerService customerService = new CustomerService();
        private InvoiceService invoiceService = new InvoiceService();
        private  HistoryUsedDeviceService historyUsedDeviceService = new HistoryUsedDeviceService();
        private DeviceService deviceService = new DeviceService();
        public LoginScreen_Admin()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Password;
            Staff staff = staffService.GetByRoleUsernamePassword(username, password);
            Customer customer = customerService.GetCustomerLogin(username, password);
            if (staff != null)
            {
                Application.Current.Properties["StaffId"] = staff.Sid.ToString();
                if(staff.Roleid == 1)
                {
                    Admin.AdminScreen adminScreen = new Admin.AdminScreen();
                    this.Hide();
                    adminScreen.ShowDialog();
                }
                else if(staff.Roleid == 2)
                {
                    CashierScreen cashierScreen = new CashierScreen();
                    this.Hide();
                    cashierScreen.Show();
                }
                else
                {
                    ManagerScreen managerScreen = new ManagerScreen();
                    this.Hide();
                    managerScreen.ShowDialog();
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
        private static int deviceId = 1;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterCustomer customer = new RegisterCustomer();
            this.Close();
            customer.ShowDialog();
        }
    }
}
