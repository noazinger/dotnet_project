
using DO;
namespace DalList;
using DalApi;

public struct DalOrderItem:IOrderItem
{
    static public int size = DataSource.orderItemsData.Count();
    public void Create(OrderItem orderItem)
    {
        OrderItem findOrder=DataSource.orderItemsData.Find(item => item.OrderID == orderItem.OrderID && item.ProductID == orderItem.ProductID);
        if (findOrder.Equals(default(OrderItem))) throw new AlreadyExistsException();
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

    public IEnumerable<OrderItem> Read()
    {
        if (DataSource.orderItemsData.Count() != 0)
            return DataSource.orderItemsData;
        else throw new DataIsEmpty();

    }

    public IEnumerable<OrderItem> ReadByOrderId(int orderId)
    {
        
        OrderItem[] itemsArr = new OrderItem[DataSource.orderItemsData.Count()];
        int idx = 0;
        for (int i = 0; i < DataSource.orderItemsData.Count(); i++)
        {
            if (DataSource.orderItemsData[i].OrderID == orderId)
            {
                itemsArr[idx] = DataSource.orderItemsData[i];
                idx++;
            }
        }
        if (idx == 0)
        {
            throw new NotFoundException();
        }
        return itemsArr;
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
