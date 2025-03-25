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
using FinalProject.Admin;
using Repositories.Models;
using Services;

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for CashierScreen.xaml
    /// </summary>
    public partial class CashierScreen : Window
    {
        private DeviceService deviceService = new();
        private InvoiceService invoiceService = new();
        private HistoryUsedDeviceService historyUsedDeviceService = new();
        private HistoryBuyGoodService historyBuyGoodService = new();

        public CashierScreen()
        {
            InitializeComponent();
        }




        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceManageScreen im = new();
            if (im.ShowDialog() == false)
            {

            }
        }

        public void FillItcList()
        {
            itcListDevice.ItemsSource = deviceService.GetAllDevice();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillItcList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var border = sender as Border;
            string sid = Application.Current.Properties["StaffId"] as string;
            
            if (border != null && border.DataContext is Device device)
            {
                CalculateInvoiceScreem ci = new();
                ci.device = device;
                ci.txbStaff.Text = sid;
                ci.txbStaff.IsEnabled = false;
                HistoryUsedDevice hud = historyUsedDeviceService.GetDeviceRunning(device.Did);
                Invoice invoice = null;
                if(hud != null)
                {
                     invoice = invoiceService.GetById((int)hud.InvoiceId);

                }
                if(invoice != null)
                {
                    
                    ci._invoice = invoice;
                    ci.tbxInvoiceId.Text = invoice.IId.ToString();
                    ci.txbDevice.Text = device.Did.ToString();
                    ci.dpkDate.Text = invoice.InvoiceDate.ToString();
                    ci.txbCustomer.Text = invoice.CustomerId.ToString();
                    ci.txbStaff.Text = invoice.StaffId.ToString();
                    ci.dgvGood.ItemsSource = historyBuyGoodService.GetByInvoiceId(invoice.IId);
                    ci.timeInput.Text = hud.Start.ToString();
                    ci.tbxInvoiceId.IsReadOnly = true;
                    ci.txbDevice.IsReadOnly = true;
                    ci.dpkDate.IsEnabled = false;
                    ci.txbCustomer.IsReadOnly = true;
                    ci.txbStaff.IsReadOnly = true;
                    ci.btnSetFrom.IsEnabled = false;
                    
                }
                ci.dpkDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                ci.dpkDate.IsEnabled = false;
                //Invoice invoice = invoiceService.GetById();
                ci.txbDevice.Text = device.Did.ToString();
                if (ci.ShowDialog() == false)
                {
                    FillItcList();
                }
            }

        }
    }
}
