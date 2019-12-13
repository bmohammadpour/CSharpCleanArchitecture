using App.Core.DomainService;
using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        //Constructor Dependency Injection
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public Order NewOrder(DateTime orderDate, DateTime deliveryDate)
        {
            var order = new Order()
            {
                OrderDate = orderDate,
                DeliveryDate = deliveryDate
            };

            return order;
        }

        public Order CreateOrder(Order order)
        {
            if (order.Customer == null || order.Customer.Id <= 0)
                throw new InvalidDataException("To create Order you need a Customer");

            if (_customerRepository.ReadById(order.Customer.Id) == null)
                throw new InvalidDataException("Customer Not found");

            if (order.OrderDate == null)
                throw new InvalidDataException("Order needs a Order Date");

            return _orderRepository.Create(order);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepository.ReadById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.ReadAll().ToList();
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepository.Update(order);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepository.Delete(id);
        }
    }
}
