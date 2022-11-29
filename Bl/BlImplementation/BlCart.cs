﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlCart : ICart
    {
        IDal dalEntity = new Dal.DalList();
        public BO.Cart Add(BO.Cart cart, int id)
        {
            foreach (var i in cart.items)
            {
                if (i.ProductID == id)
                {
                    DO.Product d = dalEntity.Product.ReadSingle(id);
                    if (d.inStock > 0) {
                        i.Amount++;
                        i.TotalPrice += i.Price;
                        cart.TotalPrice += i.Price;
                        return cart;
                    }
                    else
                    {
                        throw new BO.NotInStock();
                    }
                }
            }
            foreach (var p in dalEntity.Product.Read())
            {
                if (p.ID == id)
                {
                    if (p.inStock > 0) {
                        BO.OrderItem orderItem = new BO.OrderItem();
                        orderItem.ID = 0;
                        orderItem.Name = p.Name;
                        orderItem.Price = p.Price;
                        orderItem.ProductID = p.ID;
                        orderItem.Amount = 1;
                        orderItem.TotalPrice = p.Price;
                        cart.items.Add(orderItem);
                        return cart;
                    }
                    else throw new BO.NotInStock();
                }

            }
            throw new BO.NotExistException("the product is not exist");

        }
        public BO.Cart Update(BO.Cart cart, int productID, int amount)
        {
            foreach (var i in cart.items)
            {
                if (i.ProductID == productID)
                {
                    if (amount < i.Amount)
                    {
                        if (i.Amount - amount == 0)
                        {
                            cart.TotalPrice -= i.TotalPrice;
                            cart.items.Remove(i);
                            return cart;

                        }
                        else
                        {
                            i.Amount -= amount;
                            i.TotalPrice -= i.Price * amount;
                            cart.TotalPrice += i.TotalPrice;
                            return cart;

                        }
                    }
                    else
                    {
                        DO.Product product = dalEntity.Product.ReadSingle(productID);
                        if (product.inStock - amount >= 0)
                        {
                            i.Amount += amount;
                            i.TotalPrice += i.Price * amount;
                            cart.TotalPrice += i.TotalPrice;
                            return cart; 
                        }
                        else throw new BO.NotInStock();
                    }

                }
            }
            throw new BO.NotExistException("the product is not exist in cart");

        }
        public void OrderConfirmation(BO.Cart cart, string name, string email, string address)
        {
            try
            {
                if (name == "") throw new BO.NotValidException("the name isnt valid");
                if (email == "") throw new BO.NotValidException("the email isnt valid");
                if (address == "") throw new BO.NotValidException("the address isnt valid");
                var Email = new EmailAddressAttribute();
                if (!Email.IsValid(email) || email == "") throw new BO.NotValidException("the email isnt valid");
                DO.Product p = new DO.Product();
                DO.Order newOrder = new();
                newOrder.OrderDate = DateTime.Now;
                newOrder.DeliveryDate = DateTime.MinValue;
                newOrder.ShipDate = DateTime.MinValue;
                newOrder.CustomerAddress = address;
                newOrder.CustomerName = name;
                newOrder.CustomerEmail = email;
                var id = dalEntity.Order.Add(newOrder);
                    foreach (var i in cart.items)
                    {
                    i.ID = id;
                        DO.OrderItem orderItem = new();
                        orderItem.OrderID = id;
                        orderItem.Price = i.Price;
                        orderItem.ProductID = i.ProductID;
                        orderItem.Amount = i.Amount;
                        dalEntity.OrderItem.Create(orderItem);                        
                    }
                foreach (var item in cart.items)
                {
                    List<DO.Product> products = dalEntity.Product.Read().ToList();
                    p = products.Find(x => x.ID == item.ProductID);
                    if (p.inStock < item.Amount) throw new BO.NotValidException("Not enough of this product in stock");
                    if (item.Amount < 0) throw new BO.NotValidException("the amount isnt valid");
                    p.inStock -= item.Amount;
                    dalEntity.Product.Update(p);
                }
            }
            catch(StackOverFlowException exc)
            {
                throw new BO.OverflowException(exc);
            }
            catch (AlreadyExistsException exc)
            {
                throw new BO.AlreadyExistsException(exc);
            }
            catch(DataIsEmpty exc)
            {
                throw new BO.NotDataException(exc);
            }
            catch(NotFoundException exc)
            {
                throw new BO.NotExistException(exc);
            }

        
        }
    }
}

