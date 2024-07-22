using Microsoft.EntityFrameworkCore;
using OnlineStoreManagement.Data;
using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Repositories.Interfaces;

namespace OnlineStoreManagement.Repositories
{
    // Implement ProductRepository
    // ProductRepository kế thừa từ BaseRepository<Product>
    // và có thể được sử dụng ở bất cứ đâu mà IRepository<Product> được yêu cầu, tuân thủ LSP.
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger) : base(context, logger) {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            _logger.LogInformation("Fetching products in category {category}", category);
            return await _context.Products
                .Where(p => p.Category == category)
                .ToListAsync();
        }
    }
}
