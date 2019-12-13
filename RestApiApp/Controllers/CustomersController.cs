using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.ApplicationService;
using App.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace RestApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/Customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetAllCustomers();
        }

        // GET api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id < 1 ) return BadRequest("Id must be greater than 0.");

            //return _customerService.FindCustomerById(id);
            return _customerService.FindCustomerByIdIncludeOrders(id);
        }

        // POST api/Customers
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                return BadRequest("FirstName is Required for Creating Customer");
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                return BadRequest("LastName is Required for Creating Customer");
            }

            return _customerService.CreateCustomer(customer);
        }

        // PUT api/Customers/5
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.Id)
            {
                return BadRequest("Parameter Id and customer Id must be the same.");
            }

            return Ok(_customerService.UpdateCustomer(customer));
        }

        // DELETE api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _customerService.DeleteCustomer(id);

            if (customer == null)
            {
                return StatusCode(404, $"Did not find customer with Id {id}");
            }

            return Ok($"Customer with Id: {id} is Deleted");
        }
    }
}
