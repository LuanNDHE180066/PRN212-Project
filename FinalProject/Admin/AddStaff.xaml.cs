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
    /// Interaction logic for AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        private RoleService roleService= new RoleService();
        private StaffService staffService= new StaffService();
        public AdminScreen AdminScreen { get; set; }
        public AddStaff()
        {
            InitializeComponent();
            this.Resources["roles"] = roleService.GetAll();
            lbScreen.Content = "Add Staff";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address= txtAddress.Text;
            string phone = txtPhone.Text;
            string password = txtPassword.Text;
            string username = txtUsername.Text;
            int role = int.Parse(cbRole.SelectedValue.ToString());
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
                return;
            }
            if(staffService.isExistPhone(phone) || staffService.isExistUsername(username) || staffService.isExistAddress(address)){
                MessageBox.Show("Thông tin đã tồn tại trong hệ thống");
                return;
            }
            var confirm = MessageBox.Show("Bạn chắc chắn chưa","Cảnh báo", MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if(confirm == MessageBoxResult.No)
            {
                return;
            }
            DateOnly? startDate = DateOnly.FromDateTime(DateTime.Now);
            Staff staff = new Staff() {SName = name, Phone = phone,Address = address,StartDate = startDate, Username=username,Password=password,Status=1, Roleid = role };
            staffService.AddStaff(staff);
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AdminScreen.LoadDataGrid();
        }
    }
}
