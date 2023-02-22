using BlApi;
using BO;
using DalApi;
using DO;
using System.Reflection;
using System.Xml.Serialization;
using IOrder = BlApi.IOrder;

namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        DalApi.IDal? dalEntity = DalApi.Factory.Get();

        private DO.Order convertBOtoDOFunc(BO.Order bOrder)
        {
            object obDo = new DO.Order();
            obDo.GetType().GetProperties().Select(p =>
            {
                p.SetValue(obDo, bOrder.GetType().GetProperty(p.Name)?.GetValue(bOrder));
                return p;
            }).ToList();
            return (DO.Order)obDo;
        }

        private BO.Order convertDOtoBOFunc(DO.Order dOrder, int id)
        {
            BO.Order b_order = new();
            b_order.GetType().GetProperties().Where(prop => (prop.Name != "Status" && prop.Name != "Items" && prop.Name != "TotalPrice")).Select(prop =>
            {
                prop.SetValue(b_order, dOrder.GetType().GetProperty(prop.Name)?.GetValue(dOrder));
                return prop;
            }).ToList();
            (List<BO.OrderItem> ls, double totalPrice) = getItemsfunc(id);
            b_order.Items = ls;
            b_order.TotalPrice = totalPrice;
            if (b_order.ShipDate <= DateTime.Now)
                b_order.Status = BO.OrderStatus.Dispatched;
            else
                b_order.Status = BO.OrderStatus.Shipped;
            if (b_order.DeliveryDate <= DateTime.Now)
                b_order.Status = BO.OrderStatus.Delivered;
            return (BO.Order)b_order;
        }

        public (List<BO.OrderItem>, double) convertingDOProductToBOProductForList(List<DO.OrderItem> orderItems)
        {
            List<BO.OrderItem> b_orderItems =
                orderItems.Select(oi => new BO.OrderItem
                {
                    ID = oi.ID,
                    Name = dalEntity.Product.ReadSingle(oi.ID).Name,
                    ProductID = oi.ProductID,
                    Price = oi.Price,
                    Amount = oi.Amount,
                    TotalPrice = oi.Amount * oi.Price,
                }).ToList();
            double totalPrice = 0;
            b_orderItems.Sum(oi => totalPrice += oi.TotalPrice);
            return (b_orderItems.ToList(), totalPrice);
        }
        (List<BO.OrderItem>, double) getItemsfunc(int id)
        {
            List<DO.OrderItem> orderItem = dalEntity.OrderItem.Read(ord => ord.OrderID == id).ToList();
            List<BO.OrderItem> items = new();
            return convertingDOProductToBOProductForList(orderItem);
            /// <summary>
            /// the function requesting the orders list
            /// and Build an order list of the OrderForList
            /// </summary>
            /// <returns> return list of all the orders</returns>
            /// <exception cref="BO.NotDataException"> if the orders return null</exception>
            /// 
        }
        public IEnumerable<BO.OrderForList> ReadOrders()
        {
            List<BO.OrderForList> OrdersList = new List<BO.OrderForList>();
            try
            {
                List<DO.Order> orders = dalEntity.Order.Read().ToList();
                var ordersList =
                    from order in orders
                    let boOrder = dalEntity.OrderItem.Read(ord => ord.OrderID == order.ID).ToList()
                    select new BO.OrderForList()
                    {
                        ID = order.ID,
                        CustomerName = order.CustomerName,
                        AmountOfItems = boOrder == null ? 0 : boOrder.Count,
                        TotalPrice = boOrder == null ? 0 : boOrder.Sum(ord => ord.Price * ord.Amount),
                        Status = (order.DeliveryDate < DateTime.Now) ? BO.OrderStatus.Delivered :
                        (order.ShipDate < DateTime.Now) ? BO.OrderStatus.Shipped : BO.OrderStatus.Dispatched,
                    };
                return ordersList;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
            //    foreach (var item in orders)
            //    {
            //        BO.OrderForList order = new();
            //        order.ID = item.ID;
            //        order.CustomerName = item.CustomerName;
            //        if (item.OrderDate <= DateTime.Now && item.ShipDate <= DateTime.Now)
            //        {
            //            order.Status = (BO.OrderStatus)1;
            //        }
            //        else if (item.ShipDate <= DateTime.Now && item.DeliveryDate <= DateTime.Now)
            //        {
            //            order.Status = (BO.OrderStatus)2;
            //        }
            //        else
            //        {
            //            order.Status = (BO.OrderStatus)3;
            //        }
            //        int amount = 0;
            //        double totalPrice = 0;
            //        List<DO.OrderItem> items = dalEntity.OrderItem.ReadByOrderId(item.ID).ToList();
            //        foreach (var itemIn in items)
            //        {
            //            amount += itemIn.Amount;
            //            totalPrice += itemIn.Price * itemIn.Amount;
            //        }
            //        order.AmountOfItems = amount;
            //        order.TotalPrice = totalPrice;
            //        OrdersList.Add(order);

            //    }
            //}
            //catch (DataIsEmpty exc)
            //{
            //    throw new BO.NotDataException(exc);
            //}
            //return OrdersList;
        }

        /// <summary>
        /// the function requesting Order details, get the order Id
        /// If the ID is a positive number , request a data layer order
        /// request order items from a data layer and Build an order object
        /// based on the received data and calculate missing information
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return a constructed order object </returns>
        /// <exception cref="BO.NotExistException"> if itsnt found the order by order Id</exception>
        /// <exception cref="BO.NotDataException"> if the data is empty</exception>
        public BO.Order ReadOrderInformation(int id)
        {
            BO.Order order = new BO.Order();
            try
            {
                if (id < 0) throw new BO.NotValidException("ID is negative number");
                DO.Order dOrder = dalEntity.Order.ReadSingle(id);
                return convertDOtoBOFunc(dOrder, id);
                //    if (id > 0)
                //    {
                //        DO.Order singleOrder = dalEntity.Order.ReadSingle(id);
                //        order.ID = id;
                //        order.CustomerName = singleOrder.CustomerName;
                //        order.CustomerAddress = singleOrder.CustomerAddress;
                //        order.CustomerEmail = singleOrder.CustomerEmail;
                //        order.OrderDate = singleOrder.OrderDate;
                //        order.ShipDate = singleOrder.ShipDate;
                //        order.DeliveryDate = singleOrder.DeliveryDate;
                //        if (singleOrder.OrderDate <= DateTime.Now)
                //            order.Status = (BO.OrderStatus)1;
                //        else
                //            order.Status = (BO.OrderStatus)2;
                //        if(singleOrder.DeliveryDate <= DateTime.Now)
                //            order.Status = (BO.OrderStatus)3;
                //        order.PaymentDate = DateTime.Now;
                //    }
                //    double total = 0;
                //    List<BO.OrderItem> itemInformation = new List<BO.OrderItem>();
                //    foreach (var i in dalEntity.OrderItem.ReadByOrderId(id))
                //    {
                //        BO.OrderItem item = new BO.OrderItem();
                //        item.ID = i.OrderID;
                //        item.Name = dalEntity.Product.ReadSingle(i.ProductID).Name;
                //        item.ProductID = i.ProductID;
                //        item.Price = i.Price;
                //        item.Amount = i.Amount;
                //        item.TotalPrice = item.Amount * item.Price;
                //        total += item.TotalPrice;
                //        itemInformation.Add(item);
                //    }
                //    order.Items = itemInformation;
                //    order.TotalPrice = total;
                //}
                //catch (NotFoundException exc)
                //{
                //    throw new BO.NotExistException(exc);
                //}
                //catch (DataIsEmpty exc)
                //{
                //    throw new BO.NotDataException(exc);
                //}
                //return order;

            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }

        }
        /// <summary>
        /// the function update the shipping date in the order
        /// Check if an order exists and has not yet been sent
        /// Update the shipping date in the order 
        /// try to update an order for a data layer
        /// </summary>
        /// <param name="orderNumber"> the function get the order number</param>
        /// <returns>return an updated order object of a logical entity</returns>
        /// <exception cref="BO.NotExistException"> if itsnt found the order by order Id</exception>
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
        /// <summary>
        /// the function update the delivery date in the order
        /// Check if an order exists and has not yet been sent
        /// Update the delivery date in the order 
        /// try to update an order for a data layer
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns>return an updated order object of a logical entity</returns>
        /// <exception cref="BO.NotExistException"> if itsnt found the order by order Id</exception>
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
        public int? SelectingOrderForTreatment()
        {
            List<BO.OrderForList> ?OrdersList = new List<BO.OrderForList>();
            try
            {
                List<DO.Order> orders = dalEntity.Order.Read().ToList();
                var ordersList =
                    from order in orders
                    where order.ShipDate == DateTime.MinValue && order.DeliveryDate == DateTime.MinValue
                    orderby order.OrderDate
                    select order;
               return ordersList?.First().ID;
                //    select new BO.OrderForList()
                //    {
                        
                //        Status = (order.DeliveryDate < DateTime.Now) ? BO.OrderStatus.Delivered :
                //        (order.ShipDate < DateTime.Now) ? BO.OrderStatus.Shipped : BO.OrderStatus.Dispatched,
                //    };
                //return ordersList;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
        }
        public BO.OrderTracking OrderTrack(int id)
        {
            try
            {
                //DO.Order currOrder = dalEntity.Order.ReadSingle(x => x.ID == id);
                DO.Order currOrder = dalEntity.Order.ReadSingle(id);
                BO.OrderTracking orderTracking = new();
                orderTracking.ID = currOrder.ID;
                orderTracking.packageStatus.Add(new Tuple<DateTime, BO.OrderStatus>((DateTime)currOrder.OrderDate, BO.OrderStatus.Dispatched));
                orderTracking.Status = BO.OrderStatus.Dispatched;
                if (currOrder.ShipDate <= DateTime.Now)
                {
                    orderTracking.packageStatus?.Add(new Tuple<DateTime, BO.OrderStatus>((DateTime)currOrder.ShipDate, BO.OrderStatus.Shipped));
                    orderTracking.Status = BO.OrderStatus.Shipped;
                }
                if (currOrder.DeliveryDate <= DateTime.Now)
                {
                    orderTracking.packageStatus?.Add(new Tuple<DateTime, BO.OrderStatus>((DateTime)currOrder.DeliveryDate, BO.OrderStatus.Delivered));
                    orderTracking.Status = BO.OrderStatus.Delivered;
                }
                return orderTracking;
            }
            catch (NotFoundException err)
            {
                throw new BO.NotExistException(err);
            }
        }

        public object UpdateShipping(int? orderId)
        {
            throw new NotImplementedException();
        }
    }
}
