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
    /// Interaction logic for InvoiceManageScreen.xaml
    /// </summary>
    public partial class InvoiceManageScreen : Window
    {
        private InvoiceService invoiceService = new InvoiceService();
        public InvoiceManageScreen()
        {
            InitializeComponent();
        }

        private void dgvInvoiceList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Invoice selectedInvoice = dgvInvoiceList.SelectedItem as Invoice;
            if(selectedInvoice != null)
            {
                InvoiceDetailScreen indc = new();
                indc.invoice = selectedInvoice;
                indc.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgvInvoiceList.ItemsSource = invoiceService.GetAll();
        }
    }
}
