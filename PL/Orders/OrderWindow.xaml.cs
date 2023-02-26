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
using DO;
using System.Collections.ObjectModel;
using DalApi;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        bool IsCustomer;
        int id;
        public ObservableCollection<BO.OrderItem> orderObs { get; set; }
        public OrderWindow(BO.Order order, bool isCustomer)
        {
            InitializeComponent();
            IsCustomer = isCustomer;
            id = order.ID;
            txtOrderCustomerAddress.IsReadOnly = !isCustomer;
            txtOrderCustomerEmail.IsReadOnly = !isCustomer;
            txtOrderCustomerName.IsReadOnly = !isCustomer;
            txtOrderDeliveryDate.IsReadOnly = !isCustomer;
            txtOrderID.IsReadOnly = !isCustomer;
            txtOrderOrderDate.IsReadOnly = !isCustomer;
            txtOrderShipDate.IsReadOnly = !isCustomer;
            txtOrderStatus.IsReadOnly = !isCustomer;
            txtOrderOrderDate.Text = order.OrderDate.ToString();
            txtOrderStatus.Text = order.Status.ToString();
            txtOrderShipDate.Text = order.ShipDate.ToString();
            txtOrderDeliveryDate.Text = order.DeliveryDate.ToString();
            BO.Order order1 = bl.Order.ReadOrderInformation(order.ID);
            this.DataContext=order1;
            button_update_delivery.Visibility= !isCustomer ? Visibility.Collapsed : Visibility.Visible;  
            button_update_shipping.Visibility= !isCustomer ? Visibility.Collapsed : Visibility.Visible;
            try
            {
                //OrderItemsListView.ItemsSource = order.Items;
                orderObs = (order.Items == null) ? new() : new(order.Items);
                OrderItemsListView.DataContext = orderObs;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        private void back_To_Main(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void btn_update_shipping(object sender, RoutedEventArgs e)
        {
            BO.Order orderToUpdate=bl.Order.UpdateShipping(id);
            txtOrderStatus.Text = orderToUpdate.Status.ToString();
            txtOrderShipDate.Text = orderToUpdate.ShipDate.ToString();
            txtOrderDeliveryDate.Text = orderToUpdate.DeliveryDate.ToString();
        }
        private void btn_update_delivery(object sender, RoutedEventArgs e)
        {
            BO.Order orderToUpdate = bl.Order.UpdateDelivery(id);
            txtOrderStatus.Text = orderToUpdate.Status.ToString();
            txtOrderShipDate.Text = orderToUpdate.ShipDate.ToString();
            txtOrderDeliveryDate.Text = orderToUpdate.DeliveryDate.ToString();
        }
    }
}
