namespace PaymentService.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }

    public class PaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
