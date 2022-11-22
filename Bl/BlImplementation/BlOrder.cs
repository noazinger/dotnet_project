using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        IDal dalEntity = new Dal.DalList();
        public IEnumerable<IOrder> ReadOrders()
        {
            BO.OrderForList list;
            foreach (DO.Order item in dalEntity.Order.Read())
            {
                BO.Order()
                list.add(item);
            }
            return list;
        }
        public IOrder ReadOrderInformation(int id);
        public IOrder UpdateShipping(int orderNumber);
        public IOrder UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
