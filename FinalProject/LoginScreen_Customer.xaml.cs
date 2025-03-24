using FinalProject.CustomerManager;
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
    /// Interaction logic for LoginScreen_Customer.xaml
    /// </summary>
    public partial class LoginScreen_Customer : Window
    {
        private CustomerService customerService= new CustomerService();
        private InvoiceService invoiceService= new InvoiceService();
        private HistoryUsedDeviceService historyUsedDeviceService = new HistoryUsedDeviceService();
        private int deviceId =1;
        public LoginScreen_Customer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Customer c = customerService.GetCustomerLogin(username, password);
            if(c != null )
            {
                Application.Current.Properties["customerId"]= c.Cid.ToString();
                int invoiceId = createInvoice(c.Cid);
                Application.Current.Properties["invoiceId"] = invoiceId.ToString();
                createUsedDevice(invoiceId);
                OrderFood orderFood = new OrderFood();
                this.Close();
                orderFood.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
                return;
            }
        }
        private int createInvoice(int cId)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            Invoice invoice = new Invoice() {InvoiceDate =date,CustomerId= cId,StaffId =1 };
            invoiceService.AddNewInvoice(invoice);
            return invoiceService.GetAll().Last().IId;
        }
        public void createUsedDevice(int invoiceId)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly start = TimeOnly.FromDateTime(DateTime.Now);
            HistoryUsedDevice history =new HistoryUsedDevice() { InvoiceId = invoiceId,DeviceId = deviceId,Date = date,Start=start} ;
            historyUsedDeviceService.AddHistoryUsedDevice(history);
        }
    }
}
