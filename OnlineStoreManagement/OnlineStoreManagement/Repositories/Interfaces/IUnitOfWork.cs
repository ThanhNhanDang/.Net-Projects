namespace OnlineStoreManagement.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        Task<int> CompleteAsync();
    }
}
