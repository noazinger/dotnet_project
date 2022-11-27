using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace DalApi
{
    public interface Icrud <T>
    {
        int Create(T n);
        void Update(T n);
        IEnumerable<T> Read();
        T ReadSingle(int id);
        void Delete(int id);
        
    }
}
