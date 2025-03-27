using MahApps.Metro.Controls;
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
        private DeviceService deviceService = new DeviceService();
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
            panelFood.Children.Clear(); // Xóa dữ liệu cũ trước khi hiển thị mới

            foreach (Good good in list)
            {
                // Tạo viền container đẹp
                Border border = new Border
                {
                    Width = 220,
                    Padding = new Thickness(10),
                    Margin = new Thickness(10),
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(10),
                    Background = Brushes.White,
                    Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.Gray,
                        BlurRadius = 8,
                        ShadowDepth = 2
                    }
                };

                // StackPanel chứa nội dung
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Vertical };

                // Ảnh sản phẩm
                Image image = new Image
                {
                    Height = 120,
                    Width = 120,
                    Source = new BitmapImage(new Uri(good.Img, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.UniformToFill,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                };

                // Tên sản phẩm
                TextBlock nameTextBlock = new TextBlock
                {
                    Text = good.GName.ToUpper(),
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.DarkBlue,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(5)
                };

                // Giá và số lượng
                TextBlock priceTextBlock = new TextBlock
                {
                    Text = $"💰 {good.UnitPrice} VNĐ | 📦 SL: {good.Quantity}",
                    FontSize = 14,
                    Foreground = Brushes.DarkRed,
                    FontWeight = FontWeights.SemiBold,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(5)
                };

                // NumericUpDown (Chọn số lượng)
                NumericUpDown numericUpDown = new NumericUpDown
                {
                    Width = 100,
                    Height = 30,
                    Minimum = 1,
                    Maximum = good.Quantity > 0 ? (double)good.Quantity : 1, // Không cho chọn số lớn hơn tồn kho
                    Value = 1,
                    Foreground = Brushes.Black,
                    Margin = new Thickness(5),
                };

                // Button "Mua ngay"
                Button button = new Button
                {
                    Content = "🛒 Mua ngay",
                    Width = 120,
                    Height = 35,
                    Background = good.Quantity > 0 ? Brushes.DarkOrange : Brushes.Gray,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    Cursor = Cursors.Hand,
                    IsEnabled = good.Quantity > 0,
                    Margin = new Thickness(5),
                    Tag = good.Gid,
                    DataContext = numericUpDown
                };
                button.Click += Button_Click;

                // Thêm vào StackPanel
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(nameTextBlock);
                stackPanel.Children.Add(priceTextBlock);
                stackPanel.Children.Add(numericUpDown);
                stackPanel.Children.Add(button);

                // Gán vào Border
                border.Child = stackPanel;

                // Thêm vào giao diện chính
                panelFood.Children.Add(border);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int gId = int.Parse(button.Tag.ToString());
            int invoiceId = int.Parse(Application.Current.Properties["invoiceId"] as string);
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            NumericUpDown numUpDown = button.DataContext as NumericUpDown;
            int quantity=1;
            if (numUpDown != null)
            {
                quantity = (int)numUpDown.Value;  // Lấy giá trị số lượng
            }
            Good good = goodService.GetById(gId);
            good.Quantity = good.Quantity-quantity;
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
        }
    }
}
