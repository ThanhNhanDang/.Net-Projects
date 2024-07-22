using OnlineStoreManagement.Entities;

namespace OnlineStoreManagement.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId);
    }
}
