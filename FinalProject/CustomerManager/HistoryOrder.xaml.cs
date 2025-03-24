using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Repositories.Models;
using Services;

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for HistoryOrder.xaml
    /// </summary>
    public partial class HistoryOrder : Window
    {
        private CustomerService customerService = new CustomerService();
        private HistoryBuyGoodService HistoryBuyGoodService = new HistoryBuyGoodService();
        public HistoryOrder()
        {
            InitializeComponent();
            showHistoryOrder();
            int cid =  int.Parse(Application.Current.Properties["customerId"] as string);
            Customer customer = customerService.GetCustomerByID(cid);
            lbName.Content = customer.CName;
        }
        public void showHistoryOrder()
        {
            int cid = int.Parse(Application.Current.Properties["customerId"] as string);
            List<HistoryBuyGood> history = HistoryBuyGoodService.GetByCustomerId(cid);

            
            panelHistory.Children.Clear();

            
            var groupedHistory = history.GroupBy(h => h.Invoice.IId);

            foreach (var invoiceGroup in groupedHistory)
            {
                
                Border invoiceBorder = new Border
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Black,
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 10, 0, 10), 
                    Width = 450, 
                    Background = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center 
                };

                
                StackPanel invoicePanel = new StackPanel();

                
                TextBlock invoiceTextBlock = new TextBlock
                {
                    Text = "Invoice ID: " + invoiceGroup.Key,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 5),
                    TextAlignment = TextAlignment.Left 
                };

                
                DataGrid dataGrid = new DataGrid
                {
                    AutoGenerateColumns = false,
                    Height = 100,
                    Margin = new Thickness(0, 5, 0, 5),
                    Columns =
            {
                new DataGridTextColumn { Header = "Sản phẩm", Binding = new Binding("GName"), Width = 150 },
                new DataGridTextColumn { Header = "Số lượng", Binding = new Binding("Quantity"), Width = 80 },
                new DataGridTextColumn { Header = "Giá", Binding = new Binding("UnitPrice"), Width = 100 },
                new DataGridTextColumn { Header = "Tổng tiền", Binding = new Binding("TotalPrice"), Width = 100 }
            }
                };

                
                dataGrid.ItemsSource = invoiceGroup.Select(h => new
                {
                    GName = h.Goods.GName,
                    Quantity = h.Quantity,
                    UnitPrice = h.Goods.UnitPrice,
                    TotalPrice = h.Quantity * h.Goods.UnitPrice
                }).ToList();

               
                invoicePanel.Children.Add(invoiceTextBlock);
                invoicePanel.Children.Add(dataGrid);

                
                invoiceBorder.Child = invoicePanel;

                
                panelHistory.Children.Add(invoiceBorder);
            }
        }
        
    }
}
