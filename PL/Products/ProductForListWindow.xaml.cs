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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        private IBl bl;
        public ProductForListWindow(IBl b)
        {
            InitializeComponent();
            bl = b;
            ProductListView.ItemsSource = b.Product.ReadCatalog();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory)); 
        }
        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            object categor = CategorySelector.SelectedItem;
            IEnumerable<BO.ProductItem> list = bl.Product.ReadProductByCategoty((BO.catagory)categor);
            ProductListView.ItemsSource = list;
        }
        private void back_To_Main(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)((ListView)sender).SelectedItem;
            new Products.ProductWindow(p).Show();
            this.Close();
        }
    }
}
