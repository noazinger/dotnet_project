using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlProduct : Iproduct
    {
        IDal dalEntity = new Dal.DalList();
        /// <summary>
        /// function requesting the product list
        /// Build on the database a list of products of the ProductForList
        /// </summary>
        /// <returns> return the constructed list</returns>
        /// <exception cref="BO.NotDataException"> if the products data is empty</exception>
        public IEnumerable<BO.ProductForList> ReadListProducts()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            try
            {
                foreach (DO.Product item in dalEntity.Product.Read())
                {
                    BO.ProductForList product = new BO.ProductForList();
                    product.ID = item.ID;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.catagory = (BO.catagory)item.catagory;
                    products.Add(product);
                }
                return products;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
        }
        /// <summary>
        /// the function requesing the catalog list
        /// Request a list of products from the data layer
        /// Build on the database a list of products of the ProductItem type
        /// </summary>
        /// <returns> return the constructed list</returns>
        /// <exception cref="BO.NotDataException"> if the products data is empty</exception>
        public IEnumerable<BO.ProductItem> ReadCatalog()
        {
            List<BO.ProductItem> products = new List<BO.ProductItem>();
            try
            {
                foreach (DO.Product item in dalEntity.Product.Read())
                {
                    BO.ProductItem product = new BO.ProductItem();
                    product.ID = item.ID;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.catagory = (BO.catagory)item.catagory;
                    product.Amount = item.inStock;
                    product.InStock = item.inStock > 0 ? true : false;
                    products.Add(product);
                }
                return products;
            }
            catch (DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
        }
        /// <summary>
        /// the function requesting Product details For Director, get product ID
        /// If the ID is a positive number - try to request a product from the data layer
        /// Build a product object based on the received data and calculate missing information
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return a constructed Product object</returns>
        /// <exception cref="BO.NotExistException"> if the item isnt exist in thr data</exception>
        /// <exception cref="BO.NotValidException"> id Id is negatice or 0</exception>
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
        /// <summary>
        /// the function requesting Product details For Customer, get product ID
        /// If the ID is a positive number - try to request a product from the data layer
        /// Build a product object based on the received data and calculate missing information
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return a constructed Product object</returns>
        /// <exception cref="BO.NotExistException"> if the item isnt exist in thr data</exception>
        /// <exception cref="BO.NotValidException"> id Id is negatice or 0</exception>
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
        /// <summary>
        /// the function adding a product, get product object
        /// If the data is correct, try to request an addition to the data layer
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.NotValidException"> check the inputs validation</exception>
        /// <exception cref="BO.AlreadyExistsException"> if the product is exist in the data</exception>
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
                dalEntity.Product.Create(tempProduct);

            }
            catch (AlreadyExistsException exc)
            {
                throw new BO.AlreadyExistsException(exc);
            }

        }
        /// <summary>
        /// the function delete product, get product ID
        /// Check that the product does not appear in any order
        /// and try to delete from the data layer
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BO.ExistsInOrder"> if the priduct is exist in order confirmation</exception>
        /// <exception cref="BO.NotExistException"> if the product isnt exist</exception>
        public void Delete(int id)
        {
            List<DO.OrderItem> orderItems = dalEntity.OrderItem.Read().ToList();
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
                dalEntity.Product.Delete(id);
            }
            catch (NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }

        }
        /// <summary>
        /// the function update product , get product object
        /// try to request an update to the data layer
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.NotValidException"> if one of the inputs isnt valid</exception>
        /// <exception cref="BO.NotExistException"> if the product isnt exist</exception>
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
            tempProduct.catagory = (DO.catagory)product.catagory;
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
