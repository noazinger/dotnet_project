using BlApi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Xml.Linq;
using PL.Products;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        //private BlApi.IBl Bl { get; set; }
      IBl? bl = Factory.Get();
        BO.Cart myCart = new BO.Cart();
        public OrderForList(IBl b, BO.Cart c)
        {
            InitializeComponent();
            bl = b;
            try
            {
                myCart = c;
                OrderListView.ItemsSource = bl.Product.ReadCatalog();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem pi = (BO.ProductItem)((ListView)sender).SelectedItem;
/*            OrderWindow OW = new(pi);
*//*            OW.Show();*/
            this.Hide();
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)((ListView)sender).SelectedItem;
            new ProductWindow(p, myCart).Show();
            Close();
        }
        public void showCart_btn(object sender, RoutedEventArgs e)
        {
            new PL.Carts.CartWindow().Show() ;
            this.Hide();

        }

    }
}
