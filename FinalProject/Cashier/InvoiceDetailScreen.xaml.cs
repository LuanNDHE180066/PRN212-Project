using System;
using System.Collections;
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

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for InvoiceDetailScreen.xaml
    /// </summary>
    /// 

    public partial class InvoiceDetailScreen : Window
    {
        public Invoice invoice = null;

        private InvoiceService invoiceService = new();
        private HistoryBuyGoodService historyBuyGoodService = new();
        private HistoryUsedDeviceService historyUsedDeviceService = new();
        public InvoiceDetailScreen()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (invoice != null)
            {
                tbxInvoiceId.Text = invoice.IId.ToString();
                txbDate.Text = invoice.InvoiceDate.ToString();
                tbxTotal.Text = invoice.Total.ToString();
                txbCustomer.Text = invoice.Customer.CName;
                txbStaff.Text = invoice.Staff.SName;

                List<HistoryBuyGood> hg = historyBuyGoodService.GetByInvoiceId(invoice.IId);
                HistoryUsedDevice hd = historyUsedDeviceService.GetByInvoiceId(invoice.IId);
                txbFrom.Text = hd.Start.ToString();
                txbTo.Text = hd.End.ToString();
                txbTotalUseDevice.Text = hd.Amount.ToString();
                txbDateUse.Text = hd.Date.ToString();
                dgvGood.ItemsSource = hg;
                decimal totalGood = 0;
                foreach (HistoryBuyGood good in hg)
                {
                    totalGood += (decimal) good.Amount;
                }
                txbGoodTotal.Text =totalGood.ToString();

                //foreach (HistoryBuyGood hbg in hg)
                //{
                //    StackPanel stackPanel = new StackPanel();
                //    StackPanel stackPanel1 = new StackPanel();
                //    StackPanel stackPanel2 = new StackPanel();

                //    stackPanel.Orientation = Orientation.Horizontal;
                //    stackPanel1.Orientation = Orientation.Horizontal;
                //    stackPanel2.Orientation = Orientation.Horizontal;

                //    TextBlock tbl = new TextBlock();
                //    tbl.Text = "Good";
                //    TextBlock tbl1 = new();
                //    tbl1.Text = "Quantity";
                //    TextBlock tbl2 = new();
                //    tbl2.Text = "Amount";

                //    tbl.Width = 56;
                //    tbl1.Width = 60;
                //    tbl2.Width = 60;

                //    TextBox txb = new TextBox();
                //    txb.Text = $"{hbg.Goods.GName}";

                //    TextBox txb1 = new TextBox();
                //    txb1.Text = $"{hbg.Quantity}";

                //    TextBox txb2 = new TextBox();
                //    txb2.Text = $"{hbg.Amount}";

                //    txb1.Width = 70;
                //    txb2.Width = 70;
                //    txb1.Width = 70;


                //    stackPanel.Children.Add(tbl);
                //    stackPanel1.Children.Add(tbl1);
                //    stackPanel2.Children.Add(tbl2);

                //    stackPanel.Children.Add(txb);
                //    stackPanel1.Children.Add(txb1);
                //    stackPanel2.Children.Add(txb2);



                //    spnGoodList.Children.Add(stackPanel);
                //    spnGoodList.Children.Add(stackPanel2);
                //    spnGoodList.Children.Add(stackPanel1);
                //}
            }
        }
    }
}
