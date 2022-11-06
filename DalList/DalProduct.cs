using DalList;
using DO;
namespace DalList;

public struct DalProduct
{
    public static void Create(Product obj)
    {
        if (DataSource.Config.ProductIndex > 49)
        {
            throw new Exception("There isn't enough space");
        }
        DataSource.productsData[DataSource.Config.ProductIndex++] = obj;
    }
    public static Product ReadSingle(int Id)
    {
        if (Id < 100000 || Id > 999999)
        {
            throw new Exception("ID isn't valid");
        }
        for(int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.productsData[i].ID == Id)
                return DataSource.productsData[i];
        }
        throw new Exception("The product isn't exist");
    }

    public static Product[] Read()
    {
        Product[] tempProducts = new Product[DataSource.Config.ProductIndex];
        for(int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            tempProducts[i] = DataSource.productsData[i];
        }
        return tempProducts;
    }

    public static void Delete(int Id)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if(DataSource.productsData[i].ID == Id)
            {
                Product temp=DataSource.productsData[i];
                DataSource.productsData[i]= DataSource.productsData[DataSource.Config.ProductIndex];
                DataSource.productsData[DataSource.Config.ProductIndex]= temp;
                DataSource.Config.ProductIndex--;
                return;
            }
        }
        throw new Exception("Product isn't exist");
    }

    public static void UpDate(Product obj)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.productsData[i].ID == obj.ID)
            {
                DataSource.productsData[i] = obj;
                return;
            }
        }
        throw new Exception("Product isn't exist");
    }
}
