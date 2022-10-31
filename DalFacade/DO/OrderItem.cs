using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

    public struct OrderItem
    {
    int ProductID { set; get;}
    int OrderID { set; get;}
    double Price { set; get;}
    int Amount { set; get;}

    public override string ToString() => $@"
        Product ID={ProductID}, 
        Order ID = {OrderID},
    	Price= {Price},
    	Amount= {Amount}
";
}

