
using DO;
namespace DalList;
using DalApi;

public struct DalOrderItem:IOrderItem
{
    static public int size = DataSource.orderItemsData.Count();
    public void Create(OrderItem orderItem)
    {
        bool alreadyExist = false;
        for (int i = 0; i < DataSource.orderItemsData.Count(); i++)
        {
            if (DataSource.orderItemsData[i].OrderID == orderItem.OrderID && DataSource.orderItemsData[i].ProductID == orderItem.ProductID)
            {
                alreadyExist = true;
                break;
            }
        }
        if (!alreadyExist)
        {
            if (DataSource.orderItemsData.Count() == 200)
                throw new StackOverFlowException();
            DataSource.orderItemsData.Add((OrderItem)orderItem);
        }
        else
            throw new AlreadyExistsException();
    }
    public OrderItem ReadSingleByOrderIdAndProductId(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.orderItemsData.Count(); i++)
        {
            if (DataSource.orderItemsData[i].OrderID == orderId && DataSource.orderItemsData[i].ProductID == productId)
                return DataSource.orderItemsData[i];
        }
        throw new DataIsEmpty();
    }

    public OrderItem ReadSingle(int id)
    {
        for (int i = 0; i < DataSource.orderItemsData.Count(); i++)
        {
            if (DataSource.orderItemsData[i].OrderID == id)
                return DataSource.orderItemsData[i];
        }
        throw new NotFoundException();
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
            throw new DataIsEmpty();
            /*            throw new ObjectIsNotExist();
            */
        }
        return itemsArr;
    }
    public void Delete(int id)
    {
        OrderItem item=DataSource.orderItemsData.Find(item => item.ID == id);
        if (item.ID!=null)
        {
            DataSource.orderItemsData.Remove(item);
            return;
        } 
        throw new NotFoundException();
     
        
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
