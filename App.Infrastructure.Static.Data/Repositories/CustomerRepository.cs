using App.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Entity;

namespace App.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        // Our Fake DB
        static int id = 1;
        private List<Customer> _customers = new List<Customer>();

        public Customer Create(Customer customer)
        {
            customer.Id = id++;
            _customers.Add(customer);
            return customer;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _customers;
        }

        public Customer ReadById(int id)
        {
            foreach (var customer in _customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }
            return null;
        }

        // Remove later when we use UOW
        public Customer Update(Customer customer)
        {
            var customerFromDB = this.ReadById(customer.Id);
            if (customerFromDB != null)
            {
                customerFromDB.FirstName = customer.FirstName;
                customerFromDB.LastName = customer.LastName;
                customerFromDB.Address = customer.Address;
                return customerFromDB;
            }
            return null;
        }

        public Customer Delete(int id)
        {
            var customerFound = this.ReadById(id);
            if (customerFound != null)
            {
                _customers.Remove(customerFound);
                return customerFound;
            }
            return null;
        }
    }
}
