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
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
            var customerForUpdate = FindCustomerById(customer.Id);

            customerForUpdate.FirstName = customer.FirstName;
            customerForUpdate.LastName = customer.LastName;
            customerForUpdate.Address = customer.Address;

            return customerForUpdate;
        }

        public Customer DeleteCustomer(int id)
        {
            return _customerRepository.Delete(id);
        }
    }
}
