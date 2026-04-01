using Microsoft.AspNetCore.Mvc;
using InventoryService.Models;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static readonly List<InventoryItem> _stock = new()
        {
            new InventoryItem { ProductId = Guid.NewGuid(), ProductName = "Laptop", AvailableQuantity = 10, ReservedQuantity = 0 },
            new InventoryItem { ProductId = Guid.NewGuid(), ProductName = "Mouse", AvailableQuantity = 50, ReservedQuantity = 0 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> Get() => Ok(_stock);

        [HttpGet("{productId}")]
        public ActionResult<InventoryItem> Get(Guid productId)
        {
            var item = _stock.FirstOrDefault(i => i.ProductId == productId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("reserve")]
        public IActionResult Reserve([FromBody] ReservationRequest request)
        {
            var item = _stock.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null) return NotFound("Product not found.");

            if (item.AvailableQuantity < request.Quantity)
                return BadRequest("Insufficient stock.");

            item.AvailableQuantity -= request.Quantity;
            item.ReservedQuantity += request.Quantity;

            return Ok(new { Message = "Stock reserved successfully.", RemainingAvailable = item.AvailableQuantity });
        }

        [HttpPost("release")]
        public IActionResult Release([FromBody] ReservationRequest request)
        {
            var item = _stock.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null) return NotFound("Product not found.");

            item.AvailableQuantity += request.Quantity;
            item.ReservedQuantity -= request.Quantity;

            return Ok(new { Message = "Stock released successfully.", RemainingAvailable = item.AvailableQuantity });
        }
    }
}
