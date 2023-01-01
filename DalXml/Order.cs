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

    void Create(DO.Order or)
    {

        XDocument orderElement = XDocument.Load("..\\xml\\Order.xml");
        XElement order = new XElement("Order",
        new XElement("ID", or.ID),
         new XElement("CustomerName", or.CustomerName),
         new XElement("CustomerEmail", or.CustomerEmail),
         new XElement("CustomerAddress", or.CustomerAddress),
        new XElement("OrderDate", or.OrderDate),
         new XElement("ShipDate", or.ShipDate),
         new XElement("ShipDate", or.DeliveryDate));
        orderElement.Add(order);
        orderElement.Save("Order.xml");
    }
    void Update(DO.Order n)
    {
        XDocument orderElement = XDocument.Load("..\\xml\\Order.xml");
        XElement ID = new XElement("ID", n.ID);                       
        XElement CustomerName = new XElement("CustomerName", n.CustomerName);
        XElement CustomerEmail = new XElement("CustomerEmail", n.CustomerEmail);
        XElement CustomerAddress = new XElement("CustomerAddress", n.CustomerAddress);
        XElement OrderDate = new XElement("OrderDate", n.OrderDate);
        XElement ShipDate = new XElement("ShipDate", n.ShipDate);
        XElement DeliveryDate = new XElement("ShipDate", n.DeliveryDate);
        XElement order = new XElement("order", ID, CustomerName, CustomerEmail, CustomerAddress, OrderDate, ShipDate, DeliveryDate);
        orderElement.Save("Order.xml");

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


