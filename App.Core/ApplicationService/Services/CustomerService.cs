using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Entity;
using App.Core.DomainService;
using System.Linq;

namespace App.Core.ApplicationService.Services
{
    public class CustomerService : ICustomerService
    {
        //Constructor Dependency Injection
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public Customer NewCustomer(string firstName, string lastName, string address)
        {
            var cust = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };

            return cust;
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepository.Create(customer);
        }

        public Customer FindCustomerById(int id)
        {
            return _customerRepository.ReadById(id);
        }

        public Customer FindCustomerByIdIncludeOrders(int id)
        {
            var customer = _customerRepository.ReadById(id);
            customer.Orders = _orderRepository.ReadAll()
                .Where(order => order.Customer.Id == customer.Id)
                .ToList();

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.ReadAll().ToList();
        }

        public List<Customer> GetAllByFirstName(string name)
        {
            var list = _customerRepository.ReadAll(); // Not executed anything yet

            var query = list.Where(cust => cust.FirstName.Equals(name));
            query.OrderBy(customer => customer.FirstName);

            return query.ToList(); // Executed here
        }

        public Customer UpdateCustomer(Customer customer)
        {
           return _customerRepository.Update(customer);
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepository.Delete(id);
        }
    }
}
