using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

    namespace Dal;
    using DalApi;
    using DO;

internal class Order :IOrder
{
    public int Add(DO.Order order)
    {

    }
          void Create(DO.Order or)
           {
           XElement order = XDocument.Load("Order.xml").Root;
    
            order = new XElement("id", or.ID);

        XElement id = new XElement("id", item.Id);

            XElement firstName = new XElement("firstName", item.FirstName);
            XElement lastName = new XElement("lastName", item.LastName);

            XElement name = new XElement("name", firstName, lastName);

            XElement student = new XElement("student", id, name);

            return student;
          }

    }
    void Update(T n);
        IEnumerable<T?> Read(Func<T, bool> func = null)
    {
   
    }
}
        T ReadSingle(int id);
        void Delete(int id);
}

 

