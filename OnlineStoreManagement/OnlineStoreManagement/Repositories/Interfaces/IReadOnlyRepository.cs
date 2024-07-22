namespace OnlineStoreManagement.Repositories.Interfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }

}
