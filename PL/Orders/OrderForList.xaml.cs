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
        //private BlApi.IBl Bl { get; set; }
        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderForList(IBl b)
        {
            InitializeComponent();
            bl = b;
            try
            {
                OrderListView.ItemsSource = bl.Order.ReadOrders();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem pi = (BO.ProductItem)((ListView)sender).SelectedItem;
            OrderWindow OW = new(pi);
            OW.Show();
            this.Hide();
        }
    }
}
