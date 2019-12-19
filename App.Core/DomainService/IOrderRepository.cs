using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DomainService
{
    public interface IOrderRepository
    {
        // Create Data --> POST
        Order Create(Order customer);

        // Read Data --> GET
        Order ReadById(int id);

        IEnumerable<Order> ReadAll(Filter filter = null);

        int Count();

        // Update Data --> PUT
        Order Update(Order order);

        // Delete Data --> DELETE
        Order Delete(int id);
        
    }
}
