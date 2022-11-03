using DalFacade;
using DO;
using DalList;




void AddOrder()
{  
 
    Order newOrder=new Order();
    Console.WriteLine("enter costumer name");
    newOrder.CustomerName= Console.ReadLine();
    Console.WriteLine("enter costumer email");
    newOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter costumer address");
    newOrder.CustomerAdress = Console.ReadLine();
    newOrder.OrderDate= DateTime.Today;
    TimeSpan shipSpan = TimeSpan.FromDays(2);
    newOrder.ShipDate = newOrder.OrderDate + shipSpan;
    TimeSpan deliverySpan = TimeSpan.FromDays(7);
    newOrder.DeliveryDate = newOrder.ShipDate + deliverySpan;
    DalOrder.Create(newOrder);
   
}
void PrintOrder(Order obj)
{
    Console.WriteLine("ID:" + obj.ID);
    Console.WriteLine("Customer Name:" + obj.CustomerName);
    Console.WriteLine("Customer email:" + obj.CustomerEmail);
    Console.WriteLine("Customer address:" + obj.CustomerAdress);
    Console.WriteLine("order date:" + obj.OrderDate);
    Console.WriteLine("ship date:" + obj.ShipDate);
    Console.WriteLine("delivery date:" + obj.DeliveryDate);


}
void ViewOrder()
{
    Order[] ordersArr = DalOrder.Read();
    for(int i = 0; i < ordersArr.Length; i++)
    {
        PrintOrder(ordersArr[i]);
    }
}

void ViewSingleOrder(int id)
{
    Order orders = DalOrder.ReadSingle(id);
    PrintOrder(orders);

}

void UpDateOrder()
{
    Order newOrder = new Order();
    Console.WriteLine("enter the id order to update");
    newOrder.ID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter costumer name");
    newOrder.CustomerName = Console.ReadLine();
    Console.WriteLine("enter costumer email");
    newOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter costumer address");
    newOrder.CustomerAdress = Console.ReadLine();
    DalOrder.UpDate(newOrder);

}

void OrderFunc()
{

    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add orders" +
      " 2. view orders 3. view single order 4. update order 5. delete order  0. to exit");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    AddOrder();
                    break;
                case 2:
                    ViewOrder();
                    break;
                case 3:
                    Console.WriteLine("enter id order to view");
                    int v_id = (int)Convert.ToInt64(Console.ReadLine());
                    ViewSingleOrder(v_id);
                    break;
                case 4:
                    UpDateOrder();
                    break;
                case 5:
                    Console.WriteLine("enter th id order to delete");
                    int d_id = (int)Convert.ToInt64(Console.ReadLine());
                    DalOrder.Delete(d_id);
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while ((choice > 0 || choice < 5));
}

void ProductFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add orders" +
      " 2. view orders 3. view single order 4. update order 5. delete order  0. to exit");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ViewProduct();
                    break;
                case 3:
                    Console.WriteLine("enter id product to view");
                    int v_id = (int)Convert.ToInt64(Console.ReadLine());
                    ViewSingleProduct(v_id);
                    break;
                case 4:
                    UpDateProduct();
                    break;
                case 5:
                    Console.WriteLine("enter th id product to delete");
                    int d_id = (int)Convert.ToInt64(Console.ReadLine());
                    DalProduct.Delete(d_id);
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while ((choice > 0 && choice < 5));
}

void AddProduct()
{
    Product product = new Product();
    int id = DataSource.Config.ProductIndex++;
    product.ID = id;
    Console.WriteLine("enter name for the new product");
    product.Name = Console.ReadLine();
    Console.WriteLine("enter the product's category: 1 - suit, 2 - pants, 3 - tie, 4 -  shirt, 5 - accssories");
    int choice = (int)Convert.ToInt64(Console.ReadLine());
    product.catagory = (catagory)choice;
    Console.WriteLine("enter price for the new product");
    product.Price = float.Parse(Console.ReadLine());
    Console.WriteLine("enter amount in stock");
    Console.WriteLine("enter the amout of product in stock");
    product.inStock = (int)Convert.ToInt64(Console.ReadLine());
    DalProduct.Create(product);
}

void ViewProduct()
{
    Product[] ProductArr = DalProduct.Read();
    for (int i = 0; i < ProductArr.Length; i++)
    {
        Console.WriteLine(ProductArr[i]);
    }
}

void ViewSingleProduct(int id)
{
    Product Product = DalProduct.ReadSingle(id);
    Console.WriteLine(Product);
}

void UpDateProduct()
{
    Product newProduct = new Product();
    Console.WriteLine("enter the id order to update");
    newProduct.ID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter  name");
    newProduct.Name = Console.ReadLine();
    Console.WriteLine("enter price");
    newProduct.Price = float.Parse(Console.ReadLine());
    Console.WriteLine("enter costumer category");
    int choice = (int)Convert.ToInt64(Console.ReadLine());
    newProduct.catagory = (catagory)choice;
    Console.WriteLine("enter costumer inStock");
    newProduct.inStock = (int)Convert.ToInt64(Console.ReadLine());
    DalProduct.UpDate(newProduct);
}
void main()
{
    Console.WriteLine("Please enter your choice: 1. orders 2. products 3. order-items 0. to exit");
    int choice = (int)Convert.ToInt64(Console.ReadLine());
    do
    {
        try
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    OrderFunc();
                    break;
                case 2:
                    ProductFunc();
                    break;
                case 3:
                    OrderItemFunc();
                    break;

            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while (choice >= 1 && choice <= 3);
   
}
main();
