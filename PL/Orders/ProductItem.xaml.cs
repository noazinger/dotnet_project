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
                ProductListView.ItemsSource = b.Product.ReadCatalog();
                List<string> strings = new List<string>();
               
                    foreach(var item in Enum.GetValues(typeof(BO.catagory))) { 
                        strings.Add(item.ToString());   
                     }
                strings.Add("see all");
                CategorySelector.ItemsSource = strings;     


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
            new PL.Carts.CartWindow(myCart).Show() ;
            this.Hide();



        }

        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
           
            try
            {
                
                object categor = CategorySelector.SelectedItem;
                string c = categor.ToString();
                IEnumerable<BO.ProductItem> list;
                if (categor == "see all") list = bl.Product.ReadCatalog();

                else
                {
                    BO.catagory enCat = (BO.catagory)Enum.Parse(typeof(BO.catagory), c);
                    list = bl.Product.ReadProductByCategoty(enCat);
                }
                ProductListView.ItemsSource = list;
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
    }
}
