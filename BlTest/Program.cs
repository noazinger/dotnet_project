using DalApi;
using DalList;
using BO;

IDal dalEntity = new Dal.DalList();
//================ Order Functions ================//
void AddOrder()
{
    Order newOrder = new Order();
    Console.WriteLine("enter customer name");
    newOrder.CustomerName = Console.ReadLine();
    Console.WriteLine("enter costumer email");
    newOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter costumer address");
    newOrder.CustomerAddress = Console.ReadLine();
    newOrder.OrderDate = DateTime.Today;
    TimeSpan shipSpan = TimeSpan.FromDays(2);
    newOrder.ShipDate = newOrder.OrderDate + shipSpan;
    TimeSpan deliverySpan = TimeSpan.FromDays(7);
    newOrder.DeliveryDate = newOrder.ShipDate + deliverySpan;
    dalEntity.Order.Create(newOrder);

}


void ViewOrder()
{
    foreach (Order item in dalEntity.Order.Read())
    {
        if (item.ID != 0)
        {
            Console.WriteLine(item);
        }

    }

}

void ViewSingleOrder(int id)
{
    Order orders = dalEntity.Order.ReadSingle(id);
    Console.WriteLine(orders);

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
    dalEntity.Order.Update(newOrder);
}

void OrderFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add orders  " +
        "2. view orders 3. view single order 4. update order 5. delete order  0. to exit");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
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
                    dalEntity.Order.Delete(d_id);
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while (choice != 0);
}

//================ Product Functions ================//

void ProductFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add products " +
        " 2. view products 3. view single product 4. update product 5. delete product");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
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
                    dalEntity.Product.Delete(d_id);
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while (choice != 0);
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
    Console.WriteLine("enter the amount of product");
    product.inStock = (int)Convert.ToInt64(Console.ReadLine());
    dalEntity.Product.Create(product);
}

void ViewProduct()
{
    foreach (Product item in dalEntity.Product.Read())
    {
        if (item.ID != 0)
        {
            Console.WriteLine(item);
        }

    }
}

void ViewSingleProduct(int id)
{
    Product Product = dalEntity.Product.ReadSingle(id);
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
    dalEntity.Product.Update(newProduct);
}

//================ Order-Item Functions ================//

void AddOrderItem()
{
    OrderItem newOrderItem = new OrderItem();
    Console.WriteLine("enter order id");
    newOrderItem.OrderID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter Product id");
    newOrderItem.ProductID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter  amount");
    newOrderItem.Amount = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter  price");
    newOrderItem.Price = (int)Convert.ToInt64(Console.ReadLine());
    dalEntity.OrderItem.Create(newOrderItem);
}
void ViewOrdersItems()
{
    foreach (OrderItem item in dalEntity.OrderItem.Read())
    {
        Console.WriteLine(item);
    }

}
void ViewSingleOrderItem()
{
    Console.WriteLine("enter id order to view");
    int v_id = (int)Convert.ToInt64(Console.ReadLine());
    OrderItem orderItem = dalEntity.OrderItem.ReadSingle(v_id);
    Console.WriteLine(orderItem);
}
void UpDateOrderItem()
{
    OrderItem newOrderItem = new OrderItem();
    Console.WriteLine("enter the id order to update");
    newOrderItem.OrderID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter order id");
    newOrderItem.OrderID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter product id");
    newOrderItem.ProductID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter amount");
    newOrderItem.Amount = (int)Convert.ToInt64(Console.ReadLine());
    newOrderItem.Price = (int)Convert.ToInt64(Console.ReadLine());
    dalEntity.OrderItem.Update(newOrderItem);

}

void OrderItemFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add order item" +
      " 2. view orders items 3. view single order item 4. update order item 5. delete order item");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    AddOrderItem();
                    break;
                case 2:
                    ViewOrdersItems();
                    break;
                case 3:

                    ViewSingleOrderItem();
                    break;
                case 4:
                    UpDateOrderItem();
                    break;
                case 5:
                    Console.WriteLine("enter th id product to delete");
                    int d_id = (int)Convert.ToInt64(Console.ReadLine());
                    dalEntity.Product.Delete(d_id);
                    break;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.ToString());
        }
    } while (choice != 0);
}

void main()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. orders 2. products 3. cart 0. to exit");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
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
    } while (choice != 0);
}
main();

