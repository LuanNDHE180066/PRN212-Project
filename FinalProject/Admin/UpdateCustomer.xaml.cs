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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        private CustomerService customerService = new CustomerService();
        private Customer currentCustomer;
        public UpdateCustomer(Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            loadCustomerData();
        }
        public void loadCustomerData()
        {
            if (currentCustomer == null)
            {
                MessageBox.Show("Information of Customer is null", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            txtcid.Text = currentCustomer.Cid.ToString();
            txtcName.Text = currentCustomer.CName;
            txtHours.Text = currentCustomer.Hours.ToString();
            txtPhone.Text = currentCustomer.Phone;
            txtEmail.Text = currentCustomer.Email;
            txtUsername.Text = currentCustomer.Username;
            txtPasword.Text = currentCustomer.Password;
            cbStatus.SelectedIndex = currentCustomer.Status == 1 ? 0 : 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtcName.Text) || string.IsNullOrEmpty(txtHours.Text) ||
       string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
       string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPasword.Text) ||
       cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill out the below fields", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? CName = txtcName.Text;
            int? Hours = int.Parse(txtHours.Text);
            string? Phone = txtPhone.Text;
            string? Email = txtEmail.Text;
            string? Username = txtUsername.Text;
            string? Password = txtPasword.Text;
            int? Status = cbStatus.SelectedIndex == 0 ? 1 : 0;

            MessageBoxResult result = MessageBox.Show(
                "Are you sure to update this Customer?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            var customer = currentCustomer;
            customer.CName = CName;
            customer.Hours = Hours;
            customer.Phone = Phone;
            customer.Email = Email;
            customer.Username = Username;
            customer.Password = Password;
            customer.Status = Status;

            customerService.UpdateCustomer(customer);
            MessageBox.Show("Update Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerScreen manageCustomerScreen = new ManageCustomerScreen();
            manageCustomerScreen.Show();
            this.Close();
        }
    }
    
}
