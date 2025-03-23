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
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for UpdateGoodType.xaml
    /// </summary>
    public partial class UpdateGoodType : Window
    {

        public GoodType goodType = null;
        private GoodTypeService gtService = new GoodTypeService();
        public UpdateGoodType()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            GoodType gt = new();
            string gtName = txbGoodName.Text.Trim();
            gt.GtName = gtName;
            if (gtName.IsNullOrEmpty())
            {
                MessageBox.Show("Làm ơn nhập vào ô trống", "Chuỗi rỗng", MessageBoxButton.OK);
                return;
            }
            if (goodType != null)
            {
                gt.Gtid = int.Parse(txbGoodTypeId.Text);
                if (gtService.UpdateGoodType(gt))
                {
                    MessageBox.Show("Cập nhật thành công", "Thành công", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Thất bại", MessageBoxButton.OK);
                }
            }
            else
            {
                if (gtService.AddGoodType(gt))
                {
                    MessageBox.Show("Thêm thành công", "Thành công", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Thất bại", MessageBoxButton.OK);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (goodType != null)
            {
                txbGoodTypeId.IsEnabled = false;
                txbGoodTypeId.Text = goodType.Gtid.ToString();
                txbGoodName.Text = goodType.GtName;
            }
            else
            {
                txbGoodTypeId.Visibility = Visibility.Hidden;
                tblGtId.Visibility = Visibility.Hidden;
            }
        }
    }
}
