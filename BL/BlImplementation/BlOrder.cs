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

        //private DO.Order convertBOtoDOFunc(BO.Order bOrder)
        //{
        //    object obDo = new DO.Order();
        //    obDo.GetType().GetProperties().Select(p =>
        //    {
        //        p.SetValue(obDo, bOrder.GetType().GetProperty(p.Name)?.GetValue(bOrder));
        //        return p;
        //    }).ToList();
        //    return (DO.Order)obDo;
        //}
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
            if (b_order.ShipDate == DateTime.MinValue)
                b_order.Status = BO.OrderStatus.Dispatched;
            else if (b_order.DeliveryDate == DateTime.MinValue)
                b_order.Status = BO.OrderStatus.Shipped;
            else
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
                        Status = (order.ShipDate == DateTime.MinValue) ? BO.OrderStatus.Dispatched :
                        (order.DeliveryDate == DateTime.MinValue) ? BO.OrderStatus.Shipped : BO.OrderStatus.Delivered,
                    };
                return ordersList;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
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
                if (orderBo.ShipDate == DateTime.MinValue)
                    orderBo.Status = BO.OrderStatus.Dispatched;
                else if (orderBo.DeliveryDate == DateTime.MinValue)
                    orderBo.Status = BO.OrderStatus.Shipped;
                else
                    orderBo.Status = BO.OrderStatus.Delivered;
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
                if (orderBo.ShipDate == DateTime.MinValue)
                    orderBo.Status = BO.OrderStatus.Dispatched;
                else if (orderBo.DeliveryDate == DateTime.MinValue)
                    orderBo.Status = BO.OrderStatus.Shipped;
                else
                    orderBo.Status = BO.OrderStatus.Delivered;
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
            List<BO.OrderForList>? OrdersList = new List<BO.OrderForList>();
            try
            {
                List<DO.Order> orders = dalEntity.Order.Read().ToList();
                var ordersList=(
                    from order in orders
                    where order.ShipDate == DateTime.MinValue || order.DeliveryDate == DateTime.MinValue
                    orderby order.OrderDate
                    select order).ToList();
                if (ordersList.Count==0) return null;
                else return ordersList?.First().ID;
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
