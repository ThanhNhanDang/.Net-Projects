namespace OnlineStoreManagement.Repositories.Interfaces
{
    public interface IWriteOnlyRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
