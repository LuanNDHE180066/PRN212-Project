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
    /// Interaction logic for UpdateStaff.xaml
    /// </summary>
    public partial class UpdateStaff : Window
    {
        private RoleService roleService = new RoleService();
        private StaffService staffService = new StaffService();
        public Staff Staff { get; set; }
        public AdminScreen AdminScreen { get; set; }
        public UpdateStaff(Staff staff)
        {
            InitializeComponent();
            this.Staff = staff;
            this.Resources["roles"] = roleService.GetAll();
            lbScreen.Content = "Update Staff";
            this.DataContext = staff;
        }
 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ");
                return;
            }
            if(staffService.isExistPhoneExceptStaff(phone,Staff) ||
                staffService.isExistAddressExceptStaff(address,Staff))
            {
                MessageBox.Show("Thông tin đã có trong hệ thống");
                return;
            }
            int? status = int.Parse(cbStatus.SelectedValue.ToString());
            int role = int.Parse(cbRole.SelectedValue.ToString());
            Staff.SName = name;
            Staff.Address = address;
            Staff.Phone = phone;
            Staff.Roleid = role;
            if(status ==0 && Staff.Status.GetValueOrDefault()==1)
            {
                Staff.EndDate = DateOnly.FromDateTime(DateTime.Now);
            }
            Staff.Status = status;
            staffService.UpdateStaff(Staff);
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.AdminScreen.LoadDataGrid();
        }
    }
}
