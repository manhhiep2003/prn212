using Azure.Core;
using Jewelry.Business;
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
    /// Interaction logic for wRequestDetail.xaml
    /// </summary>
    public partial class wRequestDetail : Window
    {
        private readonly RequestDetailBusiness _business;
        public wRequestDetail()
        {
            InitializeComponent();
            this._business = new RequestDetailBusiness();
            this.LoadGrdRequestDetail();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(RequestDetailID.Text);

                if (item.Data == null)
                {
                    var requestDetail = new RequestDetail()
                    {
                        Id = RequestDetailID.Text,
                        Name = Name.Text,
                        Jewelry = Jewelry.Text,
                        Price = Price.Text,
                        ReqId = RequestID.Text
                    };

                    var result = await _business.Save(requestDetail);
                    MessageBox.Show(result.Message, "Save");   
                }
                else
                {
                    var requestDetail = item.Data as RequestDetail;
                    requestDetail.Name = Name.Text;
                    requestDetail.Jewelry = Jewelry.Text;
                    requestDetail.Price = Price.Text;
                    requestDetail.ReqId = RequestID.Text;
                    var result = await _business.Update(requestDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                RequestDetailID.Text = string.Empty;
                Name.Text = string.Empty;
                Jewelry.Text = string.Empty;
                Price.Text = string.Empty;
                RequestID.Text = string.Empty;
                this.LoadGrdRequestDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdRequestDetail_ButtonSelect_Click(object obj, RoutedEventArgs e)
        {
            Button btn = (Button)obj;

            string requestDetailID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(requestDetailID))
            {
                var requestResult = await _business.GetById(requestDetailID);

                if (requestResult.Status > 0 && requestResult.Data != null)
                {
                    var item = requestResult.Data as RequestDetail;
                    RequestDetailID.Text = item.Id;
                    Name.Text = item.Name;
                    Jewelry.Text = item.Jewelry;
                    Price.Text = item.Price;
                    RequestID.Text = item.ReqId;
                }
            }
        }
        private async void grdRequestDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as RequestDetail;
                    if (item != null)
                    {
                        var requestDetailResult = await _business.GetById(item.Id);

                        if (requestDetailResult.Status > 0 && requestDetailResult.Data != null)
                        {
                            item = requestDetailResult.Data as RequestDetail;
                            RequestDetailID.Text = item.Id;
                            Name.Text = item.Name;
                            Jewelry.Text = item.Jewelry;
                            Price.Text = item.Price;
                            RequestID.Text = item.ReqId;
                        }
                    }
                }
            }
        }
        private async void grdRequestDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string requestDetailID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(requestDetailID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(requestDetailID);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdRequestDetail();
                }
            }
        }

        private async void LoadGrdRequestDetail()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdRequestDetail.ItemsSource = result.Data as List<RequestDetail>;
            }
            else
            {
                grdRequestDetail.ItemsSource = new List<RequestDetail>();
            }
        }
    }
}
