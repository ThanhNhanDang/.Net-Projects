using OnlineStoreManagement.Data;
using OnlineStoreManagement.Repositories.Interfaces;

namespace OnlineStoreManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /* 
        UnitOfWork quản lý tất cả các repository và
        đảm bảo rằng tất cả các thay đổi được lưu vào database trong một giao dịch duy nhất.
         */
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ProductRepository> _loggerProduct;
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public ICustomerRepository Customers { get; private set; }



        public UnitOfWork(ApplicationDbContext context, ILogger<ProductRepository> loggerProduct)
        {
            _context = context;
            _loggerProduct = loggerProduct;
            Products = new ProductRepository(_context, _loggerProduct);
            Orders = new OrderRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
