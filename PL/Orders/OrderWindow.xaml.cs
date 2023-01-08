using BlApi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>

    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = Factory.Get();
        public OrderWindow(BO.ProductItem ob)
        {
            InitializeComponent();
        }
    }
}
