using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class BlOrder : IOrder<BO.OrderForList>
    {
        public IEnumerable<BO.OrderForList> ReadOrders()
        {
            IDal dalEntity = new Dal.DalList();
            List <BO.OrderForList> OrdersList = new List<BO.OrderForList>();
            foreach (var item in dalEntity.Order.Read())
            {
                BO.OrderForList order = new BO.OrderForList();
                order.ID = item.ID;
                order.CustomerName=item.CustomerName;
                if(item.OrderDate>=DateTime.Now && item.ShipDate<=DateTime.Now)
                {
                    order.Status = (BO.OrderStatus)1;
                }
                else if(item.ShipDate>=DateTime.Now && item.DeliveryDate <= DateTime.Now)
                {
                    order.Status = (BO.OrderStatus)2;
                }
                else
                {
                    order.Status = (BO.OrderStatus)3;
                }
                BO.OrderItem orderItem = new BO.OrderItem();

                
                order.AmountOfItems=(BO.OrderItem.It)
                order.Status = (BO.OrderStatus)item.;
                
                OrdersList.Add(order);
            }
            return OrdersList;
        }


        public IOrder ReadOrderInformation(int id);
        public IOrder UpdateShipping(int orderNumber);
        public IOrder UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
