using App.Core.DomainService;
using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Infrastructure.Static.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
            if (FakeDB.Orders.Count > 0) return;
            var order1 = new Order()
            {
                Id = FakeDB.OrderId++,
                DeliveryDate = DateTime.Now.AddMonths(2),
                OrderDate = DateTime.Now.AddMonths(-1),
                Customer = new Customer() { Id = 1 }
            };
            FakeDB.Orders.Add(order1);

            var order2 = new Order()
            {
                Id = FakeDB.OrderId++,
                DeliveryDate = DateTime.Now.AddMonths(1),
                OrderDate = DateTime.Now.AddDays(-2),
                Customer = new Customer() { Id = 1 }
            };
            FakeDB.Orders.Add(order2);
        }

        public Order Create(Order order)
        {
            order.Id = FakeDB.OrderId++;
            FakeDB.Orders.Add(order);
            return order;
        }

        public IEnumerable<Order> ReadAll()
        {
            return FakeDB.Orders;
        }

        public Order ReadById(int id)
        {
            return FakeDB.Orders.FirstOrDefault(order => order.Id == id);
        }

        // Remove later when we use UOW
        public Order Update(Order order)
        {
            var orderFromDB = this.ReadById(order.Id);
            if (orderFromDB != null)
            {
                orderFromDB.OrderDate = order.OrderDate;
                orderFromDB.DeliveryDate = order.DeliveryDate;
                if (order.Customer != null && orderFromDB.Customer != null)
                {
                    orderFromDB.Customer.Id = order.Customer.Id;
                }
                return orderFromDB;
            }
            return null;
        }

        public Order Delete(int id)
        {
            var orderFound = this.ReadById(id);
            if (orderFound != null)
            {
                FakeDB.Orders.Remove(orderFound);
                return orderFound;
            }
            return null;
        }
    }
}
