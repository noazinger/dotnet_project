using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface Iproduct
    {
        public IEnumerable<Iproduct> ReadListProducts();
        public Iproduct ReadCatalog();
        public Iproduct ReadSingleProductForDirector(int id);
        public Iproduct ReadSingleProductForCustomer(int id);
        public void Create(Iproduct product);
        public void Delete(int id);
        public void Update(Iproduct product);
    }
}
