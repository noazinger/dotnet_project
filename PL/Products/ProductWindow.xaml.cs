using BlApi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PL.Products
{
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = Factory.Get();
        private int id = 0;
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));

        }
        public ProductWindow(BO.ProductItem ob)
        {
            try
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void comboBox_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            object categor = CategorySelector.SelectedItem;
            try
            {
                IEnumerable<BO.ProductItem> list = bl.Product.ReadProductByCategoty((BO.catagory)categor);
            }
            catch(Exception exc)
            {
                MessageBox.Show( "The Data Is Empty");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string function = func_butt.Content.ToString();
            if (function == "add")
            {
                try
                {
                    BO.Product product = new();
                    product.ID=Config.ProductID;
                    product.Name = name.Text;
                    product.Price = double.Parse(price.Text);
                    object categor = CategorySelector.SelectedItem;
                    product.catagory = (BO.catagory)categor;
                    product.InStock = int.Parse(amount.Text);
                    bl.Product.Add(product);
                }
                catch (Exception exc)
                {
                    MessageBox.Show( "the item is already exists");
                }
                   
            }
            if(function == "update")
            {
                try
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
                catch (Exception exc)
                {
                    MessageBox.Show( "The Product is not found");
                }

            }
            new ProductForListWindow(bl).Show();
            this.Hide();
        }
        private void back_To_Main(object sender, RoutedEventArgs e)
        {
            new ProductForListWindow(bl).Show();
            this.Close();
        }

      
    }
    public class Config
    {
        public static int ProductId = 10000;
        static public int ProductID { get { return ProductId++; } }
    }
}
