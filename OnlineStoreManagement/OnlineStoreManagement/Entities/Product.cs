using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreManagement.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }

        /*(6,2) xác định độ chính xác của số thập phân:

6 là tổng số chữ số(bao gồm cả phần nguyên và phần thập phân)
2 là số chữ số sau dấu thập phân*/
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        
    }
}
