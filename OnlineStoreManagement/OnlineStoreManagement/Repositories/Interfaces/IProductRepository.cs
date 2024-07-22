using OnlineStoreManagement.Entities;

namespace OnlineStoreManagement.Repositories.Interfaces
{
    // Interface cụ thể cho Product repository
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    }
}
