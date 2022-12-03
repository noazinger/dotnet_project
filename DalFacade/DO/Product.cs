using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;


public struct Product
{
    public int ID;
    public string ? Name;
    public double Price;
    public catagory? catagory;
    public int inStock;
    public override string ToString() => $@"
    ID={ID},
    Product Name={Name}, 
    Product Price={Price},
    Product catagory={catagory},
    Amount in Stock = {inStock},
    ";
    }