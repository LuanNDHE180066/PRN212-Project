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
using MahApps.Metro.Controls.Dialogs;
using Repositories.Models;
using Services;
using Xceed.Wpf.Toolkit;

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for GoodQuantityScreen.xaml
    /// </summary>
    public partial class GoodQuantityScreen : Window
    {

        public HistoryBuyGood _hbg;
        public Good _good;
        public GoodService _gService = new();
        private HistoryBuyGoodService _goodService = new();
        public GoodQuantityScreen()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(txbQuantity.Text);
                if (quantity > 0)
                {
                    MessageBoxResult rs = Xceed.Wpf.Toolkit.MessageBox.Show($"Do you sure to update quantity of {_good.GName}", "OK", MessageBoxButton.YesNo);
                    if (rs == MessageBoxResult.Yes)
                    {
                        int? oldQuantity = _hbg.Quantity;
                        _hbg.Quantity = quantity;
                        _hbg.Amount = _hbg.Quantity * _good.UnitPrice;
                        _good.Quantity += (oldQuantity - _hbg.Quantity);
                        _goodService.Update(_hbg);
                        _gService.UpdateGood(_good);
                        Xceed.Wpf.Toolkit.MessageBox.Show("Update successfull");
                        return;
                    }
                }
                else
                {
                    MessageBoxResult rs = Xceed.Wpf.Toolkit.MessageBox.Show($"Do you sure to delete {_good.GName}", "OK", MessageBoxButton.YesNo);
                    if (rs == MessageBoxResult.Yes)
                    {
                        _good.Quantity += _hbg.Quantity;
                        _gService.UpdateGood(_good);
                        _goodService.Delete(_hbg);
                        Xceed.Wpf.Toolkit.MessageBox.Show("Update successfull");
                        this.Close();
                        return;
                    }
                }
            }
            catch
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Please enter integer number");
            }
        }
    }
}
