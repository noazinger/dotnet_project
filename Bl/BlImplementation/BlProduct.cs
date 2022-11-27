﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlProduct:Iproduct<BlProduct>
    {
        IDal dalEntity = new Dal.DalList();
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
            catch(DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
           
            
           
        }
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
                dalEntity.Product.Create(tempProduct);

            }
            catch(AlreadyExistsException exc)
            {
                throw new BO.AlreadyExistsException(exc);
            }
        
        }
        public void Delete(int id)
        {
            foreach (var item in dalEntity.OrderItem.Read())
            {
                if (item.ProductID == id)
                {
                    DO.Order order = dalEntity.Order.ReadSingle(id);
                   if (order.ShipDate > DateTime.Now)
                    {
                        throw new BO.ExistsInOrder();
                    }
                }
                    
            }
            try
            {
                dalEntity.Product.Delete(id);
            }
            catch(NotFoundException exc)
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