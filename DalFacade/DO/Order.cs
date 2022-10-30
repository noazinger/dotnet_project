

namespace DO;

public struct Order
{
    int ID { set; get; }
    string CustomerName { set; get; }
    string CustomerEmail { set; get; }
    string CustomerAdress { set; get; }
    DateTime OrderDate { set; get; }
    DateTime ShipDate { set; get; }
    DateTime DeliveryDate { set; get; }

    public override string ToString() => $@"
    ID={ID},
    Customer Name={CustomerName}, 
    Customer Email={CustomerEmail},
    Customer Adress={CustomerAdress},
    Order Date={OrderDate},
    Ship Date = {ShipDate},
    Delivery Date= {DeliveryDate} ";
}






