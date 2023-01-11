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

using BlApi;
using PL.Orders;
using PL.Products;

namespace PL.admin
{
    /// <summary>
    /// Interaction logic for adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Product p = new BO.Product();
        private BO.Order ord = new BO.Order();
        private BO.ProductItem pi = new();
        public adminWindow(IBl b)
        {
            try
            {
                InitializeComponent();
                bl = b;
                ProductsListview.ItemsSource = bl.Product.ReadListProducts();
                OrdersListview.ItemsSource = bl.Order.ReadOrders();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void addProBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(pi, "add").Show();
            Close();
        }
        private void itemsClicked(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.ProductForList pr = (BO.ProductForList)((ListView)sender).SelectedItem;
                p = bl.Product.ReadSingleProductForDirector(pr.ID);
                pi.ID = p.ID;
                pi.Price = p.Price;
                pi.catagory = p.catagory;
                pi.Name=p.Name;
                new ProductWindow(pi, "update").Show();
                Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void ordersClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                int oId = (OrdersListview.SelectedItem as BO.OrderForList).ID;
                ord = bl?.Order.ReadOrderInformation(oId);
                new OrderWindow(ord, false).Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
