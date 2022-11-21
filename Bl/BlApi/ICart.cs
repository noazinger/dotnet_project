using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public  interface ICart
    {
        public void Create(ICart cart);
        public void Update(ICart cart);

    }
}
