using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   
        public class NotExistException : Exception
        {
        public NotExistException(Exception exc):base("The item does not exist", exc)
        {

        }
       
         

        }
    public class NotValidException : Exception
    {

        public override string Message => "  The ID number is incorrect";


    }
  

}
