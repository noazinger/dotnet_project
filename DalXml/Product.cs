using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Serialization;

internal class Product : IProduct
{
    public void Create(DO.Product p)
    {
        StreamReader sr = new StreamReader(@"..\..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Product));
        StreamWriter sw = new StreamWriter(@"..\..\xml\Product.xml");
        var de = serializer.Deserialize(sr);
        serializer.Serialize(sw, p);
        sr.Close();
        sw.Close();
    }

    public void Update(DO.Product p)
    {
        StreamReader sr = new StreamReader(@"..\..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Product));
        StreamWriter sw = new StreamWriter(@"..\..\xml\Product.xml");
        List<DO.Product> list = (List<DO.Product>)serializer.Deserialize(sr);
        DO.Product product = list.Where(e => e.ID == p.ID).FirstOrDefault();
        list.Remove(product);
        serializer.Serialize(sw, p);
        sw.Close();
        sr.Close();
    }
    public IEnumerable<DO.Product> Read(Func<DO.Product, bool> func = null)
    {
        StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>));
        List<DO.Product> list = (List<DO.Product>?)serializer?.Deserialize(sr);
        sr.Close();
        return list;
    }
    public DO.Product ReadSingle(int id)
    {
        StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>));
        List<DO.Product> list = (List<DO.Product>)serializer.Deserialize(sr);
        DO.Product product = list.Where(e => e.ID == id).FirstOrDefault();
        sr.Close();
        return product;
    }
    public void Delete(int id)
    {
        StreamReader sr = new StreamReader(@"..\..\xml\OrderItem.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>));
        StreamWriter sw = new StreamWriter(@"..\..\xml\OrderItem.xml");
        List<DO.Product> list = (List<DO.Product>?)serializer?.Deserialize(sr);
        DO.Product product=list.Where(e=> e.ID == id).FirstOrDefault();
        list.Remove(product);
        serializer.Serialize(sw,list);
        sr.Close();
        sw.Close();
    }
}

