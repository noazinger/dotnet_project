using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;


    public class NotInStock : Exception
{
    public NotInStock() : base("The product does not in stock")
    {

    }
}
public class NotExistException : Exception
{
    public NotExistException(Exception exc) : base("The item does not exist", exc) { }
    public NotExistException(string mess) : base(mess) { }

}
public class NotDataException : Exception
{

    public NotDataException(Exception exc) : base("the Data Is Empty", exc) { }
 
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(Exception exc) : base("it is already exists on data", exc) { }
  
}


public class NotValidException : Exception
{
    public NotValidException(Exception exc) : base("the input is not valid", exc) { }

    public NotValidException(string exc) : base(exc) { }


}
public class ExistsInOrder : Exception
{
    public ExistsInOrder() : base("the product exist in some order") { }

}


 



