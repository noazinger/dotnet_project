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
using BlApi;
using BO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = Factory.Get();

        public OrderWindow(Order order)
        {
            InitializeComponent();
            
/*            txtOrderCustomerName.Text = order.CustomerName;
            txtOrderCustomerEmail.Text = order.CustomerEmail;
            txtOrderCustomerAddress.Text = order.CustomerAddress;*/
            //this.DataContext = order;
            txtOrderOrderDate.Text = order.OrderDate.ToString();
            //txtOrderOrderDate.Text = order.OrderDate.ToString();
            txtOrderStatus.Text = order.Status.ToString();
            txtOrderShipDate.Text = order.ShipDate.ToString();
            txtOrderDeliveryDate.Text = order.DeliveryDate.ToString();
            BO.Order order1 = bl.Order.ReadOrderInformation(order.ID);
            this.DataContext=order1;
            try
            {
                OrderItemsListView.ItemsSource = order.Items;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }

        }
    }
}
