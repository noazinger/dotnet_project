using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class NotFoundException : Exception
    {

        public override string Message => "not found";


    }
    public class DuplicateException : Exception
    {

        public override string Message => "exist";


    }
    public class StackOverFlowException : Exception
    {

        public override string Message => "Stack overflow -there is not enough space";


    }
    public class NotValidException : Exception
    {

        public override string Message => "the input is not valid";


    }

}
