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
    public partial class ProductWindow : Window
    {
        IBl bl = new BlImplementation.Bl();
        private int id = 0;
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));

        }
        public ProductWindow(BO.ProductItem ob)
        {
            InitializeComponent();
            id = ob.ID;
            name.Text = ob.Name;
            price.Text = ob.Price.ToString();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));
            CategorySelector.SelectedItem = ob.catagory;
            amount.Text = ob.Amount.ToString();
            func_butt.Content = "update";
        }
        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            object categor = CategorySelector.SelectedItem;
            IEnumerable<BO.ProductItem> list = bl.Product.ReadProductByCategoty((BO.catagory)categor);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string function = func_butt.Content.ToString();
            if (function == "add")
            {
                BO.Product product = new();
                product.Name = name.Text;
                product.Price = double.Parse(price.Text);
                object categor = CategorySelector.SelectedItem;
                 product.catagory = (BO.catagory)categor;
                product.InStock = int.Parse(amount.Text);
                bl.Product.Add(product);     
            }
            if(function == "update")
            {
                BO.Product product = new();
                product.ID = id;
                product.Name = name.Text;
                product.Price = double.Parse(price.Text);
                object categor = CategorySelector.SelectedItem;
                product.catagory = (BO.catagory)categor;
                product.InStock = int.Parse(amount.Text);
                bl.Product.Update(product);
            }
            new MainWindow().Show();
            this.Hide();
        }
        private void back_To_Main(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

      
    }
}
