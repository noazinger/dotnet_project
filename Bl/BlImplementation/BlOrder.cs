
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
            List <BO.OrderForList> OrdersList = new List<BO.OrderForList>();
            try
            {
                List<DO.Order> orders = dalEntity.Order.Read().ToList();
                foreach (var item in orders )
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
                    List<DO.OrderItem> items = dalEntity.OrderItem.Read().ToList();
                    foreach (var itemIn in items )
                    {
                        //BO.OrderItem orderItem = new BO.OrderItem();
                        if (itemIn.ID == item.ID) {
                            amount++;
                        }
                        if (itemIn.OrderID == item.ID)
                        {
                            //amount = orderItem.Amount;
                            //order.AmountOfItems = amount;
                            amount = itemIn.Amount;
                            price = (double)(itemIn.Price * amount);
                            //totalPrice += price;
                            totalPrice = price;
                        }
                    }
                    order.AmountOfItems = amount;
                    order.TotalPrice = totalPrice;
                    OrdersList.Add(order);
                    amount++;
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
            BO.Order order = new BO.Order();
            try
            {
                if (id > 0)
                {
                    DO.Order singleOrder = dalEntity.Order.ReadSingle(id);
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
                    itemInformation.Name = dalEntity.Product.ReadSingle(itemInformation.ProductID).Name;
                    itemInformation.ProductID = item.ProductID;
                    itemInformation.Price = item.Price;
                    itemInformation.Amount = item.Amount;
                    itemInformation.TotalPrice = itemInformation.Amount * itemInformation.Price;
                }
            }
            catch(NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }
            catch(DataIsEmpty exc)
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
                orderBo.CustomerAddress= orderDo.CustomerAddress;
                orderBo.CustomerName= orderDo.CustomerName;
                orderBo.CustomerEmail = orderDo.CustomerEmail;
                orderBo.ID=orderDo.ID;
                orderBo.ShipDate= orderDo.ShipDate;
                orderBo.OrderDate= orderDo.OrderDate;
                orderBo.DeliveryDate= orderDo.DeliveryDate;
                return orderBo;
            }
            catch (NotFoundException err)
            {
                throw new BO.NotExistException(err) ;
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
