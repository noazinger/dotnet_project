using System;
using System.Collections.Generic;
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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BO.Cart ca = new();
        public CartWindow(BO.Cart cart)
        {
           InitializeComponent();
           OrderItemView.ItemsSource = cart.items;
           this.DataContext = cart.items;
           
           total_price.Text = cart.TotalPrice.ToString();
           total_price.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ca.CustomerName == "") throw new BO.NotValidException("the name isnt valid");
            if (ca.CustomerAddress == "") throw new BO.NotValidException("the address isnt valid");
            var Email = new EmailAddressAttribute();
            if (!Email.IsValid(ca.CustomerEmail) || ca.CustomerEmail == "") throw new BO.NotValidException("the email isnt valid");
            MessageBox.Show("please complete your details");   
        }
    }
}
