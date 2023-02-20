using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class BlProduct : Iproduct
    {
        DalApi.IDal? dalEntity = DalApi.Factory.Get();
        public IEnumerable<BO.ProductForList> ReadListProducts()
        {
            try
            {
                List<DO.Product> products = dalEntity.Product.Read().ToList();
                var productsList =
                    from pr in products
                    select new BO.ProductForList
                    {
                        ID = pr.ID,
                        Name = pr.Name,
                        Price = pr.Price,
                        catagory = (BO.catagory)pr.catagory
                    };
                   
                return productsList;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
        }
        public IEnumerable<BO.ProductItem> ReadCatalog()
        {
            try
            {
                List<DO.Product> products = dalEntity.Product.Read().ToList();
                var productsItem =
                    from pr in products
                    select new BO.ProductItem
                    {
                        ID = pr.ID,
                        Name = pr.Name,
                        Price = pr.Price,
                        catagory = (BO.catagory)pr.catagory,
                        Amount = pr.inStock,
                        InStock = pr.inStock > 0 ? true : false
                    };

                return productsItem;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }

        }
        
        public IEnumerable<BO.ProductItem> ReadProductByCategoty(BO.catagory category)
        {
            try
            {
                IEnumerable<DO.Product> lst = dalEntity.Product.Read(p => p.catagory == (DO.catagory)category);
                List<BO.ProductItem> productCatagory = (from pc in lst
                                      select new BO.ProductItem
                                      {
                                          ID = pc.ID,
                                          Name = pc.Name,
                                          Price = pc.Price,
                                          catagory = (BO.catagory)pc.catagory,
                                          Amount = pc.inStock,
                                          InStock = pc.inStock > 0 ? true : false
                                      }).ToList();
                return productCatagory;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
        }
        public BO.Product ReadSingleProductForDirector(int id)
        {
            if (id > 0)
            {
                try
                {
                    DO.Product item = dalEntity.Product.ReadSingle(id);
                    BO.Product product = new BO.Product();
                    product.ID = item.ID;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.catagory = (BO.catagory)item.catagory;
                    product.InStock = item.inStock;
                    return product;
                }
                catch (NotFoundException exc)
                {
                    throw new BO.NotExistException(exc);
                }
                catch (NotValidException exc)
                {
                    throw new BO.NotValidException(exc);
                }
            }
            else throw new BO.NotValidException("the ID is not valid");
        }
        public BO.Product ReadSingleProductForCustomer(int id)
        {
            if (id > 0)
            {
                try
                {
                    DO.Product item = dalEntity.Product.ReadSingle(id);
                    BO.Product product = new BO.Product();
                    product.ID = item.ID;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.catagory = (BO.catagory)item.catagory;
                    return product;
                }
                catch (NotFoundException exc)
                {
                    throw new BO.NotExistException(exc);
                }
                catch (NotValidException exc)
                {
                    throw new BO.NotValidException(exc);
                }
            }
            else throw new BO.NotValidException("the ID is not valid");

        }
        public void Add(BO.Product product)
        {
            if (product.ID < 0) throw new BO.NotValidException("the ID is not valid");
            if (product.Name == "") throw new BO.NotValidException("the name is not valid");
            if (product.Price < 0) throw new BO.NotValidException("the price is not valid");
            if (product.InStock < 0) throw new BO.NotValidException("the in stock amount is not valid");
            DO.Product tempProduct = new DO.Product();
            tempProduct.ID = product.ID;
            tempProduct.Name = product.Name;
            tempProduct.Price = product.Price;
            tempProduct.catagory = (DO.catagory)product.catagory;
            tempProduct.inStock = product.InStock;
            try
            {
                dalEntity?.Product.Create(tempProduct);

            }
            catch (AlreadyExistsException exc)
            {
                throw new BO.AlreadyExistsException(exc);
            }

        }
        public void Delete(int id)
        {
            List<DO.OrderItem>? orderItems = dalEntity?.OrderItem?.Read().ToList();
            DO.OrderItem orderItem = orderItems.Find(order => order.ID == id);
            if (!orderItem.Equals(default(DO.OrderItem)))
            {
                DO.Order order = dalEntity.Order.ReadSingle(orderItem.OrderID);
                if (order.ShipDate > DateTime.Now)
                {
                    throw new BO.ExistsInOrder();
                }
            }
            try
            {
                dalEntity?.Product.Delete(id);
            }
            catch (NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }
        }
        public void Update(BO.Product product)
        {
            if (product.ID < 0) throw new BO.NotValidException("the ID is not valid");
            if (product.Name == "") throw new BO.NotValidException("the name is not valid");
            if (product.Price < 0) throw new BO.NotValidException("the price is not valid");
            if (product.InStock < 0) throw new BO.NotValidException("the in stock amount is not valid");
            DO.Product tempProduct = new DO.Product();
            tempProduct.ID = product.ID;
            tempProduct.Name = product.Name;
            tempProduct.Price = product.Price;
            tempProduct.catagory = (DO.catagory?)product.catagory;
            tempProduct.inStock = product.InStock;
            try
            {
                dalEntity.Product.Update(tempProduct);
            }
            catch (NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }
        }
    }
}
