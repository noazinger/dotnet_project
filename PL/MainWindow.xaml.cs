﻿using System;
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

                new Products.ProductForListWindow(bl).Show();
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

                new Orders.OrderForList(bl).Show();
                this.Hide();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }


    }
}
