﻿
using DO;
namespace DalList;

public struct DalOrder
{
    public static void Create(Order obj)
    {
        if (DataSource.Config.OrderIndex > 99)
            throw new Exception("There is not enough space");
         obj.ID= DataSource.Config.OrderIndex++;
         DataSource.ordersData[DataSource.Config.OrderIndex-1] = obj;
    }
    public static Order ReadSingle(int id)
    {
        for(int i=0;i< DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.ordersData[i].ID == id) return DataSource.ordersData[i];
        }
        throw new Exception("The order does not exist");

    }
    public static Order[] Read()
    {
        Order[] tempOrders = new Order[DataSource.Config.OrderIndex];
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            tempOrders[i]= DataSource.ordersData[i];
        }
        return tempOrders;

    }
    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex + 1; i++)
        {
            if (DataSource.ordersData[i].ID == id)
            {
                Order tmp = new Order();
                tmp = DataSource.ordersData[i];
                DataSource.ordersData[i]=  DataSource.ordersData[DataSource.Config.OrderIndex];
                DataSource.ordersData[DataSource.Config.OrderIndex]= tmp;
                DataSource.Config.OrderIndex--;
                return;
            }

        }
        throw new Exception("The order does not exist");
    }
    public static void UpDate(Order obj)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.ordersData[i].ID == obj.ID)
            {
                DataSource.ordersData[i] = obj;
                return;
            }

        }
        throw new Exception("The order does not exist");
    }



}
