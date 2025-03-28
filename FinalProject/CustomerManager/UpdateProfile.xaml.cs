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
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Window
    {
        CustomerService CustomerService = new CustomerService();
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
                    return;
                }

            
                customer.CName = txtFullName.Text.Trim();
                customer.Username = txtUsername.Text.Trim();
                customer.Email = txtEmail.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();

                CustomerService.UpdateCustomer(customer);

          
                    MessageBox.Show("Update Successfully!", "Annouce", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
