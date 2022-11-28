using DalList;
using DalApi;
using DO;
namespace DalList;
using DalApi;

public struct DalProduct:IProduct
{
    static public int size = DataSource.productsData.Count();
    public  void Create(Product obj)
    {
        if (DataSource.Config.ProductIndex > 49) throw new StackOverFlowException();
        obj.ID = DataSource.Config.ProductID;
        Product product=DataSource.productsData.Find(p => p.ID == obj.ID);
        if(!product.Equals(default(Product))) throw new AlreadyExistsException();   
        DataSource.productsData.Add(obj);
    }
    public  Product ReadSingle(int Id)
    {
        if (Id < 10000 || Id > 99999)
        {
            throw new NotValidException();
        }
        for(int i = 0; i < size; i++)
        {
            if (DataSource.productsData[i].ID == Id)
                return DataSource.productsData[i];
        }
        throw new NotFoundException();
    }

    public IEnumerable<Product> Read()
    {
        if (size != 0)
            return DataSource.productsData;
        throw new DataIsEmpty();

    }

    public  void Delete(int Id)
    {
        for (int i = 0; i < size; i++)
        {
            if(DataSource.productsData[i].ID == Id)
            {
                DataSource.productsData.Remove(DataSource.productsData[i]);
                return;
            }
        }
        throw new NotFoundException();
    }

    public  void Update(Product obj)
    {
        for (int i = 0; i < size; i++)
        {
            if (DataSource.productsData[i].ID == obj.ID)
            {
                DataSource.productsData[i] = obj;
                return;
            }
        }
        throw new NotFoundException();
    }
}
