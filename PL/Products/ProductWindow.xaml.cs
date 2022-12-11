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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl bl = new BlImplementation.Bl();
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            
        }
        public ProductWindow(BO.ProductForList ob)
        {
            InitializeComponent();
            name.Text = ob.Name;
            price.Text = ob.Price.ToString();
            catagory.Text=ob.catagory.ToString();
            amount.Text = "";
            func.Content = "update";


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.Product product= new();
            product.Name = name.Text;
            product.Price =double.Parse(price.Text);
            product.catagory = (BO.catagory)int.Parse(catagory.Text);
            product.InStock = int.Parse(amount.Text);
            bl.Product.Add(product);
            new MainWindow().Show();
            this.Hide();
        }
    }
}
