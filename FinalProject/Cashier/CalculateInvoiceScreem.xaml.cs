﻿using System;
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
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Tokens;
using Repositories.Models;
using Services;

namespace FinalProject.Cashier
{
    /// <summary>
    /// Interaction logic for CalculateInvoiceScreem.xaml
    /// </summary>
    public partial class CalculateInvoiceScreem : Window
    {
        private DispatcherTimer _mouseMoveTimer;
        private bool _canUpdate = true; // Biến kiểm soát thời điểm cập nhật
        public Device device = null;
        public CustomerService customerService = new();
        public StaffService staffService = new();
        public InvoiceService invoiceService = new();
        public HistoryBuyGoodService historyBuyGoodService = new();
        public HistoryUsedDeviceService historyUsedDeviceService = new();
        public DeviceService deviceService = new();
        public Invoice _invoice = null;
        private int _invoiceId = -1;
        private GoodService goodService = new();
        public CalculateInvoiceScreem()
        {
            InitializeComponent();
            _mouseMoveTimer = new DispatcherTimer();
            _mouseMoveTimer.Interval = TimeSpan.FromMilliseconds(10000); // Giới hạn cập nhật mỗi 1 giây
            _mouseMoveTimer.Tick += (s, e) => _canUpdate = true; // Sau mỗi giây, cho phép cập nhật lại
            _mouseMoveTimer.Start();
        }

        private void timeInput_LostFocus(object sender, RoutedEventArgs e)
        {
            string timeText = timeInput.Text;

            if (IsValidTime(timeText))
            {
                errorText.Text = "";
                errorText.Visibility = Visibility.Collapsed;
            }
            else
            {
                errorText.Text = "Thời gian không hợp lệ!";
                errorText.Visibility = Visibility.Visible;
            }
        }

        private bool IsValidTime(string timeText)
        {
            if (string.IsNullOrWhiteSpace(timeText) || !timeText.Contains(":"))
                return false;

            string[] parts = timeText.Split(':');
            if (parts.Length != 2) return false;

            if (int.TryParse(parts[0], out int hour) && int.TryParse(parts[1], out int minute))
            {
                return hour >= 0 && hour < 24 && minute >= 0 && minute < 60;
            }

            return false;
        }

        private void timeOuput_LostFocus(object sender, RoutedEventArgs e)
        {
            string timeText = timeOuput.Text;

            if (IsValidTime(timeText))
            {
                errorText2.Text = "";
                errorText2.Visibility = Visibility.Collapsed;
            }
            else
            {
                errorText2.Text = "Thời gian không hợp lệ!";
                errorText2.Visibility = Visibility.Visible;
            }
        }

        private void SetFrom_Click(object sender, RoutedEventArgs e)
        {
            timeInput.Value = DateTime.Now.ToString("HH:mm");
        }

        private void SetTo_Click(object sender, RoutedEventArgs e)
        {
            timeOuput.Value = DateTime.Now.ToString("HH:mm");
        }

        private void btnAddGood_Click(object sender, RoutedEventArgs e)
        {
            OrderGoodScreen o = new();
            if (_invoice == null)
            {
                if (_invoiceId != -1)
                {
                    o.invoice = invoiceService.GetAll().FirstOrDefault(x => x.IId == _invoiceId);
                    if (o.ShowDialog() == false)
                    {
                        dgvGood.ItemsSource = historyBuyGoodService.GetByInvoiceId(_invoiceId);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Id", "", MessageBoxButton.OK);
                }
            }
            else
            {
                o.invoice = invoiceService.GetAll().FirstOrDefault(x => x.IId == _invoice.IId);
                if (o.ShowDialog() == false)
                {
                    dgvGood.ItemsSource = historyBuyGoodService.GetByInvoiceId(_invoice.IId);
                }
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {


            if (!timeInput.Text.Contains("_"))
            {

                HistoryUsedDevice hub = historyUsedDeviceService.GetDeviceRunning(device.Did);
                try
                {
                    int staffId = -1;
                    int customerId = -1;
                    if (_invoice == null)
                    {
                        if (string.IsNullOrEmpty(dpkDate.Text))
                        {
                            MessageBox.Show("Hãy nhập ngày", "", MessageBoxButton.OK);
                            return;
                        }

                        if (string.IsNullOrEmpty(txbStaff.Text) || string.IsNullOrEmpty(txbCustomer.Text))
                        {
                            MessageBox.Show("Hãy nhập Customer ID và Staff ID", "", MessageBoxButton.OK);
                            return;
                        }

                        staffId = int.Parse(txbStaff.Text);
                        customerId = int.Parse(txbCustomer.Text);


                        if (staffService.GetById(staffId) == null)
                        {
                            MessageBox.Show("Staff không tồn tại", "", MessageBoxButton.OK);
                            return;
                        }


                        if (customerService.GetById(customerId) == null)
                        {
                            MessageBox.Show("Customer không tồn tại", "", MessageBoxButton.OK);
                            return;
                        }


                        if (timeInput.Text.Contains("_"))
                        {
                            MessageBox.Show("Hãy nhập vào thời gian bắt đầu", "", MessageBoxButton.OK);
                            return;
                        }


                        if (!IsValidTime(timeInput.Text))
                        {
                            MessageBox.Show($"{timeInput.Text}", "", MessageBoxButton.OK);
                            MessageBox.Show("Thời gian không hợp lệ", "", MessageBoxButton.OK);
                            return;
                        }
                    }
                    else
                    {
                        staffId = int.Parse(txbStaff.Text);
                        customerId = int.Parse(txbCustomer.Text);
                    }

                    DateOnly date = DateOnly.Parse(dpkDate.Text);


                    TimeOnly inputTime = TimeOnly.Parse(timeInput.Text);
                    if (!timeInput.Text.Contains("_") && timeOuput.Text.Contains("_") && device != null && _invoice == null && _invoiceId == -1)
                    {
                        MessageBox.Show("Create invoice !");
                        btnSetTo.IsEnabled = true;
                        Device d = deviceService.GetAllDevice().FirstOrDefault(dev => dev.Did == device.Did);
                        d.Status = 2;
                        deviceService.UpdateDevice(d);

                        decimal total = new decimal(0);
                        Invoice invoice = new Invoice() { CustomerId = customerId, StaffId = staffId, InvoiceDate = date, Total = 0, IsEnd = false };
                        invoiceService.AddNewInvoice(invoice);
                        invoice = invoiceService.GetAll().Last();
                        _invoiceId = invoice.IId;
                        tbxInvoiceId.Text = invoice.IId.ToString();
                        HistoryUsedDevice historyUsed = new HistoryUsedDevice() { Date = date, InvoiceId = invoice.IId, Start = inputTime, DeviceId = device.Did };
                        historyUsedDeviceService.Add(historyUsed);
                        btnSetFrom.IsEnabled = false;

                    }
                    else if (!timeInput.Text.Contains("_") && !timeOuput.Text.Contains("_"))
                    {
                        TimeOnly outputTime = TimeOnly.Parse(timeOuput.Text);
                        TimeSpan timeSpan = outputTime - hub.Start.Value;
                        decimal duration = Math.Round(((decimal)timeSpan.TotalHours), 2);
                        hub.End = outputTime;

                        if (duration < 1)
                        {
                            hub.Amount = device.PricePerHour;
                        }
                        else
                        {
                            hub.Amount = duration * device.PricePerHour;
                        }
                        historyUsedDeviceService.Update(hub);
                        txbTotalUseDevice.Text = hub.Amount.ToString();

                        txbTotalUseDevice.IsReadOnly = true;
                        btnSetFrom.IsEnabled = false;
                        btnSetTo.IsEnabled = false;

                        foreach (Device d in deviceService.GetAllDevice())
                        {
                            if (d.Did == device.Did)
                            {
                                d.Status = 1;
                                deviceService.UpdateDevice(d);
                            }
                        }

                        decimal totalGood = 0;
                        List<HistoryBuyGood> hbg = new();
                        if (_invoice != null)
                        {
                            hbg = historyBuyGoodService.GetByInvoiceId(_invoice.IId);
                        }
                        else
                        {
                            hbg = historyBuyGoodService.GetByInvoiceId(_invoiceId);
                        }
                        if (hbg.Count > 0)
                        {
                            foreach (HistoryBuyGood good in hbg)
                            {
                                totalGood += (decimal)good.Amount;
                            }
                        }

                        decimal totalUse = (decimal)hub.Amount;
                        txbGoodTotal.Text = totalGood.ToString();
                        tbxTotal.Text = (totalGood + hub.Amount).ToString();
                        Invoice invoice = null;
                        if (_invoice == null)
                        {
                            invoice = invoiceService.GetById(_invoiceId);
                            invoice.Total = totalUse + totalGood;
                        }
                        else
                        {
                            invoice = invoiceService.GetById(_invoice.IId);
                            invoice.Total = totalUse + totalGood;
                        }
                        Customer customer = customerService.GetById(int.Parse(txbCustomer.Text));
                        customer.Hours += (int?)duration;
                        customerService.Update(customer);
                        invoice.IsEnd = true;
                        invoiceService.Update(invoice);
                        tbxTotal.IsEnabled = false;
                        btnSave.IsEnabled = false;
                        btnAddGood.IsEnabled = false;
                    }
                    else if ((_invoice != null && timeOuput.Text.Contains("_") || (_invoiceId != null && timeOuput.Text.Contains("_"))))
                    {
                        MessageBox.Show("Please set end time to generate invoice", "", MessageBoxButton.OK);
                        return;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Hãy nhập đúng định dạng cho ID Customer và Staff (chỉ số)", "", MessageBoxButton.OK);
                    return;
                }
                catch (Exception ex) // Bắt các ngoại lệ khác nếu cần
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "", MessageBoxButton.OK);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy nhập thông tin để tạo hóa đơn", "", MessageBoxButton.OK);
                return;
            }
        }

        private void dgvGood_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            HistoryBuyGood hbg = dgvGood.SelectedItem as HistoryBuyGood;
            if (hbg != null)
            {
                GoodQuantityScreen gqs = new();
                gqs._hbg = hbg;
                gqs._good = goodService.GetById((int)hbg.GoodsId);
                gqs.txbInvoiceId.Text = _invoiceId != -1 ? _invoiceId.ToString() : _invoice.IId.ToString();
                gqs.txbGoodName.Text = gqs._good.ToString();
                gqs.txbQuantity.Text = hbg.Quantity.ToString();
                var imagePath = goodService.GetById((int)hbg.GoodsId).Img; 

                var bitmap = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

                gqs.imgGoodImage.Source = bitmap;
                if (gqs.ShowDialog() == false)
                {
                    FillHistoryBuyGood();
                }
            }
        }

        private void FillHistoryBuyGood()
        {
            dgvGood.ItemsSource = historyBuyGoodService.GetByInvoiceId(_invoiceId != -1 ? _invoiceId : _invoice.IId);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_canUpdate) return; // Nếu chưa hết thời gian chờ, bỏ qua

            _canUpdate = false; // Chặn cập nhật tiếp theo
            _mouseMoveTimer.Start(); // Bắt đầu đếm ngược cho lần cập nhật tiếp theo

            if(_invoice != null || _invoiceId != -1)
            {
                UpdateData();
            }

        }

        private void UpdateData()
        {
            HistoryUsedDevice hud = historyUsedDeviceService.GetByInvoiceId(
                _invoiceId != -1 ? _invoiceId : _invoice.IId);
            if(hud.Start != null)
            {
                this.btnSetFrom.IsEnabled = false;
                timeInput.Text = hud.Start.ToString();
            }
            if(hud.End != null)
            {
                this.btnSetTo.IsEnabled = false;
                timeOuput.Text = hud.End.ToString();

            }
            dgvGood.ItemsSource = historyBuyGoodService.GetByInvoiceId(
                _invoiceId != -1 ? _invoiceId : _invoice.IId);
        }
    }
}
