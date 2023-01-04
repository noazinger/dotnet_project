// See https://aka.ms/new-console-template for more information
using Dal;
BlApi.IBl? bl = BlApi.Factory.Get();

void Add_Order()
{
    bl.Order.UpdateDelivery(20008);
}