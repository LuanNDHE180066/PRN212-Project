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
using Services;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for DashBoardScreen.xaml
    /// </summary>
    public partial class DashBoardScreen : Window
    {
        ExpenditureService expenditureService = new ExpenditureService();
        InvoiceService invoiceService = new InvoiceService();   
        CustomerService customerService = new CustomerService();
        StaffService StaffService = new StaffService();
        GoodsService GoodService = new GoodsService();
        HistoryBuyGoodService historyBuyGoodService = new HistoryBuyGoodService();
        public DashBoardScreen()
        {
            InitializeComponent();
            tbxExpen.Text = expenditureService.GetTotal().ToString();
            tbxInvoice.Text    = invoiceService.getAllTotal().ToString();
            tbxCustomer.Text = customerService.CountCustomer().ToString();
            tbxStaff.Text = StaffService.CountStaff().ToString();
            tbxGoods.Text = GoodService.CountGoods().ToString();
            loadListGood();
        }
        public void loadListGood()
        {
            var goods = historyBuyGoodService.GetTop3BestSellingGoods();
            listGoods.ItemsSource = goods;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminScreen adminScreen = new AdminScreen();
            adminScreen.Show();
            this.Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedYear = cbxYear.SelectedItem as ComboBoxItem;
            var selectedMonthIndex = cbxMonth.SelectedIndex;

            if (selectedYear == null || selectedMonthIndex == 0)
            {
                tbxInvoice.Text = invoiceService.getAllTotal()?.ToString() ?? "0";
                tbxExpen.Text = expenditureService.GetTotal()?.ToString() ?? "0";
                return;
            }

            if (selectedYear != null && selectedMonthIndex >= 0)
            {
                int year = int.Parse(selectedYear.Content.ToString());
                int month = selectedMonthIndex;  

                
                decimal? totalInvoice = invoiceService.GetTotalByMonthAndYear(year, month);
                decimal? totalExpense = expenditureService.GetTotalByYearAndMonth(year, month);
                tbxInvoice.Text = totalInvoice.ToString();
                tbxExpen.Text = totalExpense.ToString();
            }
            
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            DashBoardDetail detail = new DashBoardDetail();
            detail.Show();
            this.Close();
        }
    }
}
