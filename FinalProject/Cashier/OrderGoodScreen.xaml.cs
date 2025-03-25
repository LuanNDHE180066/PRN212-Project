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
using FinalProject.Admin;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for OrderGoodScreen.xaml
    /// </summary>
    public partial class OrderGoodScreen : Window
    {
        public Invoice invoice = null;
        private GoodService goodService = new GoodService();
        private GoodTypeService goodTypeService = new GoodTypeService();
        private HistoryBuyGoodService historyBuyGoodService = new();
        private HistoryBuyGood _hbg = new();

        public OrderGoodScreen()
        {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //itcGoods.ItemsSource = goodService.GetAllActiveGood();
            //List<GoodType> listGoodType = new List<GoodType>();
            //GoodType gt = new GoodType() { Gtid = 0, GtName = "All" };
            //listGoodType.Add(gt);
            //foreach (GoodType item in goodTypeService.GetGoodTypes())
            //{
            //    listGoodType.Add(item);
            //}
            //cbxCategory.ItemsSource = listGoodType;
            //cbxCategory.SelectedIndex = 0;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(invoice != null)
            {
                var border = sender as Border;
                

                if (border != null && border.DataContext is Good selectedGood)
                {
                   
                    if(historyBuyGoodService.GetByInvoiceId(invoice.IId) == null)
                    {
                        HistoryBuyGood hbg = new HistoryBuyGood() { Date = invoice.InvoiceDate, InvoiceId = invoice.IId, GoodsId = selectedGood.Gid,
                        Quantity = 1, Amount = selectedGood.UnitPrice};
                        MessageBox.Show("Thêm thành công");
                        historyBuyGoodService.Add(hbg);
                    }
                    else
                    {
                        List<int?> goodIds = historyBuyGoodService.GetByInvoiceId(invoice.IId).Select(x => x.GoodsId).ToList();
                        List<HistoryBuyGood> listHbg = historyBuyGoodService.GetByInvoiceId(invoice.IId);
                        foreach (var hbgs in listHbg)
                        {
                            if(hbgs.Quantity > goodService.GetById((int)hbgs.GoodsId).Quantity)
                            {
                                MessageBox.Show("Out of stock", "", MessageBoxButton.OK);
                                return;
                            }
                        }

                        if (!goodIds.Contains(selectedGood.Gid))
                        {
                            HistoryBuyGood hbg = new HistoryBuyGood()
                            {
                                Date = invoice.InvoiceDate,
                                InvoiceId = invoice.IId,
                                GoodsId = selectedGood.Gid,
                                Quantity = 1,
                                Amount = selectedGood.UnitPrice
                            };
                            MessageBox.Show("Thêm thành công");
                            historyBuyGoodService.Add(hbg);
                        }
                        else
                        {
                            HistoryBuyGood hbg = historyBuyGoodService.GetByInvoiceId(invoice.IId).FirstOrDefault(x => x.GoodsId == selectedGood.Gid);
                            hbg.Quantity = hbg.Quantity + 1;
                            hbg.Amount = hbg.Amount * hbg.Quantity;
                            historyBuyGoodService.Update(hbg);
                            MessageBox.Show("Thêm thành công");
                        }
                    }
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

        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateGoodsScreen u = new UpdateGoodsScreen();
        //    u.tblHeader.Text = "Add new Good";
        //    u.spnGoodId.Visibility = Visibility.Hidden;
        //    u.cbxType.SelectedIndex = 0;
        //    if (u.ShowDialog() == false)
        //    {
        //        u._selectedBook = null;
        //        FillItcGood();
        //    }
        //}

        //private void btnGoodType_Click(object sender, RoutedEventArgs e)
        //{
        //    GoodsTypeScreen g = new();
        //    if (g.ShowDialog() == false)
        //    {
        //        cbxCategory.ItemsSource = goodTypeService.GetGoodTypes();
        //        FillItcGood();
        //    }
        //}

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
