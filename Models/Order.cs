using System.ComponentModel.DataAnnotations;

namespace Mobile_Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";

        public ApplicationUser? User { get; set; }
        public List<OrderItem>? Items { get; set; }
    }
}
