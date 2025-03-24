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
    /// Interaction logic for WorkingHistoryDetailScreen.xaml
    /// </summary>
    public partial class WorkingHistoryDetailScreen : Window
    {
        public WorkingHistoryDTO workingHistoryDTO;
        private WorkingHistoryService workingHistoryService= new WorkingHistoryService();
        public WorkingHistoryDetailScreen(WorkingHistoryDTO workingHistoryDTO)
        {
            InitializeComponent();
            this.workingHistoryDTO = workingHistoryDTO;
            LoadData();
        }
        public void LoadData()
        {
            FillText();
            dtgWorkDetail.ItemsSource = workingHistoryService.GetByStaffId(workingHistoryDTO.Id);
        }
        public void FillText()
        {
            tbName.Text = this.workingHistoryDTO.Name;
            tbStartDate.Text = workingHistoryDTO.StartDate.ToString();
            tbEndDate.Text = workingHistoryDTO.EndDate.ToString();
            tbTotal.Text = workingHistoryDTO.Total.ToString();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            var selected = datePicker.SelectedDate;
            dtgWorkDetail.ItemsSource = workingHistoryService.GetByStaffIdAndDate(workingHistoryDTO.Id, selected.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<WorkingHistory> workingHistories = dtgWorkDetail.ItemsSource as List<WorkingHistory>;
            dtgWorkDetail.ItemsSource = workingHistories.OrderByDescending(s => s.Date).ToList();
        }
    }
}
