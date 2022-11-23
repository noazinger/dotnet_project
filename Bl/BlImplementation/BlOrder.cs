﻿
using BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        IDal dalEntity = new Dal.DalList();
        public IEnumerable<BO.OrderForList> ReadOrders()
        {
            List <BO.OrderForList> OrdersList = new List<BO.OrderForList>();
            try
            {
                foreach (var item in dalEntity.Order.Read())
                {
                    BO.OrderForList order = new BO.OrderForList();
                    order.ID = item.ID;
                    order.CustomerName = item.CustomerName;
                    if (item.OrderDate >= DateTime.Now && item.ShipDate <= DateTime.Now)
                    {
                        order.Status = (BO.OrderStatus)1;
                    }
                    else if (item.ShipDate >= DateTime.Now && item.DeliveryDate <= DateTime.Now)
                    {
                        order.Status = (BO.OrderStatus)2;
                    }
                    else
                    {
                        order.Status = (BO.OrderStatus)3;
                    }
                    int amount = 0;
                    double price = 0;
                    double totalPrice = 0;
                    foreach (var itemIn in dalEntity.OrderItem.Read())
                    {
                        BO.OrderItem orderItem = new BO.OrderItem();
                        if (orderItem.ID == item.ID)
                        {
                            amount = orderItem.Amount;
                            order.AmountOfItems = amount;
                            price = itemIn.Price * amount;
                            totalPrice += price;
                            amount++;
                        }
                    }
                    order.AmountOfItems = amount;
                    order.TotalPrice = totalPrice;
                    OrdersList.Add(order);
                }
            }
            catch(DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }

            return OrdersList;
        }
        public BO.Order ReadOrderInformation(int id)
        {
            if (id > 0)
            {
                DO.Order singleOrder = dalEntity.Order.ReadSingle(id);
                BO.Order order = new BO.Order();
                singleOrder.ID = order.ID;
                singleOrder.CustomerName = order.CustomerName;
                singleOrder.CustomerAddress = order.CustomerAddress;
                singleOrder.CustomerEmail = order.CustomerEmail;
                singleOrder.OrderDate = order.OrderDate;
                singleOrder.ShipDate = order.ShipDate;
                singleOrder.DeliveryDate = order.DeliveryDate;
            }
            foreach (var item in dalEntity.OrderItem.ReadByOrderId(id)) 
            {
                BO.OrderItem itemInformation = new BO.OrderItem();
                itemInformation.ID = item.OrderID;
                itemInformation.Name= dalEntity.Product.ReadSingle(itemInformation.ProductID).Name;    
                itemInformation.ProductID = item.ProductID;
                itemInformation.Price = item.Price;
                itemInformation.Amount = item.Amount;
                itemInformation.TotalPrice= itemInformation.Amount* itemInformation.Price;
            }

            // ○	תזרוק חריגה מתאימה משלה בקשת מוצר נכשלה (מוצר לא קיים בשכבת נתונים - תפיסת חריגה)
        }
        public IOrder UpdateShipping(int orderNumber);
        public IOrder UpdateDelivery(int orderNumber);
        public void Update(int orderNumber);
    }
}
