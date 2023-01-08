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
namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderForList(IBl b)
        {
            InitializeComponent();
            bl = b;
            try
            {
                OrdersListView.ItemsSource = b.Product.ReadCatalog();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object categor = CategorySelector.SelectedItem;
                IEnumerable<BO.ProductItem> list = bl.Product.ReadProductByCategoty((BO.catagory)categor);
                OrdersListView.ItemsSource = list;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)((ListView)sender).SelectedItem;
            new OrderWindow(p).Show();
            this.Close();
        }
    }
}
