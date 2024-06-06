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
using Jewelry.Business;
using Jewelry.Data;
using Jewelry.Data.Models;

namespace Jewelry.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wRequest.xaml
    /// </summary>
    public partial class wRequest : Window
    {

        private readonly RequestBusiness _business;
        public wRequest()
        {
            InitializeComponent();
            this._business = new RequestBusiness();
            this.LoadGrdRequest();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(RequestId.Text);

                if (item.Data == null)
                {
                    var request = new Request()
                    {
                        Id = RequestId.Text,
                        CusId = CusId.Text,
                        TotalPrice = TotalPrice.Text
                    };

                    var result = await _business.Save(request);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var request = item.Data as Request;
                    request.CusId = CusId.Text;
                    request.TotalPrice = TotalPrice.Text;
                    var result = await _business.Update(request);
                    MessageBox.Show(result.Message, "Save");
                }
                RequestId.Text = string.Empty;
                CusId.Text = string.Empty;
                TotalPrice.Text = string.Empty;
                this.LoadGrdRequest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void grdRequest_ButtonSelect_Click(object obj, RoutedEventArgs e)
        {
            Button btn = (Button)obj;

            string requestId = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(requestId))
            {
                var requestResult = await _business.GetById(requestId);

                if (requestResult.Status > 0 && requestResult.Data != null)
                {
                    var item = requestResult.Data as Request;
                    RequestId.Text = item.Id;
                    CusId.Text = item.CusId;
                    TotalPrice.Text = item.TotalPrice;
                }
            }
        }

        private async void grdRequest_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Request;
                    if (item != null)
                    {
                        var requestResult = await _business.GetById(item.Id);

                        if (requestResult.Status > 0 && requestResult.Data != null)
                        {
                            item = requestResult.Data as Request;
                            RequestId.Text = item.Id;
                            CusId.Text = item.CusId;
                            TotalPrice.Text = item.TotalPrice;
                        }
                    }
                }
            }
        }

        private async void grdRequest_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string requestID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(requestID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", 
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(requestID);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdRequest();
                }
            }
        }

        private async void LoadGrdRequest()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdRequest.ItemsSource = result.Data as List<Request>;
            }
            else
            {
                grdRequest.ItemsSource = new List<Request>();
            }
        }
    }
}
