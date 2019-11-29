using App.Core.ApplicationService;
using App.Core.DomainService;
using App.Core.Entity;
using App.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    #region Comments

    // --- UI ---
    // Console.WriteLine | Console.ReadLine

    // --- Infrastructure ---
    // EF | Static List | Text File

    // --- Test ---
    // Unit test for Core

    /* --- Core ---
       Customer | Entity -> Core.Entity
       Domain Service - Repository | UOW -> Core
       Application Service - Service -> Core
    */

    #endregion

    public class Printer : IPrinter
    {
        #region Repository area

        private ICustomerService _customerService;

        #endregion

        public Printer(ICustomerService customerService)
        {
            _customerService = customerService;

            InitData();
        }

        #region UI

        public void StartUI()
        {
            string[] menuItems =
            {
                "List All Customers",
                "Add Customer",
                "Edit Customer",
                "Delete Customer",
                "Exit"
            };

            var selection = ShowMenu(menuItems);

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        var customers = _customerService.GetAllCustomers();
                        ListCustomers(customers);
                        break;
                    case 2:
                        var firstName = AskQuestion("FirstName: ");
                        var lastName = AskQuestion("LastName: ");
                        var address = AskQuestion("Address: ");

                        var customer = _customerService.NewCustomer(firstName, lastName, address);
                        _customerService.CreateCustomer(customer);
                        break;
                    case 3:
                        var idForEdit = PrintFindCustomerByID();
                        var customerToEdit = _customerService.FindCustomerById(idForEdit);
                        Console.WriteLine("Updating " + customerToEdit.FirstName);
                        var newFirstName = AskQuestion("FirstName: ");
                        var newLastName = AskQuestion("LastName: ");
                        var newAddress = AskQuestion("Address: ");
                        _customerService.UpdateCustomer(new Customer()
                        {
                            Id = idForEdit,
                            FirstName = newFirstName,
                            LastName = newLastName,
                            Address = newAddress
                        });
                        break;
                    case 4:
                        var idForDelete = PrintFindCustomerByID();
                        _customerService.DeleteCustomer(idForDelete);
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Bye bye!");
            Console.ReadLine();
        }

        private void ListCustomers(List<Customer> customers)
        {
            Console.WriteLine("\nList of Customers: ");

            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id} Name: {customer.FirstName} " +
                    $"{customer.LastName} " +
                    $"Address: {customer.Address}");
            }
            Console.WriteLine("\n");
        }

        string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        int PrintFindCustomerByID()
        {
            Console.WriteLine("Insert Customer Id: ");

            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number: ");
            }
            return id;
        }

        /// <summary>
        /// Shows the menu.
        /// </summary>
        /// <param name="menuItems"></param>
        /// <returns>Menu Choice as int</returns>
        private int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select what you want to do:\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                || selection < 1
                || selection > 5)
            {
                Console.WriteLine("Please select a number between 1-5");
            }

            return selection;
        }

        #endregion

        #region Infrastructure Layer / Initialization Layer

        private void InitData()
        {
            var cust1 = new Customer()
            {
                FirstName = "Ronika",
                LastName = "Mohammadpour",
                Address = "AmrollahiStreet 5"
            };
            _customerService.CreateCustomer(cust1);

            var cust2 = new Customer()
            {
                FirstName = "Romina",
                LastName = "Mohammadpour",
                Address = "AmrollahiStreet 5"
            };
            _customerService.CreateCustomer(cust2);
        }

        #endregion
    }
}
