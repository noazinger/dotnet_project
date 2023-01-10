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
        private BO.Product prod = new BO.Product();
        private BO.Order ord = new BO.Order();
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

        }
        private void itemsClicked(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int pId = (ProductsListview.SelectedItem as BO.ProductForList).ID;
                prod = bl.Product.ReadSingleProductForDirector(pId);
                Window window = new ProductWindow(bl);
                //addProductBtn.Visibility = Visibility.Hidden;
                window.Show();
                InitializeComponent();
                ProductsListview.ItemsSource = bl.Product.ReadListProducts();
            }
            catch(Exception exc)
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
                Window window = new OrderWindow(ord);
                //addProductBtn.Visibility = Visibility.Hidden;
                window.Show();
                InitializeComponent();
                OrdersListview.ItemsSource = bl.Order.ReadOrders();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
