using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlProduct:Iproduct
    {
        IDal dalEntity = new Dal.DalList();
        public IEnumerable<Iproduct> ReadListProducts()
        {
           
            foreach(DO.Product item in dalEntity.Product.Read())
            {
                BO.Product(item.ID, item.Name, item.inStock, item.catagory, item.Price);
                BO.ProductForList.
            }
        }
        public Iproduct ReadCatalog();
        public Iproduct ReadSingleProductForDirector(int id);
        public Iproduct ReadSingleProductForCustomer(int id);
        public void Create(Iproduct product);
        public void Delete(int id);
        public void Update(Iproduct product);
    }
}
