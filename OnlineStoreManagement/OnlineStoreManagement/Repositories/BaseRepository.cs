using Microsoft.EntityFrameworkCore;
using OnlineStoreManagement.Data;
using OnlineStoreManagement.Repositories.Interfaces;
using Serilog;

namespace OnlineStoreManagement.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ApplicationDbContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all {entityName} from the database", typeof(T).Name);
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching {entityName} with ID {id} from the database", typeof(T).Name, id);
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            _logger.LogInformation("Adding new {entityName} to the database", typeof(T).Name);
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _logger.LogInformation("Updating {entityName} in the database", typeof(T).Name);
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting {entityName} with ID {id} from the database", typeof(T).Name, id);
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
