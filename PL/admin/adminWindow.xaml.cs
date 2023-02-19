using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Collections.ObjectModel;
namespace PL.admin
{
    /// <summary>
    /// Interaction logic for adminWindow.xaml
    /// </summary>
    /// 
    public partial class adminWindow : Window 
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Product p = new BO.Product();

        private ObservableCollection<BO.ProductForList> pl { get; set; }

        private BO.Order ord = new BO.Order();
        private BO.ProductItem pi = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public adminWindow(IBl b)
        {
            try
            {
                InitializeComponent();
                pl = bl.Product.ReadListProducts()==null? new(): new(bl?.Product?.ReadListProducts());
                bl = b;
                ProductsListview.DataContext = pl;
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
                pi.Amount = p.InStock;
                pi.InStock = p.InStock > 0 ? true : false;
                new ProductWindow(pi, "update",pl).Show();
                this.Hide();
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
                new OrderWindow(ord, true).Show();
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btn_Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
