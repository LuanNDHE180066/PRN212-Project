using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for HistoryOrder.xaml
    /// </summary>
    public partial class HistoryOrder : Window
    {
        private CustomerService customerService = new CustomerService();
        private HistoryBuyGoodService HistoryBuyGoodService = new HistoryBuyGoodService();
        private InvoiceService invoiceService = new();
        private HistoryBuyGoodService historyBuyGoodService = new HistoryBuyGoodService();
        public HistoryOrder()
        {
            InitializeComponent();
            int cid =  int.Parse(Application.Current.Properties["customerId"] as string);
            int invoiceId =  int.Parse(Application.Current.Properties["invoiceId"] as string);
            Customer customer = customerService.GetCustomerByID(cid);
            List<HistoryBuyGood> listOrder = historyBuyGoodService.GetByInvoiceId(invoiceId);
            lbName.Content = customer.CName;
            dtgHistoryOrder.ItemsSource = listOrder;    
            decimal? total =listOrder.Sum(s=>s.Amount);
            if (total != null)
            {
                btnTotal.Content = $"Total: {total}";
            }
            else
            {
                btnTotal.Content = $"Total: 0";
            }
        }
       
        
    }
}
