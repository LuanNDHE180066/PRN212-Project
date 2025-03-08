using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen_Admin : Window
    {
        private RoleService roleService= new RoleService();
        private StaffService staffService= new StaffService();
        public LoginScreen_Admin()
        {
            InitializeComponent();
            LoadRole();
        }
        public void LoadRole()
        {
            List<Role> list = roleService.GetAll();
            cbRole.ItemsSource = list;
            cbRole.SelectedValuePath = "Rid";
            cbRole.DisplayMemberPath = "RName";
            cbRole.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? roleId = cbRole.SelectedValue as int?;
            if(roleId != null)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                Staff staff = staffService.GetByRoleUsernamePassword(username, password, roleId.Value);
                if(staff != null)
                {
                    Admin.AdminScreen adminScreen = new Admin.AdminScreen();
                    adminScreen.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Sai password or username");
                }
            }
        }
    }
}
