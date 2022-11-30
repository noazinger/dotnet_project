
using BlApi;
using DalApi;
using IOrder = BlApi.IOrder;

namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        IDal dalEntity = new Dal.DalList();
        public IEnumerable<BO.OrderForList> ReadOrders()
        {
            List<BO.OrderForList> OrdersList = new List<BO.OrderForList>();
            try
            {
                List<DO.Order> orders = dalEntity.Order.Read().ToList();
                foreach (var item in orders)
                {
                    BO.OrderForList order = new ();
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
                    double totalPrice = 0;
                    List<DO.OrderItem> items = dalEntity.OrderItem.ReadByOrderId(item.ID).ToList();
                    foreach (var itemIn in items)
                    {
                        amount +=itemIn.Amount;
                        totalPrice += itemIn.Price* itemIn.Amount;
                  
                    }
                   
                    order.AmountOfItems = amount;
                    order.TotalPrice = totalPrice;
                    OrdersList.Add(order);

                }
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
            return OrdersList;
        }
        public BO.Order ReadOrderInformation(int id)
        {
            BO.Order order = new BO.Order();
            try
            {
                if (id > 0)
                {
                    DO.Order singleOrder = dalEntity.Order.ReadSingle(id);
                    order.ID = id;
                    order.CustomerName = singleOrder.CustomerName;
                    order.CustomerAddress = singleOrder.CustomerAddress;
                    order.CustomerEmail = singleOrder.CustomerEmail;
                    order.OrderDate = singleOrder.OrderDate;
                    order.ShipDate = singleOrder.ShipDate;
                    order.DeliveryDate = singleOrder.DeliveryDate;
                }
                List<BO.OrderItem> itemInformation = new List<BO.OrderItem>();
                foreach (var i in dalEntity.OrderItem.ReadByOrderId(id))
                {
                    BO.OrderItem item = new BO.OrderItem();
                    item.ID = i.OrderID;
                    item.Name = dalEntity.Product.ReadSingle(i.ProductID).Name;
                    item.ProductID = i.ProductID;
                    item.Price = i.Price;
                    item.Amount = i.Amount;
                    item.TotalPrice = item.Amount * item.Price;
                    itemInformation.Add(item);
                }
                order.Items = itemInformation;
            }
            catch (NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }

            return order;
        }
        public BO.Order UpdateShipping(int orderNumber)
        {
            try
            {
                DO.Order orderDo = dalEntity.Order.ReadSingle(orderNumber);
                orderDo.ShipDate = DateTime.Now;
                dalEntity.Order.Update(orderDo);
                BO.Order orderBo = new BO.Order();
                orderBo.CustomerAddress = orderDo.CustomerAddress;
                orderBo.CustomerName = orderDo.CustomerName;
                orderBo.CustomerEmail = orderDo.CustomerEmail;
                orderBo.ID = orderDo.ID;
                orderBo.ShipDate = orderDo.ShipDate;
                orderBo.OrderDate = orderDo.OrderDate;
                orderBo.DeliveryDate = orderDo.DeliveryDate;
                return orderBo;
            }
            catch (NotFoundException err)
            {
                throw new BO.NotExistException(err);
            }
        }
        public BO.Order UpdateDelivery(int orderNumber)
        {
            try
            {
                DO.Order orderDo = dalEntity.Order.ReadSingle(orderNumber);
                orderDo.DeliveryDate = DateTime.Now;
                dalEntity.Order.Update(orderDo);
                BO.Order orderBo = new BO.Order();
                orderBo.CustomerAddress = orderDo.CustomerAddress;
                orderBo.CustomerName = orderDo.CustomerName;
                orderBo.CustomerEmail = orderDo.CustomerEmail;
                orderBo.ID = orderDo.ID;
                orderBo.ShipDate = orderDo.ShipDate;
                orderBo.OrderDate = orderDo.OrderDate;
                orderBo.DeliveryDate = orderDo.DeliveryDate;

                return orderBo;
            }
            catch (NotFoundException err)
            {
                throw new BO.NotExistException(err);
            }
        }
        public void Update(int orderNumber) { }
    }
}
