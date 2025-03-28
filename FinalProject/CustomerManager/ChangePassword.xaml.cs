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

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        CustomerService customerService = new CustomerService();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtcurrentpass.Text) ||
                string.IsNullOrEmpty(txtnewpass.Text) ||
                string.IsNullOrEmpty(txtconfirmpassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Are you sure to update this information?", "Confirm",
                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            try
            {
                int cid = int.Parse(Application.Current.Properties["customerId"] as string);

                Customer customer = customerService.GetCustomerByID(cid);
                if (customer == null)
                {
                    MessageBox.Show("Customer isn't existed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(customer.Password != txtcurrentpass.Text)
                {
                    MessageBox.Show("Customer isn't existed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                customer.Password = txtconfirmpassword.Text;
                customerService.UpdateCustomer(customer);
                MessageBox.Show("Update Successfully!", "Annouce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("");
            }
        }
    }
}
