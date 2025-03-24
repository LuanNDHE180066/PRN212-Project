using FinalProject.Cashier;
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
            LoadDataGrid(staffService.GetAll());
        }
        public void LoadDataGrid(List<Staff> staffs) 
        {
            dtgStaff.ItemsSource = staffs;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WorkingHistoryScreen history = new WorkingHistoryScreen();
            history.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            List<Staff> currentStaffs  = dtgStaff.ItemsSource as List<Staff>;
            LoadDataGrid(currentStaffs.OrderBy(s => s.SName).ToList());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            List<Staff> currentStaffs = dtgStaff.ItemsSource as List<Staff>;
            LoadDataGrid(currentStaffs.OrderBy(s => s.StartDate).ToList());
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtName.Text;
            List<Staff> staffs = staffService.GetAll();
            LoadDataGrid(staffs.Where(s=> s.SName.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ExpenditureGenaral expenditureGenaral = new ExpenditureGenaral();
            this.Close();
            expenditureGenaral.ShowDialog();
        }

        private void btnExp_Click(object sender, RoutedEventArgs e)
        {
            GoodManageScreen goodManageScreen = new GoodManageScreen();
            this.Close();
            goodManageScreen.ShowDialog();
        }

        private void btnGood_Click(object sender, RoutedEventArgs e)
        {
            CashierScreen cashierScreen = new CashierScreen();
            this.Close();
            cashierScreen.ShowDialog();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerScreen manageCustomerScreen   = new ManageCustomerScreen();
            manageCustomerScreen.ShowDialog();
            this.Close();
        }

        private void btnDevice_Click(object sender, RoutedEventArgs e)
        {
            DeviceManageScreen deviceManageScreen = new DeviceManageScreen();
            deviceManageScreen.ShowDialog();    
            this.Close();
        }
    }
}
