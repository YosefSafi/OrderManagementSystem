namespace InventoryService.Models
{
    public class InventoryItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }

    public class ReservationRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
