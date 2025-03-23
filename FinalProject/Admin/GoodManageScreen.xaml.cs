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
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for GoodManageScreen.xaml
    /// </summary>
    public partial class GoodManageScreen : Window
    {
        private GoodService goodService = new GoodService();
        private GoodTypeService goodTypeService = new GoodTypeService();

        public GoodManageScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itcGoods.ItemsSource = goodService.GetAllActiveGood();
            List<GoodType> listGoodType = new List<GoodType>();
            GoodType gt = new GoodType() { Gtid = 0, GtName = "All" };
            listGoodType.Add(gt);
            foreach (GoodType item in goodTypeService.GetGoodTypes())
            {
                listGoodType.Add(item);
            }
            cbxCategory.ItemsSource = listGoodType;
            cbxCategory.SelectedIndex = 0;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;

            if (border != null && border.DataContext is Good selectedGood)
            {
                UpdateGoodsScreen u = new UpdateGoodsScreen();
                u._selectedBook = selectedGood;
                if (u.ShowDialog() == false)
                {
                    FillItcGood();
                }
            }
        }

        public void FillItcGood()
        {
            itcGoods.ItemsSource = goodService.GetAllActiveGood();
            List<GoodType> listGoodType = new List<GoodType>();
            GoodType gt = new GoodType() { Gtid = 0, GtName = "All" };
            listGoodType.Add(gt);
            foreach (GoodType item in goodTypeService.GetGoodTypes())
            {
                listGoodType.Add(item);
            }
            cbxCategory.ItemsSource = listGoodType;
            cbxCategory.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateGoodsScreen u = new UpdateGoodsScreen();
            u.tblHeader.Text = "Add new Good";
            u.spnGoodId.Visibility = Visibility.Hidden;
            u.cbxType.SelectedIndex = 0;
            if(u.ShowDialog() == false)
            {
                u._selectedBook = null;
                FillItcGood();
            }   
        }

        private void btnGoodType_Click(object sender, RoutedEventArgs e)
        {
            GoodsTypeScreen g = new();
            if(g.ShowDialog() == false)
            {
                cbxCategory.ItemsSource = goodTypeService.GetGoodTypes();
                FillItcGood();
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            List<Good> list = goodService.GetAllActiveGood();
            if (!txbName.Text.Trim().IsNullOrEmpty())
            {
                list = list.Where(x => x.GName.ToLower().Contains(txbName.Text.Trim().ToLower())).ToList();
            }

            if (!txbFromPrice.Text.Trim().IsNullOrEmpty())
            {
                decimal price = decimal.Parse(txbFromPrice.Text.Trim());
                list = list.Where(x => x.UnitPrice >= price).ToList();
            }

            if (!txbToPrice.Text.Trim().IsNullOrEmpty())
            {
                decimal price = decimal.Parse(txbToPrice.Text.Trim());
                list = list.Where(x => x.UnitPrice <= price).ToList();
            }

            int cate = int.Parse(cbxCategory.SelectedValue.ToString());



            if (cate > 0)
            {
                list = list.Where(x => x.Typeid == cate).ToList();
            }

            //MessageBox.Show($"{cate}");

            itcGoods.ItemsSource = list;

        }
    }
}
