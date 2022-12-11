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
            ProductListView.ItemsSource = b.Product.ReadListProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory)); 
        }
        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            /*object SelectedItem = AttributeSelector.SelectedItem;
            IEnumerable<BO.ProductForList> lst = bl.Product.GetProductByCategoty((BO.eCategory)SelectedItem);
            ProductsListview.ItemsSource = lst;*/

            object categor = CategorySelector.SelectedItem;
            IEnumerable<BO.ProductForList> list =bl.Product.ReadProductByCategoty((BO.catagory)categor);
            ProductListView.ItemsSource = list;
/*            message.Text = CategorySelector.SelectedItem.ToString();
*/        }
    }
}
