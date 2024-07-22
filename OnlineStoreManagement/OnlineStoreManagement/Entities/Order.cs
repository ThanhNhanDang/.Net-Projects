using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreManagement.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        
        [Precision(18, 2)]
        //[Column(TypeName = "decimal(6,2)")]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = null!;
    } 
}
