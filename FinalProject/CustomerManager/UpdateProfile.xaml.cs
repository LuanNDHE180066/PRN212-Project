using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Window
    {
        CustomerService CustomerService = new CustomerService();
        MailService mailService = new MailService();
        public UpdateProfile()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            int cid = int.Parse(Application.Current.Properties["customerId"] as string);
            Customer customer = CustomerService.GetCustomerByID(cid);
            txtFullName.Text = customer.CName;
            txtUsername.Text = customer.Username;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please fill out all of field", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                
                Customer customer = CustomerService.GetCustomerByID(cid);
                if (customer == null)
                {
                    MessageBox.Show("Customer isn't existed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadData();
                    return;
                }
                if(IsValidPhoneNumber(txtPhone.Text.Trim()) == false)
                {
                    MessageBox.Show("Invalid phone only contain number and 10 numbers", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadData() ;
                    return;
                }
                if (IsValidEmail(txtEmail.Text.Trim()) == false)
                {
                    MessageBox.Show("Invalid email example like abc@gmail.com", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadData();
                    return;
                }
                customer.CName = txtFullName.Text.Trim();
                customer.Username = txtUsername.Text.Trim();
                customer.Email = txtEmail.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();

                CustomerService.UpdateCustomer(customer);
                MessageBox.Show("Update Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OrderFood orderFood = new OrderFood();
            orderFood.Show();
            this.Close();
        }
    }
}
