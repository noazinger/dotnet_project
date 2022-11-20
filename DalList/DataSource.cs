using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalList;
using DO;
using DO;
namespace DalList;
public struct DataSource
{
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
    public static List<OrderItem> [] sorderItemsData = new List<OrderItem>[200];
    public static List<Order>[] ordersData = new List<Order>[100];                                      
    public static List<Product>[] productsData = new List<Product>[50];
/*    public static Product[] productsData = new Product[50];
*/
    public static void createProductData()
    {
        for (int i = 0; i < 10; i++)
        {
            productsData[i] = new Product();
            productsData[i].ID = readionly.Next(100000, 999999);
            int Irnd = readionly.Next(0, TupleProduct.Length);
            productsData[i].catagory = TupleProduct[Irnd].Item1;
            productsData[i].Name = TupleProduct[Irnd].Item2;
            productsData[i].Price = readionly.Next(100, 650);
            productsData[i].inStock= readionly.Next(0, 200);//לאתחל 5 אחוז מהמוצרים ב-0\
            Config.ProductIndex++;
        };
    }

    public static void createOrderData()
    {
        string[] CustomerName = { "Noa", "Tovi", "Gitty" };
        string[] CustomerEmail = { "Noa123@gmail.com", "Tovi40@gmail.com", "Gitty865@gmail.com" };
        string[] CustomerAdress = { "Kotcher_8", "Revivim_26", "Bleui_65" };
        for (int i = 0; i < 3; i++)
        {
            Order order = new Order();
            order.ID = Config.OrderIndex++;
            order.CustomerName = CustomerName[(int)readionly.NextInt64(CustomerName.Length)];
            order.CustomerEmail = CustomerEmail[(int)readionly.NextInt64(CustomerEmail.Length)];
            order.CustomerAdress = CustomerAdress[(int)readionly.NextInt64(CustomerAdress.Length)];
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
        for (int i = 0; i < 22; i++)
        {
            int orderIndex = readionly.Next(0, ordersData.Length);
            int orderId = ordersData[orderIndex].ID;
            int rnd = readionly.Next(0, 5);
            for (int j = 0; j < rnd; j++)
            {
                OrderItem orderItem = new OrderItem();
/*                orderItemsData[i] = new OrderItem();
*/                orderItem.OrderID = orderId;
                int productIndex = readionly.Next(0, productsData.Length);
                orderItemsData[i].ProductID = productsData[productIndex].ID;
                orderItemsData[i].Amount = readionly.Next(0, productsData[productIndex].inStock);
                orderItemsData[i].Price = orderItemsData[i].Amount * productsData[productIndex].Price;
            };
        };
    }

    public  class Config
    {
        static public int OrderIndex = 0;
        static public int OrderItemIndex = 0;
        static public int OrderFinalIndex = 100;
        static public int ProductIndex = 0;
    }
}




