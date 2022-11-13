using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace DalApi
{
    internal interface Icrud
    {
        void Create <T> (T n);
        void Update<T> (T n);
        IEnumerable <T> Read<T> (T n);
        T ReadSingle<T> (T n);
        void Delete<T> (T n);
        
    }
}
