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
        public IEnumerable<BO.ProductForList> ReadListProducts()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();

            foreach (DO.Product item in dalEntity.Product.Read())
            {
                BO.ProductForList product = new BO.ProductForList();
                product.ID = item.ID;
                product.Name = item.Name;
                product.Price = item.Price;
                product.catagory= (BO.catagory)item.catagory;
                products.Add(product);
            }
            if(!products.Any())return products;
            else
            {
                throw new BO.NotExistException();
            }
        }
        public Iproduct ReadCatalog()
        {


        }
        public BO.Product ReadSingleProductForDirector(int id)
        {
            if (id > 0)
            {
                DO.Product item = dalEntity.Product.ReadSingle(id);
                catch (NotFoundException exc){
                    throw new BO.NotExistException(exc);
                }
                foreach ( in dalEntity.Product.Read())
                {
                    if (item.ID==id)
                    {
                        BO.Product product = new BO.Product();
                        product.ID = item.ID;
                        product.Name = item.Name;
                        product.Price = item.Price;
                        product.catagory = (BO.catagory)item.catagory;
                        return product;
                    }

                }
                throw new BO.NotExistException();
            }
            else
            {
                throw new BO.NotValidException();
            }

        }
        public Iproduct ReadSingleProductForCustomer(int id);
        public void Create(Iproduct product);
        public void Delete(int id);
        public void Update(Iproduct product);
    }
}
