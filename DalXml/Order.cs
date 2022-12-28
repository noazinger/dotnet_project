using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

    namespace Dal;
    using DalApi;
    using DO;

internal class Order : IOrder 
{
    public int Add(DO.Order or)
    {
        return 1;
    }

    void Create(Order or)
    {

        XDocument orderElement = XDocument.Load("..\\xml\\Order.xml");
        XElement ID = new XElement("ID", or.ID);
        XElement CustomerName = new XElement("CustomerName", or.CustomerName);
        XElement CustomerEmail = new XElement("CustomerEmail", or.CustomerEmail);
        XElement CustomerAddress = new XElement("CustomerAddress", or.CustomerAddress);
        XElement OrderDate = new XElement("OrderDate", or.OrderDate);
        XElement ShipDate = new XElement("ShipDate", or.ShipDate);
        XElement DeliveryDate = new XElement("ShipDate", or.DeliveryDate);
        XElement order = new XElement("order", ID, CustomerName, CustomerEmail, CustomerAddress, OrderDate, ShipDate, DeliveryDate);
        orderElement.Add(order);
        orderElement.Save("Order.xml");
    }



    void Update(Order n)
    {

    }
    IEnumerable<Order> Read(Func<Order, bool> func = null)
    {
        List<Order> n = new List<Order>();
        return n;
    }

    Order ReadSingle(int id)
    {
        Order order = new Order();
        return order;
    }
    void Delete(int id)
    {

    }

}
 

