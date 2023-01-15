using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;




//private ObservableCollection<BO.OrderItem> cl { get; set; }
//public CartWindow(IBl blP, BO.Cart c, ProductItems lastW, MainWindow mainW)
//{
//    InitializeComponent();
//    //cl=collectionList;
//    cart = c;
  
//    cl = cart.Items == null ? new() : new(cart.Items);
//    bl = blP;
//    this.DataContext = cart;
//    OrderItemsInCart.DataContext = cl;
//}


namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart thisCart = new();
        private ObservableCollection<BO.OrderItem> cl { get; set; }
        private ObservableCollection<double> TotalP = new();

        public CartWindow(BO.Cart cart)
        {
           InitializeComponent();
            //  OrderItemView.ItemsSource = cart.items;
            thisCart = cart;
/*            this.DataContext = cart.items;
*/            cl = cart.items == null ? new() : new(cart.items);
/*            TotalP.Add(cart.TotalPrice);
*/           total_price.IsReadOnly = true;
            mainGrid.DataContext = thisCart;
            OrderItemView.DataContext = cl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerName.Text == "") throw new BO.NotValidException("the name isnt valid");
            if (CustomerAddress.Text == "") throw new BO.NotValidException("the address isnt valid");
            var Email = new EmailAddressAttribute();
            if (!Email.IsValid(CustomerEmail.Text) || CustomerEmail.Text == "") throw new BO.NotValidException("the email isnt valid");
            MessageBox.Show("please complete your details");
            bl.Cart.OrderConfirmation(thisCart, CustomerName.Text, CustomerEmail.Text, CustomerAddress.Text);
            new MainWindow().Show();
        }
        private void Increase(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            thisCart= bl.Cart.Update(thisCart, orderToUpdate.ProductID, orderToUpdate.Amount+1);
            TotalP.Clear();
            TotalP.Add(thisCart.TotalPrice);
            cl.Clear();
            foreach (var item in thisCart?.items)
            {
                cl.Add(item);
            }
        }
        private void Decrease(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            thisCart = bl.Cart.Update(thisCart, orderToUpdate.ProductID, orderToUpdate.Amount - 1);
            cl.Clear();

            foreach (var item in thisCart?.items)
            {
                cl.Add(item);
            }
        }
        private void Remove(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            thisCart = bl.Cart.Update(thisCart, orderToUpdate.ProductID, 0);

            cl.Clear();

            foreach (var item in thisCart.items)
            {
                cl.Add(item);
            }
        }
    }
}
