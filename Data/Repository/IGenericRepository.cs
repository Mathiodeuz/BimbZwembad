namespace Zwemclub.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync<TId>(TId id) where TId : notnull;
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> ExistsAsync<TId>(TId id) where TId : notnull;
        void RemoveRange(List<TEntity> entities);
    }
}
