using OnlineStoreManagement.Entities;

namespace OnlineStoreManagement.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId);
    }
}
