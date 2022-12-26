using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalList;
public struct DataSource
{
    const int orderItemSize = 40;
    const int orderSize = 10;
    const int productSize = 40;

    private static void s_Initialize()
    {
        createProductData();
        createOrderData();
        createOrderItemData();
    }

    public static (catagory, string)[] TupleProduct = new[] {
        (catagory.suit, "black_suit"), (catagory.pants, "boy_pent"),
        (catagory.tie, "patterned_ties"),(catagory.shirt, "long_shirt"),
        (catagory.accssories, "silver_cufflinks"),(catagory.suit, "cotton_suit"),
        (catagory.accssories, "belt"),(catagory.shirt, "short_Shirt"),
        (catagory.tie, "grooms_tie"),(catagory.accssories, "swarovski_cufflinks")
    };
    static DataSource() { s_Initialize(); }
    static Random readionly = new Random();
    public static List<OrderItem> orderItemsData = new List<OrderItem>();
    public static List<Order> ordersData = new List<Order>();
    public static List<Product> productsData = new List<Product>();
    public static void createProductData()
    {
        for (int i = 0; i < productSize; i++)
        {
            Product product = new Product();
            product.ID = Config.ProductID;
            int Irnd = readionly.Next(0, TupleProduct.Length);
            product.catagory = TupleProduct[Irnd].Item1;
            product.Name = TupleProduct[Irnd].Item2;
            product.Price = readionly.Next(50, 650);
            product.inStock = readionly.Next(0, 200);//לאתחל 5 אחוז מהמוצרים ב-0\
            Config.ProductIndex++;
            productsData.Add(product);

        };
    }

    public static void createOrderData()
    {
        string[] CustomerName = { "Noa", "Tovi", "Gitty" };
        string[] CustomerEmail = { "Noa123@gmail.com", "Tovi40@gmail.com", "Gitty865@gmail.com" };
        string[] CustomerAddress = { "Kotcher_8", "Revivim_26", "Bleui_65" };
        for (int i = 0; i < orderSize; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderID;
            order.CustomerName = CustomerName[(int)readionly.NextInt64(CustomerName.Length)];
            order.CustomerEmail = CustomerEmail[(int)readionly.NextInt64(CustomerEmail.Length)];
            order.CustomerAddress = CustomerAddress[(int)readionly.NextInt64(CustomerAddress.Length)];
            order.OrderDate = DateTime.MinValue;
            TimeSpan delivery = TimeSpan.FromDays(2);
            order.DeliveryDate = order.OrderDate + delivery;
            TimeSpan shipDays = TimeSpan.FromDays(7);
            order.ShipDate = order.DeliveryDate + shipDays;
            ordersData.Add(order);
        };
    }
    public static void createOrderItemData()
    {

        for (int i = 0; i < ordersData.Count(); i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderID = ordersData[i].ID;
            int productIndex = readionly.Next(0, productsData.Count());
            orderItem.ProductID = productsData[productIndex].ID;
            orderItem.Amount = 1;
            orderItem.Price = orderItem.Amount * productsData[productIndex].Price;
            orderItemsData.Add(orderItem);
        }
        for (int i = 0; i < orderItemSize; i++)
        {
            int orderIndex = readionly.Next(0, ordersData.Count());
            int orderId = ordersData[orderIndex].ID;
            OrderItem orderItem = new OrderItem();
            orderItem.OrderID = orderId;
            int productIndex = readionly.Next(0, productsData.Count());
            orderItem.ProductID = productsData[productIndex].ID;
            orderItem.Amount = 1;
            orderItem.Price = orderItem.Amount * productsData[productIndex].Price;
            orderItemsData.Add(orderItem);

        };
    }

    public class Config
    {
        static public int OrderIndex = 0;
        static public int OrderItemIndex = 0;
        static public int OrderFinalIndex = 100;
        static public int ProductIndex = 0;
        static public int ProductId = 10000;

        static public int OrderId = 10000;
        static public int ProductID { get { return ProductId++; } }

        static public int OrderID { get { return OrderId++; } }
    }
}




