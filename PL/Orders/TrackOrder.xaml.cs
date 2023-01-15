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
using System.Collections.ObjectModel;
using BlApi;
namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for TrakOrder.xaml
    /// </summary>
    public partial class TrakOrder : Window
    {
        BlApi.IBl? bl = Factory.Get();
        BO.Order myOrder = new();
        public TrakOrder(BO.Order order, IBl b)
        {
            InitializeComponent();
            bl = b;
            Order_status.Content = order.Status.ToString();
            myOrder = order;
            BO.OrderTracking orderTracking = bl.Order.OrderTrack(order.ID);
            this.DataContext = orderTracking;   
            ListViewItems.ItemsSource = new ObservableCollection<Tuple<DateTime, BO.OrderStatus>>(orderTracking.packageStatus);
        }
        public void Order_d(object sender, RoutedEventArgs e)
        {
            new OrderWindow(myOrder, false).Show();
            this.Close();

        }
    }
}
