﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrder : Icrud<Order>
    {
        public int Add(DO.Order order);
    }
}
