using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Repositories.Models;
using Services;
using Path = System.IO.Path;

namespace FinalProject.Admin
{
    /// <summary>
    /// Interaction logic for UpdateGoodsScreen.xaml
    /// </summary>
    public partial class UpdateGoodsScreen : Window
    {
        public Good _selectedBook = null;
        private GoodTypeService goodTypeService = new GoodTypeService();
        private GoodService goodService = new();

        public UpdateGoodsScreen(Good g)
        {
            InitializeComponent();
            _selectedBook = g;
            LoadDb();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoadDb()
        {
            if (_selectedBook != null)
            {
                txbGoodId.IsEnabled = false;
                txbGoodId.Text = _selectedBook.Gid.ToString();
                txbGoodName.Text = _selectedBook.GName;
                cbxType.SelectedValue = _selectedBook.Typeid;
                cbxType.ItemsSource = goodTypeService.GetGoodTypes();
                txbPrice.Text = _selectedBook.UnitPrice.ToString();
                txbStock.Text = _selectedBook.Quantity.ToString();
                txbUnit.Text = _selectedBook.Unit.ToString();
                txbImg.Text = _selectedBook.Img.ToString();
                foreach (ComboBoxItem item in cbxStatus.Items)
                {
                    if (_selectedBook.Status == int.Parse(item.Tag.ToString()))
                    {
                        cbxStatus.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                tblHeader.Text = "Add new Good";

                txbGoodId.Visibility = Visibility.Hidden;
                List<GoodType> list = new List<GoodType>();
                list.Add(new GoodType() { Gtid = 0, GtName = "Select Good Type" });
                foreach (GoodType item in goodTypeService.GetGoodTypes())
                {
                    list.Add(item);
                }
                cbxType.ItemsSource = list;
                cbxStatus.SelectedIndex = 0;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Mở OpenFileDialog cho phép chọn file ảnh (để lấy tên file)
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy tên file (ví dụ: "1215629.png")
                string fileName = Path.GetFileName(openFileDialog.FileName);

                // Tạo đường dẫn dạng Pack URI tương đối (dạng bạn muốn lưu, ví dụ: "/Images/1215629.png")
                string relativePath = $"/Images/{fileName}";
                txbImg.Text = relativePath;

                // Lấy tên assembly (thường là tên project)
                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                // Tạo Pack URI đầy đủ dùng cú pháp "component"
                string fullPackUri = $"pack://application:,,,/{assemblyName};component/Images/{fileName}";

                try
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(fullPackUri, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    bmp.Freeze();
                    imgPreview.Source = bmp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txbGoodName.Text.Trim().IsNullOrEmpty() || txbGoodName.Text.Trim().IsNullOrEmpty()
                || txbPrice.Text.Trim().IsNullOrEmpty() || txbStock.Text.Trim().IsNullOrEmpty() || txbImg.Text.Trim().IsNullOrEmpty())
            {
                MessageBox.Show("Please enter all fill and correct format", "Information", MessageBoxButton.OK);
                return;
            }

            if (cbxType.SelectedValue.ToString().IsNullOrEmpty())
            {
                MessageBox.Show("Please select good type", "Information", MessageBoxButton.OK);
                return;
            }
            Good good = new Good();
            try
            {
                string GName = txbGoodName.Text;
                int Typeid = int.Parse(cbxType.SelectedValue.ToString());
                decimal UnitPrice = decimal.Parse(txbPrice.Text);
                int Quantity = int.Parse(txbStock.Text);
                string Unit = txbUnit.Text;
                string Img = txbImg.Text;
                ComboBoxItem selected = (ComboBoxItem)cbxStatus.SelectedItem;
                int status = int.Parse(selected.Tag.ToString());
                MessageBox.Show($"{status}");
                good.Status = status;
                good.UnitPrice = UnitPrice;
                good.Quantity = Quantity;
                good.Unit = Unit;
                good.Img = Img;
                good.GName = GName;
                good.Typeid = Typeid;
            }
            catch
            {
                MessageBox.Show("Wrong format number", "Format exception", MessageBoxButton.OK);
            }



            if (_selectedBook != null)
            {
                int gid = int.Parse(txbGoodId.Text);
                good.Gid = gid;
                if (goodService.UpdateGood(good))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK);
                    return;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButton.OK);
                    return;
                }
            }
            else
            {
                try
                {
                    if (goodService.AddGood(good))
                    {
                        MessageBox.Show("Add new good successfull", "Information", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add", "Successfull", MessageBoxButton.OK);
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter all fill and correct format", "Information", MessageBoxButton.OK);
                }

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
