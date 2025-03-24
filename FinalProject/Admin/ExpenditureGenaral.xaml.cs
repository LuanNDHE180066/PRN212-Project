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

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for ExpenditureGenaral.xaml
    /// </summary>
    public partial class ExpenditureGenaral : Window
    {
        private ExpenditureService expenditureService = new ExpenditureService();
        public ExpenditureGenaral()
        {
            InitializeComponent();
            LoadDataGrid(expenditureService.GetAll());
        }
        public void LoadDataGrid(List<Expenditure> list)
        {
            dtgExp.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime from = dpkFrom.SelectedDate ==null? DateTime.MinValue: dpkFrom.SelectedDate.Value;
            DateTime to = dpkTo.SelectedDate==null? DateTime.MaxValue: dpkTo.SelectedDate.Value;
            LoadDataGrid(expenditureService.GetByTime(from, to));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Expenditure> currentList = dtgExp.ItemsSource as List<Expenditure>;
            LoadDataGrid(expenditureService.OrderByTotalAsc(currentList));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Expenditure expenditure = dtgExp.SelectedItem as Expenditure;
            ExpenditureDetail expenditureDetail = new ExpenditureDetail(expenditure);
            expenditureDetail.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(expenditureService.GetAll());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ExpenditureDetail expenditureDetail  = new ExpenditureDetail();
            expenditureDetail.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WorkingHistoryScreen workingHistoryScreen = new WorkingHistoryScreen();
            this.Close();
            workingHistoryScreen.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            AdminScreen adminScreen = new AdminScreen();
            this.Close();
            adminScreen.ShowDialog();
        }
    }
}
