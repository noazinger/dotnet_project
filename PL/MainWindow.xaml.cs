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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {

            InitializeComponent();
        }

        private void admin_b(object sender, RoutedEventArgs e)
        {
            try
            {
                admin.adminWindow w = new(bl);
                w.Show();   
                //new Products.ProductForListWindow(bl).Show();
                this.Hide();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }

        private void newOrder_b(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Cart cart=new BO.Cart();  
                new Orders.OrderForList(bl,cart).Show();
                this.Hide();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        private void Track_b(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(number.Text);
            BO.Order order= bl.Order.ReadOrderInformation(id);
            new Orders.TrakOrder(order,bl).Show();
            this.Hide();
        }

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            new Simulator().Show();

        }
    }
}
