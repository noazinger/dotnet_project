﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        int ID { get; set; }
        OrderStatus Status {get;set;}
        public override string ToString() => $@"
        Order Tracking ID={ID}, 
        Order Status = {Status},
    	";

    }
}
