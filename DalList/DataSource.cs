using DO;
namespace Dal;

    internal struct DataSource
    {
        static Random readionly = new Random();
    OrderItem[] orderItemsArr = new OrderItem[200];
    Order[] ordersArr = new Order[100];
    Product[] productsArr=new Product[50];
    int[] ProductsPrice = {200,100,50,40,300,43,450,70,20,120,550};
    int[] Amount = { 7, 98, 40, 104, 30, 65, 0, 0, 80, 30, 10, 5, 6, 15, 54 };
    Object[] productName = { new { catagory="suit", name="boySuit" },
       new { catagory="pents", name="boy pent" },
       new { catagory="tie", name="patterned ties" },
       new { catagory="shirt", name="long shirt" },
       new { catagory="accssorie", name=" silver cufflinks" },
       new { catagory="suit", name="100% cotton suit" },
       new { catagory="shirt", name="short Shirt" },
       new { catagory="accssorie", name="Belt" }

 
}

