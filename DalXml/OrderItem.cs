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
            StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
            XmlSerializer serializer= new XmlSerializer(typeof(DO.OrderItem));
            var a = serializer.Deserialize(sr);
            StreamWriter sw = new StreamWriter(@"..\..\xml\OrderItem.xml");
            serializer.Serialize(sw, oi);
            sr.Close();
            sw.Close();
        } 

        public void Delete(int id)
        {
            StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            StreamWriter sw = new StreamWriter(@"..\..\xml\OrderItem.xml");
            List<DO.OrderItem> list = (List<DO.OrderItem>) serializer.Deserialize(sr);
            DO.OrderItem oi = list.Where(e => e.ID == id).FirstOrDefault();
            list.Remove(oi);
            serializer.Serialize(sw, list);
            sr.Close();
            sw.Close();
        }

        public IEnumerable<DO.OrderItem> Read(Func<DO.OrderItem, bool> func = null)
        {
            
                StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
                List<DO.OrderItem> list = (List<DO.OrderItem>?)serializer?.Deserialize(sr);
                return list;
        }

        public IEnumerable<DO.OrderItem> ReadByOrderId(int orderId) {

            StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem> list = (List<DO.OrderItem>)serializer.Deserialize(sr);
            List<DO.OrderItem> oi = (List<DO.OrderItem>)list.Where(e => e.ID == orderId);
            return oi;
        }

        public DO.OrderItem ReadSingle(int id)
        {
            StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem> list = (List<DO.OrderItem>)serializer.Deserialize(sr);
            DO.OrderItem oi = list.Where(e => e.ID == id).FirstOrDefault();
            return oi;  
            
        }

        public OrderItem ReadSingleByOrderIdAndProductId(int orderId, int productId)
        {
            OrderItem o = new();
            return o;
        }

        public void Update(DO.OrderItem n)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DO.OrderItem> IOrderItem.ReadByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        DO.OrderItem IOrderItem.ReadSingleByOrderIdAndProductId(int orderId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
