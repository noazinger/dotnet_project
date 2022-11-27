using DalList;
using BO;
using BlApi;
using BlImplementation;

IBl blEntity = new BlImplementation.Bl();
//================ Order Functions ================//

void ViewOrders()
{
    foreach (BO.OrderForList item in blEntity.Order.ReadOrders())
    {
        if (item.ID != 0)
        {
            Console.WriteLine(item);
        }
    }
}

void ViewOrderInfo()
{
    Console.WriteLine("enter id order to view");
    int id = (int)Convert.ToInt64(Console.ReadLine());
    Order order = blEntity.Order.ReadOrderInformation(id);
    Console.WriteLine(order);
}

void UpdateShipping()
{
    Console.WriteLine("enter the id order to update shipping");
    int ID = (int)Convert.ToInt64(Console.ReadLine());
    blEntity.Order.UpdateShipping(ID);
}

void UpdateDelivery()
{
    Console.WriteLine("enter the id order to update delivery");
    int ID = (int)Convert.ToInt64(Console.ReadLine());
    blEntity.Order.UpdateShipping(ID);
}
void OrderFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. view orders  " +
        "2. view order information 3. update shipping 4. update delivery 5. delete order  0. to exit");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 1:
                    ViewOrders();
                    break;
                case 2:
                    ViewOrderInfo();
                    break;
                case 3:
                    UpdateShipping();
                    break;
                case 4:
                    UpdateDelivery();
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
        " 2. Read Product For Director 3. view catalog 4. read product for customer 5.view list of product  6.delete product 7.update product ");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    ViewSingleProduct("directory");
                    break;
                case 3:
                    viewCatalog();
                    break;
                case 4:
                    ViewSingleProduct("customer");
                    break;
                case 5:
                    viewProducts();
                    break;
                case 6:
                    Console.WriteLine("enter th id product to delete");
                    int d_id = (int)Convert.ToInt64(Console.ReadLine());
                    blentity.Product.Delete(d_id);
                    break;
                case 7:
                    UpDateProduct();
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
    BO.Product product=new BO.Product();
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
    product.InStock = (int)Convert.ToInt64(Console.ReadLine());
    blentity.Product.Add(product);
}

void ViewSingleProduct(string x)
{
    Console.WriteLine("enter id product to view");
    int v_id = (int)Convert.ToInt64(Console.ReadLine());
    BO.Product product=new BO.Product();
    product= x== "directory" ? blentity.Product.ReadSingleProductForDirector(v_id): blentity.Product.ReadSingleProductForCustomer(v_id);
    Console.WriteLine(product);

}

void viewCatalog()
{
    List<ProductItem> Product = blentity.Product.ReadCatalog().ToList();
    foreach(ProductItem ProductItem in Product) Console.WriteLine(ProductItem);
}
void viewProducts()
{
    List<ProductForList> Product = blentity.Product.ReadListProducts().ToList();
    foreach (ProductForList ProductForList in Product) Console.WriteLine(ProductForList);
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
    newProduct.InStock = (int)Convert.ToInt64(Console.ReadLine());
    blentity.Product.Update(newProduct);
}

//================ Cart Functions ================//

void AddItemToCart()
{
    Cart cart = new Cart();
    OrderItem newOrderItem = new OrderItem();
    Console.WriteLine("enter order id");
    var id = (int)Convert.ToInt64(Console.ReadLine());
/*    newOrderItem.OrderID = (int)Convert.ToInt64(Console.ReadLine());
*/    Console.WriteLine("enter Product id");
    newOrderItem.ProductID = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter  amount");
    newOrderItem.Amount = (int)Convert.ToInt64(Console.ReadLine());
    Console.WriteLine("enter  price");
    newOrderItem.Price = (int)Convert.ToInt64(Console.ReadLine());
    blEntity.Cart.Add(cart, id);
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

void CartFunc()
{
    int choice;
    do
    {
        Console.WriteLine("Please enter your choice: 1. Add item to cart" +
      " 2. update cart 3. order confirmation ");
        choice = (int)Convert.ToInt64(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    AddItemToCart();
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
                    CartFunc();
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

