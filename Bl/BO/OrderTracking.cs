using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public OrderStatus? Status {get;set;}
        public List<Tuple<DateTime, OrderStatus>> packageStatus=new();
        public override string ToString() => $@"
        Order Tracking ID={ID}, 
        Order Status = {Status},
    	";

    }
}
