using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        public ICart Cart => new BlCart();

        public Iproduct Product => new BlProduct();

        public IOrder Order => new BlOrder();
    }
}
