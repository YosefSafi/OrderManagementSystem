using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get() => Ok(_orders);

        [HttpGet("{id}")]
        public ActionResult<Order> Get(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";
            _orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null) return NotFound();

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            _orders.Remove(order);
            return NoContent();
        }
    }
}
