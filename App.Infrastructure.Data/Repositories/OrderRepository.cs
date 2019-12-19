using App.Core.DomainService;
using App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CleanArchContext _context;

        public OrderRepository(CleanArchContext context)
        {
            _context = context;
        }

        public Order Create(Order order)
        {
            /*
                if (order.Customer != null &&
                    _context.ChangeTracker.Entries<Customer>()
                    .FirstOrDefault(ce => ce.Entity.Id == order.Customer.Id) == null)
                {
                    _context.Attach(order.Customer);
                }

                var orderCreate = _context.Orders.Add(order).Entity;
            */

            _context.Attach(order).State = EntityState.Added;

            _context.SaveChanges();

            return order;
        }

        public Order ReadById(int id)
        {
            return _context.Orders
                        .Include(o => o.Customer)
                        .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> ReadAll(Filter filter)
        {
            if (filter == null)
            {
                return _context.Orders;
            }

            return _context.Orders
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage);
        }

        public int Count()
        {
            return _context.Orders.Count();
        }

        public Order Update(Order order)
        {
            /*
                if (order.Customer != null &&
                    _context.ChangeTracker.Entries<Customer>()
                    .FirstOrDefault(ce => ce.Entity.Id == order.Customer.Id) == null)
                {
                    _context.Attach(order.Customer);
                }
                else
                {
                    _context.Entry(order).Reference(o => o.Customer).IsModified = true;
                }

                var orderUpdate = _context.Update(order).Entity;
            */

            _context.Attach(order).State = EntityState.Modified;

            _context.SaveChanges();

            return order;
        }

        public Order Delete(int id)
        {
            var orderRemoved = _context.Remove(new Order { Id = id }).Entity;
            _context.SaveChanges();

            return orderRemoved;
        }

    }
}
