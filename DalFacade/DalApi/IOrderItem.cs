using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrderItem : Icrud<OrderItem>
    {
        public IEnumerable<OrderItem> ReadByOrderId(int orderId);
        public OrderItem ReadSingleByOrderIdAndProductId(int orderId, int productId);
    }
}
