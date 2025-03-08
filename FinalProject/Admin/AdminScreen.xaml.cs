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
    /// Interaction logic for AdminScreen.xaml
    /// </summary>
    public partial class AdminScreen : Window
    {
        private StaffService staffService = new StaffService();
        public AdminScreen()
        {
            InitializeComponent();
            LoadDataGrid();
        }
        public void LoadDataGrid()
        {
            List<Staff> list = staffService.GetAll();
            dtgStaff.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin.AddStaff addStaff = new AddStaff() {AdminScreen =this };
            addStaff.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Staff staff = dtgStaff.SelectedItem as Staff;
            if(staff != null)
            {
                Admin.UpdateStaff updateStaff = new UpdateStaff(staff) { AdminScreen = this};
                updateStaff.ShowDialog();
            }
        }
    }
}
