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
using BlImplementation;
namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        IBl bl = new BlImplementation.Bl();
        public OrderForList(IBl b)
        {
            InitializeComponent();
            bl = b;
            OrdersListView.ItemsSource = b.Order.ReadOrders();

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}
