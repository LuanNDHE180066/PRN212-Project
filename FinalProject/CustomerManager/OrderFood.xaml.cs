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

namespace FinalProject.CustomerManager
{
    /// <summary>
    /// Interaction logic for OrderFood.xaml
    /// </summary>
    public partial class OrderFood : Window
    {
        public GoodService GoodService= new GoodService();
        private CustomerService customerService= new CustomerService();
        private GoodsService goodService = new GoodsService(); 
        private InvoiceService InvoiceService = new InvoiceService();
        HistoryBuyGoodService historyBuyGoodService = new HistoryBuyGoodService();
        private HistoryUsedDeviceService historyUsedDeviceService = new HistoryUsedDeviceService();
        public OrderFood()
        {
            InitializeComponent();
            showFood();
            int cId = int.Parse(Application.Current.Properties["customerId"] as string);
            Customer customer = customerService.GetCustomerByID(cId);
            lbName.Content = customer.CName;
        }
        public void showFood()
        {
            List<Good> list = GoodService.GetAllActiveGood();
            foreach (Good good in list)
            {
                Border border = new Border();
                border.Height = 200;
                border.Width = 200;
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(10);
                border.CornerRadius = new CornerRadius(5);

                StackPanel stackPanel = new StackPanel();

                // Anh
                Image image = new Image();
                image.Height = 100;
                image.Source = new BitmapImage(new System.Uri(good.Img, System.UriKind.Absolute));
                image.Margin = new Thickness(5);

                // Ten
                TextBlock nameTextBlock = new TextBlock();
                nameTextBlock.Text = "Sản phẩm :" + good.GName;
                nameTextBlock.FontSize = 16;
                nameTextBlock.FontWeight = FontWeights.Bold;
                nameTextBlock.TextAlignment = TextAlignment.Center;
                nameTextBlock.Margin = new Thickness(5);

                // Gia
                TextBlock priceTextBlock = new TextBlock();
                priceTextBlock.Text = "Giá: " + good.UnitPrice +"| SL: "+good.Quantity;
                priceTextBlock.TextAlignment = TextAlignment.Center;
                priceTextBlock.Margin = new Thickness(5);
             

                // Button
                Button button = new Button();
                button.Content = "Mua ngay";
                button.Width = 100;
                button.Margin = new Thickness(5);
                button.Tag = good.Gid;
                button.HorizontalAlignment = HorizontalAlignment.Center;
                if(good.Quantity == 0)
                {
                    button.IsEnabled = false;
                }
                button.Click += Button_Click;

                // Thêm các thành phần vào StackPanel
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(nameTextBlock);
                stackPanel.Children.Add(priceTextBlock);
                stackPanel.Children.Add(button);

               
                border.Child = stackPanel;

                panelFood.Children.Add(border);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int gId = int.Parse(button.Tag.ToString());
            int invoiceId = int.Parse(Application.Current.Properties["invoiceId"] as string);
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            int quantity = 1;
            Good good = goodService.GetById(gId);
            good.Quantity = good.Quantity-1;
            GoodService gsv = new GoodService();
            gsv.UpdateGood(good);
            decimal amount = good.UnitPrice.Value * quantity;
            HistoryBuyGood buyGood = new HistoryBuyGood() {InvoiceId= invoiceId, GoodsId= gId,Date =date,Quantity= quantity,Amount
            =amount};

            historyBuyGoodService.Add(buyGood);
            MessageBox.Show("Order thành công"+button.Tag.ToString());
            panelFood.Children.Clear();
            showFood();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Chắc chắn offline?","Warning",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                int invoiceId = int.Parse(Application.Current.Properties["invoiceId"] as string);
                Invoice invoice = InvoiceService.GetById(invoiceId);
                TimeOnly end = TimeOnly.FromDateTime(DateTime.Now);
                HistoryUsedDevice historyUsedDevice = historyUsedDeviceService.GetByInvoiceId(invoiceId);
                historyUsedDevice.End = end;

                TimeSpan timeSpan = end - historyUsedDevice.Start.Value;
                decimal duration = Math.Round(((decimal)timeSpan.TotalHours), 2);
                decimal amount = duration * 10000;
                historyUsedDevice.Amount = amount;
                historyUsedDeviceService.Update(historyUsedDevice);
                InvoiceService.UpdateTotal(invoiceId);
                this.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HistoryOrder historyOrder = new HistoryOrder();
            historyOrder.Show();
            this.Close();
        }
    }
}
