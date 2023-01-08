using BlApi;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;
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

        public OrderWindow(Order b)
        {
            InitializeComponent();

        }
    }
}
