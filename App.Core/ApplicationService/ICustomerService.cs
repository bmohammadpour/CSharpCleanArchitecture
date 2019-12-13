using App.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ApplicationService
{
    public interface ICustomerService
    {
        //New Customer
        Customer NewCustomer(string firstName, string lastName, string address);

        //Create Customer
        Customer CreateCustomer(Customer customer);

        //Read
        Customer FindCustomerById(int id);

        Customer FindCustomerByIdIncludeOrders(int id);

        List<Customer> GetAllCustomers();

        List<Customer> GetAllByFirstName(string name);

        //Update
        Customer UpdateCustomer(Customer customer);

        //Delete
        Customer DeleteCustomer(int id);
    }
}
