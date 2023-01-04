// See https://aka.ms/new-console-template for more information
using Dal;
BlApi.IBl? bl = BlApi.Factory.Get();

void Add_Order()
{
    bl.Order.UpdateDelivery(20008);
    List<DO.Order> list = new List<DO.Order>();
    list= (List<DO.Order>)bl.Order.ReadOrders();
    Console.WriteLine(list);
}
Add_Order();