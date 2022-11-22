using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        IDal dalEntity = new Dal.DalList();
        public IEnumerable<IOrder> ReadOrders()
        {
            List<OrderForList> ItemsList = new List<OrderForList>();
            foreach (DO.Order item in dalEntity.Order.Read())
            {
                /*BO.Order()*/
                ItemsList.Add(item);
            }
            return ItemsList;
        }
        public IOrder ReadOrderInformation(int id);
        public IOrder UpdateShipping(int orderNumber);
        public IOrder UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
