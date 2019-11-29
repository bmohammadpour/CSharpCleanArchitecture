using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DomainService
{
    public interface ICustomerRepository
    {
        // Create Data
        Customer Create(Customer customer);

        // Read Data
        Customer ReadById(int id);
        IEnumerable<Customer> ReadAll();

        // Update Data
        Customer Update(Customer customer);

        // Delete Data
        Customer Delete(int id);
    }
}
