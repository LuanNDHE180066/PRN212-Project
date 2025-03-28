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
    /// Interaction logic for ManagerScreen.xaml
    /// </summary>
    public partial class ManagerScreen : Window
    {
        public ManagerScreen()
        {
            InitializeComponent();
        }

        private void btnDevice_Click(object sender, RoutedEventArgs e)
        {
            DeviceManageScreen deviceManageScreen = new DeviceManageScreen();
            deviceManageScreen.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomerScreen manageCustomerScreen = new ManageCustomerScreen();
            manageCustomerScreen.Show();
            this.Close();
        }
        private void btnExpenditure_Click(object sender, RoutedEventArgs e)
        {
            ExpenditureGenaral expenditureGenaral = new ExpenditureGenaral();
            this.Close();
            expenditureGenaral.ShowDialog();
        }

        private void btnGood_Click(object sender, RoutedEventArgs e)
        {
            GoodManageScreen gm = new();
            gm.ShowDialog();
        }
    }
}
