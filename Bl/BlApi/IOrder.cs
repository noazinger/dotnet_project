using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> ReadOrders();
        public IOrder ReadOrderInformation(int id);
        public IOrder UpdateShipping(int orderNumber);
        public IOrder UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
