using DalApi;
using DO;
namespace Dal
{
    sealed internal class DalXml : IDal
    {
        public IOrder Order { get; } = new Dal.Order();
        public IProduct Product { get; } =new Dal.Product();
        public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    }
}