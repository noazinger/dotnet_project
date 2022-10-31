using DO;
namespace Dal.DO;


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
    Amount in Stock = {inStock},
    ";
        }