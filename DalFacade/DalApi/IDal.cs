﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi
{
    public interface IDal
    {
        public IOrder Order { get;  }
        public IProduct Product { get; }
        public IOrderItem OrderItem { get;}
    }
}