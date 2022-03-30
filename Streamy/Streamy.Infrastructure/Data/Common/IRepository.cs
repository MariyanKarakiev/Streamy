using System.Linq.Expressions;

namespace Streamy.Infrastructure.Data.Common
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> All<TEntity>() where TEntity : class;

        IQueryable<TEntity> All<TEntity>(Expression<Func<TEntity, bool>> search) where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class;

        Task<TEntity> GetByIdsAsync<TEntity>(object[] id) where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        Task DeleteAsync<TEntity>(object id) where TEntity : class;

        void Detach<T>(T entity) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
