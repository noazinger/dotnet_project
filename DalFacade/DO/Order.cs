

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
    Customer Name={CustomerName}: {Name}, 
    category - {Category}
    Price: {Price}
    Amount in stock: {InStock}";
}






