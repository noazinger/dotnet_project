using DalFacade;
using Dal;
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
    Console.WriteLine("Please enter your choice: 1. Add orders" +
        " 2. view orders 3. view single order 4. update order 5. delete order  0. to exit");
    int choice = (int)Convert.ToInt64(Console.ReadLine());
    while (choice > 0 || choice < 5)
    {
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
    }
}
void main()
{
    Console.WriteLine("Please enter your choice: 1. orders 2. products 3. order-items 0. to exit");
    int choice = (int)Convert.ToInt64(Console.ReadLine());
    try
    {
        switch (choice)
        {
            case 0:
                return;
            case 1:
                OrderFunc();
                break;
        
                
        }
    }
    catch (Exception error)
    {
        Console.WriteLine(error.ToString());
    }
   
}
main();
