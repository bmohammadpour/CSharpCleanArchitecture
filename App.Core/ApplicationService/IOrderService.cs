using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ApplicationService
{
    public interface IOrderService
    {
        //New Order
        Order NewOrder(DateTime orderDate, DateTime deliveryDate);

        //Create Order
        Order CreateOrder(Order order);

        //Read
        Order FindOrderById(int id);

        List<Order> GetAllOrders();

        List<Order> GetFilteredOrders(Filter filter);

        //Update
        Order UpdateOrder(Order order);

        //Delete
        Order DeleteOrder(int id);
    }
}
