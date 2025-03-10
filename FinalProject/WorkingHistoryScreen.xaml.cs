using FinalProject.Admin;
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
        public WorkingHistoryScreen()
        {
            InitializeComponent();
            LoadDataGrid();

        }
        public void LoadDataGrid()
        {
            List<WorkingHistoryDTO> list = staffService.WorkingHistoryDTOs();
            dtgHistory.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selected = dtgHistory.SelectedItem as WorkingHistoryDTO;
            WorkingHistoryDetailScreen workingHistoryDetailScreen  = new WorkingHistoryDetailScreen(selected);
            workingHistoryDetailScreen.ShowDialog();
        }
    }
}
