﻿using Azure.Core;
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
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;
        public wCustomer()
        {
            InitializeComponent();
            this._business = new CustomerBusiness();
            this.LoadGrdCustomer();
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(CustomerID.Text);

                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        Id = CustomerID.Text,
                        CustomerName = Name.Text,
                        CustomerPhone = Phone.Text,
                        CustomerAddress = Address.Text,
                    };

                    var result = await _business.Save(customer);
                    MessageBox.Show(result.Message, "Save");
                  
                }
                else
                {
                    var customer = item.Data as Customer;
                    customer.Id = CustomerID.Text;
                    customer.CustomerName = Name.Text;
                    customer.CustomerPhone = Phone.Text;
                    customer.CustomerAddress = Address.Text;
                    var result = await _business.Update(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                CustomerID.Text = string.Empty;
                Name.Text = string.Empty;
                Phone.Text = string.Empty;
                Address.Text = string.Empty;
                this.LoadGrdCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void grdCustomer_ButtonSelect_Click(object obj, RoutedEventArgs e)
        {
            Button btn = (Button)obj;

            string customerID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(customerID))
            {
                var customerResult = await _business.GetById(customerID);

                if (customerResult.Status > 0 && customerResult.Data != null)
                {
                    var item = customerResult.Data as Customer;
                    CustomerID.Text = item.Id;
                    Name.Text = item.CustomerName;
                    Phone.Text = item.CustomerPhone;
                    Address.Text = item.CustomerAddress;
                }
            }
        }
        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var customerResult = await _business.GetById(item.Id);

                        if (customerResult.Status > 0 && customerResult.Data != null)
                        {
                            item = customerResult.Data as Customer;
                            CustomerID.Text = item.Id;
                            Name.Text = item.CustomerName;
                            Phone.Text = item.CustomerPhone;
                            Address.Text = item.CustomerAddress;
                        }
                    }
                }
            }
        }
        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string customerID = btn.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(customerID))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(customerID);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCustomer();
                }
            }
        }

        private async void LoadGrdCustomer()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }
    }
}
