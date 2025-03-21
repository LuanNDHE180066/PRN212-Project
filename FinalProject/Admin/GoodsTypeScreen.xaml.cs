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
    /// Interaction logic for GoodsTypeScreen.xaml
    /// </summary>
    public partial class GoodsTypeScreen : Window
    {

        private GoodTypeService goodTypeService = new GoodTypeService();



        public GoodsTypeScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtgGoodType.ItemsSource = goodTypeService.GetGoodTypes();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string search = txbName.Text.Trim();
            if (!search.IsNullOrEmpty())
            {
                while (search.Contains("  "))
                {
                    search = search.Replace("  ", " ");
                }

                dtgGoodType.ItemsSource = goodTypeService.GetGoodTypes().Where(x => x.GtName.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                MessageBox.Show("Nhập vào để tìm kiếm", "Chuỗi rỗng", MessageBoxButton.OK);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateGoodType g = new UpdateGoodType();
            g.btnSave.Content = "Add";
            if (g.ShowDialog() == false)
            {
                dtgGoodType.ItemsSource = goodTypeService.GetGoodTypes();
            }
        }

        private void dtgGoodType_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            UpdateGoodType g = new UpdateGoodType();
            g.goodType = dtgGoodType.SelectedItem as GoodType;

      
            if(g.ShowDialog() == false)
            {
                
            }
            //if (g.ShowDialog() == false)
            //{
            //    dtgGoodType.ItemsSource = goodTypeService.GetGoodTypes();
            //}
        }

        private void dtgGoodType_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateGoodType g = new UpdateGoodType();
            g.goodType = dtgGoodType.SelectedItem as GoodType;

            if(g.ShowDialog() == false)
            {
                dtgGoodType.ItemsSource = goodTypeService.GetGoodTypes();
                List<GoodType> listGoodType = new List<GoodType>();
                GoodType gt = new GoodType() { Gtid = 0, GtName = "All" };
                listGoodType.Add(gt);
            }
           
        }
    }
}
