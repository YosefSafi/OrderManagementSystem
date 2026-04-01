using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static readonly List<Transaction> _transactions = new();

        [HttpGet("{orderId}")]
        public ActionResult<Transaction> GetByOrderId(Guid orderId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.OrderId == orderId);
            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpPost("process")]
        public IActionResult Process([FromBody] PaymentRequest request)
        {
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                OrderId = request.OrderId,
                Amount = request.Amount,
                Status = "Completed", // Simulating a successful payment
                TransactionDate = DateTime.UtcNow
            };

            _transactions.Add(transaction);

            return Ok(new { Message = "Payment processed successfully.", Transaction = transaction });
        }

        [HttpPost("refund/{orderId}")]
        public IActionResult Refund(Guid orderId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.OrderId == orderId);
            if (transaction == null) return NotFound("Transaction not found.");

            transaction.Status = "Refunded";
            return Ok(new { Message = "Payment refunded successfully.", Transaction = transaction });
        }
    }
}
