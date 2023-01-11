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
namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Product p = new BO.Product();
        public ProductForListWindow(IBl b, bool IsCustomer, BO.Product prod)
        {
            InitializeComponent();
            bl = b;
            try
            {
                ProductListView.ItemsSource = b.Product.ReadCatalog();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /*private void categorySelectorBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            p.Category = (BO.Category)categorySelectorBox.SelectedItem;
        }*/
        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //p.catagory = (BO.catagory)CategorySelector.SelectedItem;
                object categor = CategorySelector.SelectedItem;
                IEnumerable<BO.ProductItem> list = bl.Product.ReadProductByCategoty((BO.catagory)categor);
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

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem pr = (BO.ProductItem)((ListView)sender).SelectedItem;
            new ProductWindow(pr,"update").Show();
            this.Close();
        }
        private void Add_product_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).Show();
            Hide();
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
