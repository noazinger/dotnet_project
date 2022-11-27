using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public  interface ICart
    {
        public BO.Cart Add(BO.Cart c,int id);
        public BO.Cart Update(BO.Cart cart, int productID, int amount);
        public void OrderConfirmation(BO.Cart cart, string name, string email, string address);

    }
}
