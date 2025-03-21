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

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for CashierScreen.xaml
    /// </summary>
    public partial class CashierScreen : Window
    {
        private InvoiceService invoiceService = new();
        public CashierScreen()
        {
            InitializeComponent();
        }

    
        

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceManageScreen im = new();
            if(im.ShowDialog() == false)
            {

            }
        }
    }
}
