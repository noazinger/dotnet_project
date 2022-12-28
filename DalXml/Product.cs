using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
    internal class Product: IProduct
    {
    void Create(T n);
    void Update(T n);
    IEnumerable<T?> Read(Func<T, bool> func = null);
    T ReadSingle(int id);
    void Delete(int id);
}

