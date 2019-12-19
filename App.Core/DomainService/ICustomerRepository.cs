using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DomainService
{
    public interface ICustomerRepository
    {
        // Create Data --> POST
        Customer Create(Customer customer);

        // Read Data --> GET
        Customer ReadById(int id);

        Customer ReadByIdIncludeOrders(int id);

        IEnumerable<Customer> ReadAll();

        // Update Data --> PUT
        Customer Update(Customer customer);

        // Delete Data --> DELETE
        Customer Delete(int id);
    }
}
