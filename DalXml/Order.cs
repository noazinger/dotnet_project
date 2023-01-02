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

    public void Create(DO.Order or)
    {

        XElement? orderElement = XDocument.Load("..\\xml\\Order.xml").Root;
        XElement? order = new XElement("Order",
        new XElement("ID", or.ID),
         new XElement("CustomerName", or.CustomerName),
         new XElement("CustomerEmail", or.CustomerEmail),
         new XElement("CustomerAddress", or.CustomerAddress),
        new XElement("OrderDate", or.OrderDate),
         new XElement("ShipDate", or.ShipDate),
         new XElement("DeliveryDate", or.DeliveryDate));
        orderElement?.Element("Order")?.Add(order);
        orderElement?.Save("..\\xml\\Order.xml");
    }
   public void Update(DO.Order item)
    {
        XElement? orderElement = XDocument.Load("..\\xml\\Order.xml").Root;
        XElement? order= orderElement?.Element("Order").Elements("Order")
            .Where(e => e.Element("ID")?.Value == item.ID.ToString()).FirstOrDefault()??throw new Exception();
        order.Element("CustomerName").Value = item.CustomerName;
        //order?.Attribute("CustomerName")?.SetValue(item.CustomerName); // למה לא לעשות ככה?
        order.Element("CustomerEmail").Value = item.CustomerEmail;
        order.Element("CustomerAddress").Value = item.CustomerAddress;
        order.Element("OrderDate").Value = item.OrderDate.ToString();
        order.Element("ShipDate").Value = item.CustomerAddress;
        order.Element("DeliveryDate").Value = item.CustomerAddress;
        orderElement.Save("..\\xml\\Order.xml");
    }
   public IEnumerable<DO.Order> Read(Func<DO.Order, bool> func )
    {
        List<DO.Order> n = new List<DO.Order>();
        return n;
    }

    public DO.Order ReadSingle(int id)
    {
        XElement? orderElement = XDocument.Load("..\\xml\\Order.xml").Root;
        XElement? order = orderElement?.Element("Order").Elements("Order").
            Where(e => e.Element("ID")?.Value == id.ToString()).FirstOrDefault() ?? throw new Exception();
        DO.Order returnOrder = new DO.Order();
        returnOrder.ID= id;
        returnOrder.CustomerName = order?.Element("CustomerName").Value;
        returnOrder.CustomerEmail = order?.Element("CustomerEmail").Value;
        returnOrder.CustomerAddress = order?.Element("CustomerAddress").Value;
        returnOrder.OrderDate = Convert.ToDateTime(order?.Element("OrderDate").Value);
        returnOrder.ShipDate = Convert.ToDateTime(order?.Element("ShipDate").Value);
        returnOrder.DeliveryDate = Convert.ToDateTime(order?.Element("DeliveryDate").Value);
        return returnOrder;
    }
   public void Delete(int id)
    {
        XElement? orderElement = XDocument.Load("..\\xml\\Order.xml").Root;
        XElement? order = orderElement?.Element("Order").Elements("Order").
            Where(e => e.Element("ID")?.Value == id.ToString()).FirstOrDefault() ?? throw new Exception();
        order?.Remove();
        orderElement?.Save("..\\xml\\Order.xml");
    }
}


