using DO;
using System;
namespace Dal;

    internal struct DataSource
{
    private static void s_Initialize()
    {
        createProductData();
        createOrderData();
        createOrderItemData();
    }


    public static (catagory, string)[] TupleProduct = new[] {
        (catagory.suit, "black_suit"), (catagory.pents, "boy_pent"),
        (catagory.tie, "patterned_ties"),(catagory.shirt, "long_shirt"),
        (catagory.accssories, "silver_cufflinks"),(catagory.suit, "cotton_suit"),
        (catagory.accssories, "belt"),(catagory.shirt, "short_Shirt"),
        (catagory.tie, "grooms_tie"),(catagory.accssories, "swarovski_cufflinks")
    };
    static DataSource() { s_Initialize(); }
    static Random readionly = new Random();
    public static OrderItem[] orderItemsData = new OrderItem[200];
    public static Order[] ordersData = new Order[100];
    public static Product[] productsData=new Product[50];

    public static void createProductData()
    {
      for(int i = 0; i < 10; i++)
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
        string[] CustomerAdress = { "Noa123@gmail.com", "Tovi40@gmail.com", "Gitty865@gmail.com" };
        for (int i = 0; i < 22; i++)
        {
            ordersData[i] = new Order();
            ordersData[i].ID = Config.OrderIndex++;
            ordersData[i].CustomerName = CustomerName[(int)readionly.NextInt64(CustomerName.Length)];
            ordersData[i].CustomerEmail = CustomerEmail[(int)readionly.NextInt64(CustomerEmail.Length)];
            ordersData[i].CustomerAdress = CustomerAdress[(int)readionly.NextInt64(CustomerAdress.Length)];
            ordersData[i].OrderDate = DateTime.MinValue;
            TimeSpan delivery = TimeSpan.FromDays(2);
            ordersData[i].DeliveryDate = ordersData[i].OrderDate + delivery;
            TimeSpan shipDays = TimeSpan.FromDays(7);
            ordersData[i].ShipDate = ordersData[i].DeliveryDate + shipDays;
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
                orderItemsData[i] = new OrderItem();
                orderItemsData[i].OrderID = orderId;
                int productIndex = readionly.Next(0, productsData.Length);
                orderItemsData[i].ProductID = productsData[productIndex].ID;
                orderItemsData[i].Amount = readionly.Next(0, productsData[productIndex].inStock);
                orderItemsData[i].Price = orderItemsData[i].Amount * productsData[productIndex].Price;
            };
        };
    }
    internal class Config
    {
        static public int OrderIndex = 0;
        static public int OrderiTemIndex = 0;
        static public int OrderFinalIndex = 100;
        static public int ProductIndex = 0;
        internal int getOrderFinalIndex() { return OrderFinalIndex++; }



    }


}

