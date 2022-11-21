using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO
{
    public class ProductItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public catagory catagory { get; set; }
        public int Amount { get; set; }
        public bool InStock { get; set; }

        public override string ToString() => $@"
        ID={ID},
        Product Name={Name}, 
        Product Price={Price},
        Product catagory={catagory},
        Amount = {Amount},
        Amount in Stock = {InStock},
        ";
    }
}
