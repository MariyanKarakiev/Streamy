using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Streamy.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        public DbContext Context { get; set; }

        protected DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public IQueryable<TEntity> All<TEntity>() where TEntity : class
        {
            return DbSet<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> All<TEntity>(Expression<Func<TEntity, bool>> search) where TEntity : class
        {
            return DbSet<TEntity>().Where(search).AsQueryable();
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Context.AddAsync(entity);
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await Context.AddRangeAsync(entities);
        }

        public async Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            var entity = await GetByIdAsync<TEntity>(id);

            Delete(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            EntityEntry entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet<TEntity>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        public void Detach<T>(T entity) where T : class
        {
            EntityEntry entityEntry = Context.Entry(entity);

            entityEntry.State = EntityState.Detached;
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await DbSet<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdsAsync<TEntity>(object[] id) where TEntity : class
        {
            return await DbSet<TEntity>().FindAsync(id);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            DbSet<TEntity>().Update(entity);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
