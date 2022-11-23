using System;
using DO;
namespace BO;

public class Cart
{
	public string CustomerName { get; set; }
	public string CustomerEmail { get; set; }
	public string CustomerAddress { get; set; }
	public List<OrderItem> items { get; set; }
	public double TotalPrice { get; set; }

    public override string ToString() => $@"
        Customer Name={CustomerName},
        Customer Email={CustomerEmail}, 
        Customer Address={CustomerAddress},
        items={items},
        Total Price = {TotalPrice},
        ";
}
