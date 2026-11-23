namespace Zwemclub.Data.Repository
{
    public class GenericRepository<TEntity>(ZwemclubContext context) : IGenericRepository<TEntity>
        where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync<TId>(TId id) where TId : notnull
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync<TId>(TId id) where TId : notnull
        {
            return await context.Set<TEntity>().FindAsync(id) != null;
        }

        public void RemoveRange(List<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
