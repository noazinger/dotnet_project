using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    internal class IDal
    {
        Order _order { get;  }
        Product _product { get; }
        OrderItem _orderItem { get;}
    }
}
