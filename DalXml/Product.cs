using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;

internal class Product : IProduct
{
    public void Create(DO.Product p)
    {

        XElement? orderElement = XDocument.Load(@"..\xml\Product.xml").Root;
        XElement? product = new XElement("Product",
        new XElement("ID", p.ID),
        new XElement("Price", p.Price),
        new XElement("Name", p.Name),
        new XElement("inStock", p.inStock),
        new XElement("catagory", p.catagory));
        orderElement?.Add(product);
        orderElement?.Save(@"..\xml\Product.xml");
        /*StreamReader sr = new StreamReader(@"..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Product));
        sr.Close();
        StreamWriter sw = new StreamWriter(@"..\xml\Product.xml");
        serializer.Serialize(sw, p);
        sw.Close();*/
    }

    public void Update(DO.Product p)
    {
        
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        StreamReader sr = new StreamReader(@"..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>),xRoot);
        List<DO.Product> list = (List<DO.Product>)serializer.Deserialize(sr);
        sr.Close();
        StreamWriter sw = new StreamWriter(@"..\xml\Product.xml");
        DO.Product product = list.Where(e => e.ID == p.ID).FirstOrDefault();
        list.Remove(product);
        list.Add(p);
        serializer.Serialize(sw, list);
        sw.Close();
    }
    //public void updateAmount(int id, int amount)
    //{
    //    XmlRootAttribute xRoot = new XmlRootAttribute();
    //    xRoot.ElementName = "Products";
    //    xRoot.IsNullable = true;
    //    XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot);
    //    StreamReader reader = new StreamReader("..\\..\\..\\..\\xml\\Product.xml");
    //    List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
    //    reader.Close();
    //    StreamWriter writer = new StreamWriter("..\\..\\..\\..\\xml\\Product.xml");
    //    Product pro = products.Where(p => p.ID == id).FirstOrDefault();
    //    Product prod = pro;
    //    prod.InStock = amount;
    //    products.Remove(pro);
    //    products.Add(prod);
    //    ser.Serialize(writer, products);
    //    writer.Close();
    //    //  throw new NotImplementedException();
    //}
    public IEnumerable<DO.Product> Read(Func<DO.Product, bool> func = null)
    {

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        StreamReader sr = new StreamReader(@"..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>), xRoot);
        List<DO.Product>? list = (List<DO.Product>?)serializer.Deserialize(sr);
        sr.Close();
        return list;
    }
    public DO.Product ReadSingle(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        StreamReader sr = new StreamReader(@"..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>),xRoot);
        List<DO.Product> list = (List<DO.Product>)serializer.Deserialize(sr);
        DO.Product product = list.Where(e => e.ID == id).FirstOrDefault();
        sr.Close();
        return product;
    }
    public void Delete(int id)
    {
        StreamReader sr = new StreamReader(@"..\xml\Product.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Product>));
        List<DO.Product> list = (List<DO.Product>?)serializer?.Deserialize(sr);
        sr.Close();
        StreamWriter sw = new StreamWriter(@"..\xml\Product.xml");
        DO.Product product=list.Where(e=> e.ID == id).FirstOrDefault();
        list.Remove(product);
        serializer.Serialize(sw,list);
        sw.Close();
    }
  /*  public static IEnumerable<BO.ProductItem> ReadProductByCategoty(BO.catagory category)
    {

    }*/
}

