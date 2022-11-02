using DO;
namespace Dal;

    internal struct DataSource
{
    private static void s_Initialize()
    {
        createProductData();
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
    public static OrderItem[] orderItemsArr = new OrderItem[200];
    public static Order[] ordersData = new Order[100];
   public static Product[] productsData=new Product[50];
    public static void createProductData()
    {
      for(int i = 0; i < 12; i++)
        {
            productsData[i] = new Product();
            productsData[i].ID = readionly.Next(100000, 999999);
            int Irnd = readionly.Next(0, TupleProduct.Length);
            productsData[i].catagory = TupleProduct[Irnd].Item1;
            productsData[i].Name = TupleProduct[Irnd].Item2;
            productsData[i].Price = readionly.Next(100, 650);
            productsData[i].inStock= readionly.Next(0, 200);//לאתחל 5 אחוז מהמוצרים ב-0\        
        };
    }

    public static void createOrderData(int ID)
    {
        for (int i = 0; i < 22; i++)
        {
            ordersData[i] = new Order();
            ordersData[i].ID = Config.OrderIndex++;
            ordersData[i].ShipDate = DateTime.Now;
            ordersData[i].OrderDate = DateTime.Now;
            ordersData[i].DeliveryDate = DateTime.Now;        
        };
    }
    public class Config
    {
        static public int OrderIndex = 10000000;
    }


}

