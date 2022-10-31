using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

internal struct Product
{
    int ID;
    string Name;
    double Price;
    catagory catagory;
    int inStock;
    public override string ToString() => $@"
    ID={ID},
    Product Name={Name}, 
    Product Price={Price},
    Product catagory={catagory},
    Order Date={OrderDate},
    Amount in Stock = {inStock},
    "
        }