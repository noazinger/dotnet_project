using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList?> ReadOrders();
        public BO.Order ReadOrderInformation(int id);
        public BO.Order UpdateShipping(int orderNumber);
        public BO.Order UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
