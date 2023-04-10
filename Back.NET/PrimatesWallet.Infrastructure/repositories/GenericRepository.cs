using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;

namespace PrimatesWallet.Infrastructure.repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        protected GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Update(entity).Property("IsDeleted").CurrentValue = true;    
        }

        public void RealDelete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Activate(T entity)
        {
            _dbContext.Set<T>().Update(entity).Property("IsDeleted").CurrentValue = false;
        }

        public virtual async Task<T> GetByIdDeleted(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
