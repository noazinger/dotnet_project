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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(BO.Cart cart)
        {
            InitializeComponent();
           OrderItemView.ItemsSource = cart.items;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("please complete your details");
            
        }
    }
}
