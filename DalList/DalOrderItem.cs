
using DO;
namespace DalList;
using DalApi;

public struct DalOrderItem:IOrderItem
{
    static public int size = DataSource.orderItemsData.Count();
    public void Create(OrderItem orderItem)
    {
        OrderItem findOrder=DataSource.orderItemsData.Find(item => item.OrderID == orderItem.OrderID && item.ProductID == orderItem.ProductID);
        if (!(findOrder.Equals(default(OrderItem)))) throw new AlreadyExistsException();
        if (DataSource.orderItemsData.Count() == 200) throw new StackOverFlowException();
        DataSource.orderItemsData.Add(orderItem);
           
    }
    public OrderItem ReadSingleByOrderIdAndProductId(int orderId, int productId)
    {
        if(DataSource.orderItemsData.Count()==0) throw new DataIsEmpty();
        OrderItem findOrder= DataSource.orderItemsData.Find(item => item.OrderID == orderId && item.ProductID == productId);
        if (findOrder.Equals(default(OrderItem))) throw new NotFoundException();
        else return findOrder;
  
    }

    public OrderItem ReadSingle(int id)
    {
        if (DataSource.orderItemsData.Count() == 0) throw new DataIsEmpty();
        OrderItem findOrder = DataSource.orderItemsData.Find(item => item.OrderID == id);
        if (findOrder.Equals(default(OrderItem))) throw new NotFoundException();
        else return findOrder;
    }

    public IEnumerable<OrderItem> Read(Func<OrderItem, bool> func = null)
    {
        /*if (DataSource.orderItemsData.Count() != 0)
            return DataSource.orderItemsData;
        else throw new DataIsEmpty();*/
        return (func == null ? DataSource.orderItemsData : DataSource.orderItemsData.Where(func).ToList());
    }

    public IEnumerable<OrderItem> ReadByOrderId(int orderId)
    {
        
        List<OrderItem> items = new ();
        foreach (OrderItem item in DataSource.orderItemsData)
        {
            if (item.OrderID == orderId)
            {
                items.Add(item);
            }
        }
        if(items.Count > 0) return items;
        throw new NotFoundException();
    }
    public void Delete(int id)
    {
        OrderItem item=DataSource.orderItemsData.Find(item => item.ID == id);
        if (item.Equals(default(OrderItem))) throw new NotFoundException();

         DataSource.orderItemsData.Remove(item);
         return;   
    }


    public void Update(OrderItem orderItem)
    {
        
        int idx = -1;
        for (int i = 0; i < DataSource.orderItemsData.Count(); i++)
        {
            if (DataSource.orderItemsData[i].OrderID == orderItem.OrderID && DataSource.orderItemsData[i].ProductID == orderItem.ProductID)
                idx = i;
        }
        if (idx != -1)
        {
            DataSource.orderItemsData[idx] = orderItem;
        }
        else throw new NotFoundException();
    }

}
