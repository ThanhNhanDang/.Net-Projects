using OnlineStoreManagement.Data;
using OnlineStoreManagement.Entities;
using OnlineStoreManagement.Repositories.Interfaces;

namespace OnlineStoreManagement.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
