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


        static private Lazy<DalList>? instance = null;
        public static IDal Instance { get => GetInstance(); }


        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();

        private DalList() { }
        public static DalList GetInstance()
        {
            lock (instance ??= new Lazy<DalList>(() => new DalList()))
            {

                return instance.Value;
            }

        }
    }


}

