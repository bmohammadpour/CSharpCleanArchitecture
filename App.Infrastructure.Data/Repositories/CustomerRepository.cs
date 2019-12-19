using App.Core.DomainService;
using App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CleanArchContext _context;
        public CustomerRepository(CleanArchContext context)
        {
            _context = context;
        }

        public Customer Create(Customer customer)
        {
            var customerCreate = _context.Customers.Add(customer).Entity;
            _context.SaveChanges();

            return customerCreate;
        }

        public Customer ReadById(int id)
        {
            var changeTracker = _context.ChangeTracker.Entries<Customer>();

            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer ReadByIdIncludeOrders(int id)
        {
            return _context.Customers
                        .Include(c => c.Orders)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _context.Customers;
        }

        public Customer Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer Delete(int id)
        {
            /*
            var orderToRemove = _context.Orders.Where(o => o.Customer.Id == id);
            _context.RemoveRange(orderToRemove);
            */

            var customerRemoved = _context.Remove(new Customer { Id = id }).Entity;
            _context.SaveChanges();

            return customerRemoved;
        }

    }
}
