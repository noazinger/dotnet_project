using DalApi;
using DO;
namespace Dal
{
    sealed public class DalXml : IDal
    {
        public static IDal? Instance { get; } = new DalXml();
        private DalXml() { }
        public IOrder Order { get; } = new Dal.Order();
        public IProduct Product { get; } =new Dal.Product();
        public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    }
}