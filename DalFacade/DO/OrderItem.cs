using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

    public struct OrderItem
    {
    public int ProductID { set; get;}
    public int OrderID { set; get;}
    public double Price { set; get;}
    public int Amount { set; get;}

    public override string ToString() => $@"
        Product ID={ProductID}, 
        Order ID = {OrderID},
    	Price= {Price},
    	Amount= {Amount}
";
}

