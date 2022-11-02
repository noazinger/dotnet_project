using DO;
namespace Dal;

    internal struct DataSource
{
    private static void s_Initialize()
    {
        createProductData();
        
    }

    static DataSource() { s_Initialize(); }
    static Random readionly = new Random();
    public static OrderItem[] orderItemsArr = new OrderItem[200];
    public static Order[] ordersArr = new Order[100];
   public static Product[] productsData=new Product[50];
    public static void createProductData()
    {
      for(int i = 0; i < 12; i++)
        {
            productsData[i] = new Product();
            productsData[i].ID = readionly.Next(100000, 999999);
            Array values = Enum.GetValues(typeof(ProductsNames));
            Random random = new Random();
           Bar randomBar = (Bar)values.GetValue(random.Next(values.Length))
        }
    }
}

