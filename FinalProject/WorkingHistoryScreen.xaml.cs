using FinalProject.Admin;
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
    /// Interaction logic for WorkingHistoryScreen.xaml
    /// </summary>
    public partial class WorkingHistoryScreen : Window
    {
        private StaffService staffService = new StaffService();
        private WorkingHistoryService WorkingHistoryService = new WorkingHistoryService();
        public WorkingHistoryScreen()
        {
            InitializeComponent();
            LoadDataGrid(staffService.WorkingHistoryDTOs());
        }
        public void LoadDataGrid(List<WorkingHistoryDTO> list)
        {
            dtgHistory.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selected = dtgHistory.SelectedItem as WorkingHistoryDTO;
            WorkingHistoryDetailScreen workingHistoryDetailScreen = new WorkingHistoryDetailScreen(selected);
            workingHistoryDetailScreen.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var list = dtgHistory.ItemsSource as List<WorkingHistoryDTO>;
            LoadDataGrid(list.OrderBy(s => s.Name).ToList());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var list = dtgHistory.ItemsSource as List<WorkingHistoryDTO>;
            LoadDataGrid(list.OrderByDescending(s => s.Total).ToList());
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchName = txtName.Text;
            var list = staffService.WorkingHistoryDTOs();
            LoadDataGrid(list.Where(s => s.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AdminScreen adminScreen = new AdminScreen();
            this.Hide();
            adminScreen.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ExpenditureGenaral expenditureGenaral = new ExpenditureGenaral();
            this.Hide();
            expenditureGenaral.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(); 
        }
    }
}
