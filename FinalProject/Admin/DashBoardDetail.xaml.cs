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
    /// Interaction logic for DashBoardDetail.xaml
    /// </summary>
    public partial class DashBoardDetail : Window
    {
        HistoryBuyGoodService HistoryBuyGoodService = new HistoryBuyGoodService();
        HistoryUsedDeviceService HistoryUsedDeviceService = new HistoryUsedDeviceService();
        ExpenditureService ExpenditureService = new ExpenditureService();
        public DashBoardDetail()
        {
            InitializeComponent();
            listGoods.ItemsSource = HistoryBuyGoodService.getAllDistinctGood();
            dgvUsedDevice.ItemsSource = HistoryUsedDeviceService.GetHistoryUsedDevices();
            listExpense.ItemsSource = ExpenditureService.GetAll();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DashBoardScreen dashBoardScreen = new DashBoardScreen();
            dashBoardScreen.Show(); 
            this.Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var selectedYear = cbxYear.SelectedItem as ComboBoxItem;
            var selectedMonthIndex = cbxMonth.SelectedIndex;

           
            if (selectedYear == null || selectedMonthIndex == 0)
            {
                listGoods.ItemsSource = HistoryBuyGoodService.getAllDistinctGood();
                dgvUsedDevice.ItemsSource = HistoryUsedDeviceService.GetHistoryUsedDevices();
                listExpense.ItemsSource = ExpenditureService.GetAll();
                return;
            }

            
            if (selectedYear != null && selectedMonthIndex > 0)  
            {
                int year = int.Parse(selectedYear.Content.ToString());
                int month = selectedMonthIndex; 

                listGoods.ItemsSource = HistoryBuyGoodService.getListGoodByYearAndMonth(year, month);
                dgvUsedDevice.ItemsSource = HistoryUsedDeviceService.getListHistoryUsedDeviceByYearAndMonth(year, month);
                listExpense.ItemsSource = ExpenditureService.getListHistoryExpenditureByYearAndMonth(year, month);
            }
        }
    }
}
