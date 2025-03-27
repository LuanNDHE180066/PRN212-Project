using Microsoft.IdentityModel.Tokens;
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

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for RegisterCustomer.xaml
    /// </summary>
    public partial class RegisterCustomer : Window
    {
        private CustomerService customerService= new CustomerService();
        public RegisterCustomer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string? name = txtName.Text;
            string? email = txtEmail.Text;
            string? phone = txtPhone.Text;
            string? username = txtUsername.Text;
            string? password = txtPassword.Password;
            if(name.IsNullOrEmpty() || email.IsNullOrEmpty() || phone.IsNullOrEmpty() || username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                MessageBox.Show("Không để trống thông tin");
                return;
            }
            if (customerService.isExistedUsername(username))
            {
                MessageBox.Show("Tài khoản đã tồn tại");
                return;
            }
            if(!customerService.isValidPassword(password) || !customerService.isValidUsername(username))
            {
                MessageBox.Show("Username và password phải trên 6 kí tự, password phải chứa số");
                return;
            }
            Customer customer = new Customer() {CName = name, Email= email,Phone=phone,Hours=0,Username=username,Password=password,Status =2 };
            customerService.AddCustomer(customer);
            MessageBox.Show("Đã gửi email xác nhận, Kiểm tra hòm thư của bạn");
        }
    }
}
