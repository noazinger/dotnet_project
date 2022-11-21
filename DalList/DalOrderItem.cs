using DalApi;
using DalList;
using DO;
namespace DalList;
using DalApi;

public struct DalOrderItem:IOrderItem
{
    static public int size = DataSource.orderItemsData.Count();
    public void Create(OrderItem obj)
    {
        if (size > 200)
            throw new StackOverFlowException();
        DataSource.orderItemsData.Add(obj);
    }

    public  OrderItem ReadSingle(int Id)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.orderItemsData[i].ProductID == Id)
                return DataSource.orderItemsData[i];
        }
        throw new NotFoundException();
    }



  
        public IEnumerable<OrderItem> Read()
        {
            if (size != 0)
                return DataSource.orderItemsData;
            throw new DataIsEmpty();

        }
    
    public  void Delete(int Id)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.orderItemsData[i].ProductID == Id)
            {
                DataSource.orderItemsData.Remove(DataSource.orderItemsData[i]);
                return;
            }
        }
        throw new NotFoundException();
    }

    public  void Update(OrderItem obj)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.orderItemsData[i].ProductID== obj.ProductID)
            {
                DataSource.orderItemsData[i] = obj;
                return;
            }
        }
        throw new NotFoundException();
    }
}
