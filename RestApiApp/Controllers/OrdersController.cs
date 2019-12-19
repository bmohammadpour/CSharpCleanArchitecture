using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.ApplicationService;
using App.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_orderService.GetFilteredOrders(filter));
                //return Ok(_orderService.GetAllOrders());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater than 0.");

            return Ok(_orderService.FindOrderById(id));
        }

        // POST api/Orders
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/Orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
            {
                return BadRequest("Parameter Id and order Id must be the same.");
            }

            return Ok(_orderService.UpdateOrder(order));
        }

        // DELETE api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            var order = _orderService.DeleteOrder(id);

            if (order == null)
            {
                return StatusCode(404, $"Did not find Order with Id {id}");
            }

            return Ok($"Order with Id: {id} is Deleted");
        }
    }
}