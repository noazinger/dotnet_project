using BlApi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Xml.Linq;
using PL.admin;
using System.Collections.ObjectModel;
namespace PL.Products
{
    public partial class ProductWindow : Window
    {
        private ObservableCollection<BO.ProductForList> pw;
        Window adminW;
        BO.Cart cart=new BO.Cart();
        BO.Product prod=new BO.Product();
        BlApi.IBl? bl = Factory.Get();
        private int id = 0;
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));

        }
        public ProductWindow(BO.ProductItem ob, BO.Cart c)
        {
            //נכנס לכאן
            this.DataContext = ob;
            InitializeComponent();
            id = ob.ID;
            name.Text = ob.Name;
            price.Text = ob.Price.ToString();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));
           
            CategorySelector.SelectedItem = ob.catagory;
            amount.Text = ob.Amount.ToString();
            func_butt.Content = "Add to cart";
            name.IsReadOnly = true;
            price.IsReadOnly = true;
            amount.IsReadOnly = true;
            CategorySelector.IsEnabled = false;
            delete_btn.Visibility = Visibility.Collapsed;
            cart = c;


        }

        public ProductWindow(BO.ProductItem ob, string btn, ObservableCollection<BO.ProductForList>? pl = null)
        {
            pw = pl;

            try
            {
                InitializeComponent();
                if (btn == "add") delete_btn.Visibility = Visibility.Collapsed;
                    id = ob.ID;
                name.Text = ob.Name;
                price.Text = ob.Price.ToString();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.catagory));
                CategorySelector.SelectedItem = ob.catagory;
                amount.Text = ob.Amount.ToString();
                func_butt.Content = btn;
                if (btn == "Add to cart")
                {
                    name.IsReadOnly = true;
                    price.IsReadOnly = true;
                    CategorySelector.IsEnabled = false;
                    amount.IsReadOnly = true;
                }
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
                MessageBox.Show(exc.Message);
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
                    XElement? config = XDocument.Load(@"..\xml\dal-config.xml").Root;
                    product.ID = Convert.ToInt32(config?.Element("ids").Element("productId").Value);
                    config.Element("ids").Element("productId").Value = (product.ID + 1).ToString();
                    config.Save(@"..\xml\dal-config.xml");
                    product.Name = name.Text;
                    product.Price = double.Parse(price.Text);
                    object categor = CategorySelector.SelectedItem;
                    product.catagory = (BO.catagory)categor;
                    product.InStock = int.Parse(amount.Text);
                    bl?.Product.Add(product);
                    pw = new(bl?.Product?.ReadListProducts());
                }
                catch (Exception exc)
                {
                    MessageBox.Show( exc.Message);
                }
                new ProductForListWindow(bl,false,prod).Show();

            }
            if (function == "update")
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
                    pw.Clear();
                    IEnumerable<BO.ProductForList>pp= bl.Product.ReadListProducts();
                    foreach (var item in pp){
                        pw.Add(item);
                    }
                    new adminWindow(bl).Show();
                    Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
             /*   new ProductForListWindow(bl, false, prod).Show();*/

            }
            if (function== "Add to cart")
            {
                bl.Cart.Add(cart, id);
                new Orders.OrderForList(bl,cart).Show();
            }
            Hide();
        }
        private void back_To_Main(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.Delete(prod.ID);
                adminWindow a = new(bl);
                a.Show();
                Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
  
}
