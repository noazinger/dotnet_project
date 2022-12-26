using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DalList;
namespace Dal
{
    sealed internal class DalList : IDal
    {
        public static DalApi.IDal? Instance { get; } = DalApi.Factory.Get();
        private DalList() { }
        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();

    }
}
