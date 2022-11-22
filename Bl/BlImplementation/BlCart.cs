using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
namespace BlImplementation
{
    internal class BlCart : ICart
    {
        IDal dalEntity = new Dal.DalList();
        public void Create(ICart cart, int id);
        public void Update(ICart cart);
        public void OrderConfirmation(ICart cart, string name, string email, string address);
    }
}
