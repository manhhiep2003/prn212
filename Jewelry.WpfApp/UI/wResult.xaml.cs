using Azure.Core;
using Jewelry.Business;
using Jewelry.Data;
using Jewelry.Data.Models;
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

namespace Jewelry.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wResult.xaml
    /// </summary>
    public partial class wResult : Window
    {
        private readonly ResultBusiness _business;
        public wResult()
        {
            InitializeComponent();
            this._business = new ResultBusiness();
            this.LoadGrdResult();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(ResultID.Text);

                if (item.Data == null)
                {
                    var result = new Result()
                    {
                        Id = ResultID.Text,
                        CusId = CustomerID.Text,
                        ProductName = ProductName.Text,
                        ProductImage = ProductImage.Text,
                        TotalPrice = TotalPrice.Text,
                        Status = Status.Text,
                        TransferType = TransferType.Text,
                        ReqId = RequestID.Text
                    };

                    var jewelryResult = await _business.Save(result);
                    MessageBox.Show(jewelryResult.Message, "Save");
                  
                }
                else
                {
                    var result = item.Data as Result;
                    result.CusId = CustomerID.Text;
                    result.ProductName = ProductName.Text;
                    result.ProductImage = ProductImage.Text;
                    result.TotalPrice = TotalPrice.Text;
                    result.Status = Status.Text;
                    result.TransferType = TransferType.Text;
                    result.ReqId = RequestID.Text;
                    var jewelryResult = await _business.Update(result);
                    MessageBox.Show(jewelryResult.Message, "Save");
                }
                ResultID.Text = string.Empty;
                CustomerID.Text = string.Empty;
                ProductName.Text = string.Empty;
                ProductImage.Text = string.Empty;
                TotalPrice.Text = string.Empty;
                Status.Text = string.Empty;
                TransferType.Text = string.Empty;
                RequestID.Text = string.Empty;
                this.LoadGrdResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdResult_ButtonSelect_Click(object obj, RoutedEventArgs e)
        {
            Button btn = (Button)obj;

            string resultID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(resultID))
            {
                var result = await _business.GetById(resultID);

                if (result.Status > 0 && result.Data != null)
                {
                    var item = result.Data as Result;
                    ResultID.Text = item.Id;
                    CustomerID.Text = item.CusId;
                    ProductName.Text = item.ProductName;
                    ProductImage.Text = item.ProductImage;
                    TotalPrice.Text = item.TotalPrice;
                    Status.Text = item.Status;
                    TransferType.Text = item.TransferType;
                    RequestID.Text = item.ReqId;
                }
            }
        }

        private async void grdResult_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Result;
                    if (item != null)
                    {
                        var jewelryResult = await _business.GetById(item.Id);

                        if (jewelryResult.Status > 0 && jewelryResult.Data != null)
                        {
                            item = jewelryResult.Data as Result;
                            ResultID.Text = item.Id;
                            CustomerID.Text = item.CusId;
                            ProductName.Text = item.ProductName;
                            ProductImage.Text = item.ProductImage;
                            TotalPrice.Text = item.TotalPrice;
                            Status.Text = item.Status;
                            TransferType.Text = item.TransferType;
                            RequestID.Text = item.ReqId;
                        }
                    }
                }
            }
        }

        private async void grdResult_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string resultID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(resultID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(resultID);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdResult();
                }
            }
        }

        private async void LoadGrdResult()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdResult.ItemsSource = result.Data as List<Result>;
            }
            else
            {
                grdResult.ItemsSource = new List<Result>();
            }
        }
    }
}
