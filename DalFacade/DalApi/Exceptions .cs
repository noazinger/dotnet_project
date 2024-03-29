﻿using System;
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
    public class DataIsEmpty : Exception
    {
        public override string Message => "the Data Is Empty";
    }
    public class AlreadyExistsException : Exception
    {
        public override string Message => "the item is already exists";
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }


}
