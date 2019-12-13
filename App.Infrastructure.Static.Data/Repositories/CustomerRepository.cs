using App.Core.DomainService;
using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Entity;
using System.Linq;

namespace App.Infrastructure.Static.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        // Our Fake Data
        public CustomerRepository()
        {
            if (FakeDB.Customers.Count >= 1) return;
            var cust1 = new Customer()
            {
                Id = FakeDB.Id++,
                FirstName = "Ronika",
                LastName = "Mohammadpour",
                Address = "AmrollahiStreet 5"
            };
            FakeDB.Customers.Add(cust1);

            var cust2 = new Customer()
            {
                Id = FakeDB.Id++,
                FirstName = "Romina",
                LastName = "Mohammadpour",
                Address = "AmrollahiStreet 5"
            };
            FakeDB.Customers.Add(cust2);
        }

        public Customer Create(Customer customer)
        {
            customer.Id = FakeDB.Id++;
            FakeDB.Customers.Add(customer);
            return customer;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return FakeDB.Customers;
        }

        public Customer ReadById(int id)
        {
            #region Old Code

                //foreach (var customer in FakeDB.Customers)
                //{
                //    if (customer.Id == id)
                //    {
                //        return customer;
                //    }
                //}
                //return null;

            #endregion

            return FakeDB.Customers.FirstOrDefault(c => c.Id == id);
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
                FakeDB.Customers.Remove(customerFound);
                return customerFound;
            }
            return null;
        }
    }
}
