using DalApi;
using DO;


namespace DalList;
using DalApi;
internal class DalOrder : IOrder
{
    public static int size = DataSource.ordersData.Count();
    public void Create(Order obj) { }
    public int Add(Order obj)
    {
        if (size > 99)
            throw new StackOverFlowException();
        obj.ID = DataSource.Config.OrderID;
        DataSource.ordersData.Add(obj);
        return obj.ID;
    }
    public Order ReadSingle(int id)
    {

        Order order = DataSource.ordersData.Find(i => i.ID == id);
        if (!order.Equals(default(Order))) return order;
        throw new NotFoundException();

    }

    public IEnumerable<Order> Read(Func<Order, bool> func = null)
    {
        /*if (size != 0)
            return DataSource.ordersData;
        throw new DataIsEmpty();*/
        return (func == null ? DataSource.ordersData : DataSource.ordersData.Where(func).ToList());
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
    public void Update(Order obj)
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





