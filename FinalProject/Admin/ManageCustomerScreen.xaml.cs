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

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for ManageCustomerScreen.xaml
    /// </summary>
    public partial class ManageCustomerScreen : Window
    {
        private CustomerService customerService = new CustomerService();
        public ManageCustomerScreen()
        {
            InitializeComponent();
        }
        public void loadCustomer()
        {
            var customers = customerService.getAllCustomer();
            dgvDisplay.ItemsSource = customers;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadCustomer();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedCustomer = dgvDisplay.SelectedItem as Customer;

            // Kiểm tra xem có khách hàng nào được chọn không
            if (selectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Chuyển sang trang cập nhật và truyền dữ liệu khách hàng
            UpdateCustomer updatePage = new UpdateCustomer(selectedCustomer);
            updatePage.Show();
            this.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filterText = tbxfilter.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(filterText))
            {
                
                loadCustomer() ;
            }

          
            var filteredList = customerService.getAllCustomer()
                                .Where(c => c.CName.ToLower().Contains(filterText) ||
                                            c.Phone.Contains(filterText)) 
                                .ToList();

            dgvDisplay.ItemsSource = filteredList;

            if (filteredList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnOrderByUsername_Click(object sender, RoutedEventArgs e)
        {
            var sortedList = customerService.getAllCustomer()
                        .OrderBy(c => c.Username) 
                        .ToList();

            dgvDisplay.ItemsSource = sortedList;
        }

        private void btnOrderByHours_Click(object sender, RoutedEventArgs e)
        {
            var sortedList = customerService.getAllCustomer()
                        .OrderBy(c => c.Hours) 
                        .ToList();

            dgvDisplay.ItemsSource = sortedList;
        }
    }
}
