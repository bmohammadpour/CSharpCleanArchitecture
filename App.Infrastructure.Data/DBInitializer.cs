using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CleanArchContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            var cust1 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Ronika",
                LastName = "Mohammadpour",
                Address = "Tehran"
            }).Entity;

            var cust2 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Romina",
                LastName = "Mohammadpour",
                Address = "Tehran"
            }).Entity;

            ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.Orders.Add(new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust2
            });
            ctx.SaveChanges();
        }
    }
}
