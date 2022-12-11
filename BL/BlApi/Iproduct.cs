using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Iproduct
    {
        public IEnumerable<BO.ProductForList?> ReadListProducts();
        public IEnumerable<BO.ProductItem?> ReadCatalog();
        public IEnumerable<BO.ProductForList> ReadProductByCategoty(BO.catagory category);
        public BO.Product ReadSingleProductForDirector(int id);
        public BO.Product ReadSingleProductForCustomer(int id);
        public void Add(BO.Product product);
        public void Delete(int id);
        public void Update(BO.Product product);
    }
}
