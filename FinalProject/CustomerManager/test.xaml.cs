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
using Services;

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        public test()
        {
            InitializeComponent();
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string token = MailService.GenerateToken();
            string expiration = MailService.ExpireDateTime().ToString();
            string toEmail = "luanndhe180066@fpt.edu.vn";

            string subject = "Mã xác nhận của bạn quỳ xuống";
            string body = $"Mã xác nhận: {token} <br> Hết hạn lúc: {expiration}";

            MailService.SendEmail(toEmail, subject, body);
        }
    }
}
