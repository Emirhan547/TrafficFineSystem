using System.Linq.Expressions;

namespace TrafficFineSystem.Data.Repositories.GenericRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<bool> AnyAsync( Expression<Func<TEntity, bool>> predicate);
    }
}
