using Microsoft.IdentityModel.Tokens;
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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for ExpenditureDetail.xaml
    /// </summary>
    public partial class ExpenditureDetail : Window
    {
        private Expenditure expenditure;
        private GoodsService goodsService= new GoodsService();
        private ExpenditureService expenditureService= new ExpenditureService();
        public ExpenditureDetail(Expenditure expenditure)
        {
            InitializeComponent();
            this.expenditure = expenditure;
            if(expenditure != null )
            {
                lbNamepage.Content = "Update Expenditure";
                FillComboBoxGoods();
                FillExpenditureDetail();
            }
        }
        public ExpenditureDetail()
        {
            InitializeComponent();
            FillComboBoxGoods();
            int newID = expenditureService.GetAll().Last().ExId+1;
            txtId.Text = newID.ToString();
        }
        public void FillComboBoxGoods()
        {
            List<Good> list = goodsService.GetAll();
            cbGood.ItemsSource = list;
        }
        private void FillExpenditureDetail()
        {
            txtId.Text = expenditure.ExId.ToString();
            txtQuantity.Text = expenditure.Quantity.ToString();
            txtSupplier.Text = expenditure.Supplier;
            txtTotal.Text = expenditure.Total.ToString();
            dpkDate.SelectedDate = expenditure.ExDate.Value.ToDateTime(TimeOnly.MinValue);
            cbGood.SelectedValue = expenditure.Goods.Gid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (expenditure != null)
            {
                int id = int.Parse(txtId.Text);
                int quantity = int.Parse(txtQuantity.Text);
                string supplier = txtSupplier.Text;

                bool isNotValid = txtId.Text.IsNullOrEmpty() || txtQuantity.Text.IsNullOrEmpty()
                    || txtSupplier.Text.IsNullOrEmpty() || txtTotal.Text.IsNullOrEmpty()
                    || dpkDate.SelectedDate == null || cbGood.SelectedValue == null;
                if (isNotValid)
                {
                    MessageBox.Show("Input follow format please");
                    return;
                }
                decimal total = decimal.Parse(txtTotal.Text);
                DateTime date = dpkDate.SelectedDate.Value;
                int goodsId = int.Parse(cbGood.SelectedValue.ToString());
                Expenditure exp = expenditureService.GetById(id);
                exp.Quantity = quantity;
                exp.Supplier = supplier;
                exp.GoodsId = goodsId;
                exp.Total = total;
                exp.ExDate = DateOnly.FromDateTime(date);
                expenditureService.UpdateExpenditure(exp);
            }
            else
            {
                int id = int.Parse(txtId.Text);
                int quantity = int.Parse(txtQuantity.Text);
                string supplier = txtSupplier.Text;

                bool isNotValid = txtId.Text.IsNullOrEmpty() || txtQuantity.Text.IsNullOrEmpty()
                    || txtSupplier.Text.IsNullOrEmpty() || txtTotal.Text.IsNullOrEmpty()
                    || dpkDate.SelectedDate == null || cbGood.SelectedValue == null;
                if (isNotValid)
                {
                    MessageBox.Show("Input follow format please");
                    return;
                }
                decimal total = decimal.Parse(txtTotal.Text);
                DateTime date = dpkDate.SelectedDate.Value;
                int goodsId = int.Parse(cbGood.SelectedValue.ToString());
                Expenditure exp = new Expenditure();
                exp.Quantity = quantity;
                exp.Supplier = supplier;
                exp.GoodsId = goodsId;
                exp.Total = total;
                exp.StaffId = int.Parse(Application.Current.Properties["StaffId"] as string);
                exp.ExDate = DateOnly.FromDateTime(date);
                expenditureService.AddExpenditure(exp);
            }
            this.Close();
        }
    }
}
