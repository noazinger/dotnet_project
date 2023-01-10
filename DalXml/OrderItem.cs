using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DalApi;
using DO;
namespace Dal
{
    internal class OrderItem:IOrderItem
    {
        public void Create(DO.OrderItem oi)
        {
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer= new XmlSerializer(typeof(DO.OrderItem));
            var a = serializer.Deserialize(sr);
            StreamWriter sw = new StreamWriter(@"..\xml\OrderItem.xml");
            serializer.Serialize(sw, oi);
            sr.Close();
            sw.Close();
        } 

        public void Delete(int id)
        {
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            StreamWriter sw = new StreamWriter(@"..\xml\OrderItem.xml");
            List<DO.OrderItem> list = (List<DO.OrderItem>) serializer.Deserialize(sr);
            DO.OrderItem oi = list.Where(e => e.ID == id).FirstOrDefault();
            list.Remove(oi);
            serializer.Serialize(sw, list);
            sr.Close();
            sw.Close();
        }

        public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool> func=null)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem>? list = (List<DO.OrderItem>?) serializer.Deserialize(sr);
            sr.Close ();
            return list;            
        }
       
        public IEnumerable<DO.OrderItem> ReadByOrderId(int orderId) {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>),xRoot);
            List<DO.OrderItem> list = (List<DO.OrderItem>)serializer.Deserialize(sr);
            List<DO.OrderItem> result=new List<DO.OrderItem>();
            foreach (var i in list)
            {
                if(i.OrderID == orderId) result.Add(i);
            }
            /*IEnumerable<DO.OrderItem> result =  from X in list
                                                where X.OrderID==orderId
                                                select X;*/
         
            sr.Close();
            return result;
        }
        public DO.OrderItem ReadSingle(int id)
        {
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem> list = (List<DO.OrderItem>)serializer.Deserialize(sr);
            DO.OrderItem oi = list.Where(e => e.ID == id).FirstOrDefault();
            sr.Close();
            return oi;  
        }
        public DO.OrderItem ReadSingleByOrderIdAndProductId(int orderId, int productId)
        {
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem> list = (List<DO.OrderItem>)serializer.Deserialize(sr);
            DO.OrderItem oi = list.Where(e => e.ID == orderId && e.ProductID == productId).FirstOrDefault();
            sr.Close();
            return oi;
        }
        public void Update(DO.OrderItem item)
        {
            StreamReader sr = new StreamReader(@"..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer (typeof(OrderItem));
            StreamWriter sw = new StreamWriter(@"..\xml\OrderItem.xml");
            List<DO.OrderItem> list = (List<DO.OrderItem>) serializer.Deserialize(sr);
            DO.OrderItem orderItem = list.Where(e => e.ID == item.ID).FirstOrDefault();
            list.Remove(orderItem);
            //list.Add(item);
            //serializer.Serialize(sw, list);
            serializer.Serialize(sw, item); 
            sw.Close();
            sr.Close();
        }
    }
}
