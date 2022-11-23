using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Iproduct<T>
    {
        public IEnumerable<T> ReadListProducts();
        public T ReadCatalog();
        public T ReadSingleProductForDirector(int id);
        public T ReadSingleProductForCustomer(int id);
        public void Create(T product);
        public void Delete(int id);
        public void Update(T product);
    }
}
