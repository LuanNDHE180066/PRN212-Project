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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        private CustomerService customer = new CustomerService();
        public AddCustomer()
        {
            InitializeComponent();
        }
        public Customer getCustomer()
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(txtHours.Text) ||
                    string.IsNullOrWhiteSpace(txtcName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPasword.Text) ||
                     string.IsNullOrWhiteSpace(txtPhone.Text) ||
                     string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Hãy điền hết tất cả các ô và chọn đầy đủ các giá trị!");
                    return null;
                }
                string? CName = txtcName.Text;
                int? Hours = int.Parse(txtHours.Text);
                string? Phone = txtPhone.Text;
                string? Username = txtUsername.Text;
                string? Email = txtEmail.Text;
                string? Password = txtPasword.Text;
                int? Status = cbStatus.SelectedIndex == 0 ? 1 : 0;
                return new Customer
                {
                    CName = CName,
                    Hours = Hours,
                    Phone = Phone,
                    Email = Email,
                    Password = Password,
                    Username = Username,
                    Status = Status
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private void clearForm()
        {

            txtcName.Clear();
            txtHours.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtPasword.Clear();
            cbStatus.SelectedIndex = -1;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var cus = getCustomer();
            if (cus != null)
            {
                int cid = int.Parse(txtcid.Text);
                var x = customer.GetCustomerByID(cid);
                if (x != null)
                {
                    clearForm();
                    MessageBox.Show("Khách hàng đã tồn tại");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(
               "Bạn có chắc chắn muốn thêm khách hàng này không?",
               "Confirmation",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        customer.AddCustomer(cus);
                        MessageBox.Show("Thêm khách hàng thành công");
                    }
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerScreen manageCustomerScreen = new ManageCustomerScreen();
            manageCustomerScreen.Show();
            this.Close();
        }
    }
}
        
    

