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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ResetPasswordScreen.xaml
    /// </summary>
    public partial class ResetPasswordScreen : Window
    {

        private CustomerService customerService = new();
        private TokenService tokenService = new();
        public ResetPasswordScreen()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Username can not be blank", "", MessageBoxButton.OK);
                return;
            }
            Customer c = customerService.GetByUsername(txtUsername.Text.Trim());
            if (c != null)
            {
                MessageBox.Show("Reset password link is sent to your email!", "", MessageBoxButton.OK);
                MailService.SendEmailResetPass(c.Email, c.Cid);
                return;
            }
            else
            {
                MessageBox.Show("Username is not existed!", "", MessageBoxButton.OK);
            }
        }
    }
}
