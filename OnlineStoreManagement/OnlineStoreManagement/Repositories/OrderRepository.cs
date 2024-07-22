using Microsoft.EntityFrameworkCore;
using OnlineStoreManagement.Data;
using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Repositories.Interfaces;

namespace OnlineStoreManagement.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
        {
            _logger.LogInformation("Fetching orders for customer with ID {customerId}", customerId);
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
