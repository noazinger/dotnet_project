using DalApi;
using DO;


namespace DalList;
using DalApi;
internal class  DalOrder: IOrder
{
    public static int size = DataSource.ordersData.Count();
    public  int Create(Order obj)
    {
        if (size > 99)
            throw new StackOverFlowException();
         obj.ID= DataSource.Config.ID;
         DataSource.ordersData.Add(obj);
        return DataSource.Config.ID;
    }

    public  Order ReadSingle(int id)
    {
        for(int i=0;i< size; i++)
        {
           if (DataSource.ordersData[i].ID == id) return DataSource.ordersData[i];
        }
        throw new NotFoundException();

    }

    public IEnumerable<Order>Read()
    {
        if (size != 0)
            return DataSource.ordersData;
        throw new DataIsEmpty();
    }
    public void Delete(int id)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.ordersData[i].ID == id)
            {
                DataSource.ordersData.Remove(DataSource.ordersData[i]);
                return;
            }

        }
        throw new NotFoundException();
    }
    public  void Update(Order obj)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.ordersData[i].ID == obj.ID)
            {
                DataSource.ordersData[i] = obj;
                return;
            }

        }
        throw new NotFoundException();
    }



}





