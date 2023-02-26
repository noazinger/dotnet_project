using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BlApi;
using DalApi;

namespace BlImplementation
{

    public class Config
    {
        static int Id = 1;
        public static int OrderItemId { get { return Id++; } }
    }
    internal class BlCart : ICart
    {
        DalApi.IDal? dalEntity = DalApi.Factory.Get();
        /// <summary>
        /// the function get 2 parameters: 1. the cart 2. id of the product
        ///     if a product does not exist in the cart:
        /// I check if the product exists and is in stock
        /// add to the cart according to the current price of the product
        /// Define the total price of an item as the price of the above product
        /// Update the total price of the shopping basket\
        ///     if a product exist in the cart:
        /// Check if there is enough in stock of the product
        /// Increase quantity by 1, update total price of item and total price of cart
        ///  return the cart
        /// </summary>
        /// <param name = "cart" ></ param >
        /// < param name="id"></param>
        /// <returns> the function return the update cart after add product</returns>
        /// <exception cref = "BO.NotInStock" ></ exception >
        /// < exception cref= "BO.NotExistException" ></ exception >
        public BO.Cart Add(BO.Cart cart, int id)
        {
            if (cart.items != null)
            {
                BO.OrderItem item = cart.items.Find(x => x.ProductID == id) ?? null;
                if (item != null)
                {
                    DO.Product d = dalEntity.Product.ReadSingle(id);
                    if (d.inStock > 0)
                    {
                        item.Amount++;
                        item.TotalPrice += item.Price;
                        cart.TotalPrice += item.TotalPrice;
                        return cart;
                    }
                    else
                    {
                        throw new BO.NotInStock();
                    }
                }
            }
            else
            {
                cart.items = new List<BO.OrderItem>();
            }
            List<DO.Product> product = (from pr in dalEntity?.Product.Read()
                                        where pr.ID == id && pr.inStock > 0
                                        select pr).ToList();
            if (product == null) throw new BO.NotInStock();
            DO.Product p = product[0];
            BO.OrderItem orderItem = new BO.OrderItem();
            orderItem.ID = Config.OrderItemId;
            orderItem.Name = p.Name;
            orderItem.Price = p.Price;
            orderItem.ProductID = p.ID;
            orderItem.Amount = 1;
            if (orderItem?.TotalPrice == null) orderItem.TotalPrice = 0;
            orderItem.TotalPrice += p.Price;
            cart.items.Add(orderItem);
            cart.TotalPrice += orderItem.TotalPrice;
            return cart;
            throw new BO.NotExistException("the product is not exist");


            /// <summary>
            /// the function update the amount of the product in the cart
            /// the function get 3 parmeters: 


            throw new BO.NotExistException("the product is not exist");

        }
        /// <summary>
        /// the function update the amount of the product in the cart
        /// the function get 3 parmeters: cart object, product ID, new quantity
        /// Check if the quantity has changed:
        /// If the quantity increases - act similarly to adding a product to cart that already exists in the cart as above
        /// If the quantity is small - reduce the quantity accordingly and update the total price of the item and the cart
        /// If the quantity became 0 - delete the corresponding item from the cart and update the total price of the cart
        /// Return an updated cart object .
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productID"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="BO.NotInStock"></exception>
        /// <exception cref="BO.NotExistException"></exception>
        public BO.Cart Update(BO.Cart cart, int productID, int amount)
        {
            BO.OrderItem item = cart.items.Find(x => x.ProductID == productID) ?? null;
            if (item == null) throw new BO.NotExistException("the product is not exist in cart");


            if (amount < item.Amount)
            {
                if (amount == 0)
                {
                    cart.TotalPrice -= item.TotalPrice;
                    cart.items.Remove(item);
                    return cart;

                }
                else
                {
                    item.Amount = amount;
                    item.TotalPrice = item.Price * amount;
                    cart.TotalPrice -= item.Price * amount;
                    return cart;

                }
            }
            else
            {
                DO.Product product = dalEntity.Product.ReadSingle(productID);
                if (product.inStock - amount >= 0)
                {
                    item.Amount = amount;
                    cart.TotalPrice -= item.TotalPrice;
                    item.TotalPrice = item.Price * amount;
                    cart.TotalPrice += item.TotalPrice;
                    return cart;
                }
                else throw new BO.NotInStock();
            }


        }
        /// <summary>
        /// the function confirm cart for order
        /// the gunction get the cart object and the buyer details 
        /// I Check the integrity of all data
        /// and Create an order object based on the data in the cart
        /// I make an attempt to add an order, created to the data layer and get back an order number
        /// I Build order item objects according to the data from the cart and the aforementioned - 
        /// order number and make attempts to request the addition of an order item
        /// the products in the order should be removed from the inventory
        /// 
        /// <param name="cart"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <exception cref="BO.NotValidException"></exception>
        /// <exception cref="BO.OverflowException"></exception>
        /// <exception cref="BO.AlreadyExistsException"></exception>
        /// <exception cref="BO.NotDataException"></exception>
        /// <exception cref="BO.NotExistException"></exception>
        /// 
        ///     public void MakeAnOrder(BO.Cart c, string name, string email, string address)
       
        

            public void OrderConfirmation(BO.Cart cart, string name, string email, string address)
            {
                try
                {
                    if (name == "") throw new BO.NotValidException("the name isnt valid");
                    if (email == "") throw new BO.NotValidException("the email isnt valid");
                    if (address == "") throw new BO.NotValidException("the address isnt valid");
                    var Email = new EmailAddressAttribute();
                    if (!Email.IsValid(email) || email == "") throw new BO.NotValidException("the email isnt valid");
                    if (cart?.items?.Count() <= 0) throw new Exception("there are not order items that be ordered");
                    DO.Product p = new DO.Product();
                    DO.Order newOrder = new();
                    newOrder.OrderDate = DateTime.Now;
                    newOrder.DeliveryDate = DateTime.MinValue;
                    newOrder.ShipDate = DateTime.MinValue;
                    newOrder.CustomerAddress = address;
                    newOrder.CustomerName = name;
                    newOrder.CustomerEmail = email;

                    var id = dalEntity.Order.Add(newOrder);
                    var amount = from BO.OrderItem oi in cart.items
                                 let a = oi.Amount
                                 where a > dalEntity.Product.ReadSingle(oi.ProductID).inStock
                                 select oi;
                    if (amount.Count() > 0)
                        throw new BO.NotValidException("Not enough of this product in stock");

                    List<DO.OrderItem> listItems = (from i in cart.items
                                                    select new DO.OrderItem { Amount = i.Amount, ID = i.ID, OrderID = id, Price = i.Price, ProductID = i.ProductID }).ToList();
                    listItems.Select(item =>
                    {
                        if (item.Amount < 0) throw new BO.NotValidException("the amount isnt valid");
                        dalEntity.OrderItem.Create(item);
                        DO.Product ptoupdate = dalEntity.Product.ReadSingle(item.ProductID);
                        ptoupdate.inStock = ptoupdate.inStock - item.Amount;
                        dalEntity.Product.Update(ptoupdate);
                        return listItems;
                    }).ToList();
                }
                catch (StackOverFlowException exc)
                {
                    throw new BO.OverflowException(exc);
                }
                catch (AlreadyExistsException exc)
                {
                    throw new BO.AlreadyExistsException(exc);
                }
                catch (DataIsEmpty exc)
                {
                    throw new BO.NotDataException(exc);
                }
                catch (NotFoundException exc)
                {
                    throw new BO.NotExistException(exc);
                }
            }
        }
    }

