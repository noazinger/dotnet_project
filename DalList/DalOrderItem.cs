using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace Dal
{
    internal struct DalOrderItem
    {
        public static void Create(OrderItem obj)
        {
            if (DataSource.Config.OrderItemIndex > 200)
                throw new Exception("There is not enough space");
            DataSource.orderItemsData[DataSource.Config.OrderItemIndex++] = obj;
        }

        public static OrderItem ReadSingle(int Id)
        {
            for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
            {
                if (DataSource.orderItemsData[i].ProductID == Id)
                    return DataSource.orderItemsData[i];
            }
            throw new Exception("The product isn't exist");
        }

        public static OrderItem[] Read()
        {
            OrderItem[] tempOrderItem = new OrderItem[DataSource.Config.OrderItemIndex];
            for (int i = 0; i < DataSource.Config.ProductIndex; i++)
            {
                tempOrderItem[i] = DataSource.orderItemsData[i];
            }
            return tempOrderItem;
        }
        public static void Delete(int Id)
        {
            for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
            {
                if (DataSource.orderItemsData[i].ProductID == Id)
                {
                    OrderItem temp = DataSource.orderItemsData[i];
                    DataSource.orderItemsData[i] = DataSource.orderItemsData[DataSource.Config.OrderItemIndex];
                    DataSource.orderItemsData[DataSource.Config.OrderItemIndex] = temp;
                    DataSource.Config.OrderItemIndex--;
                }
            }
            throw new Exception("Product isn't exist");
        }

        public static void UpDate(OrderItem obj)
        {
            for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
            {
                if (DataSource.orderItemsData[i].ProductID == obj.ProductID)
                {
                    DataSource.orderItemsData[i] = obj;
                }
            }
            throw new Exception("Product isn't exist");
        }
    }
}
