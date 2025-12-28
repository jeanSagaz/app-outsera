using Core.DomainObjects;
using Core.Models;
using Equinox.Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApplicationContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id) =>
            await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public virtual async Task<PagedResult<TEntity>> GetAllAsync(int pageSize, int pageIndex)
        {
            var count = 0;
            var list = new List<TEntity>();

            count = DbSet.AsNoTracking().Count();
            list = await DbSet.AsNoTracking().Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

            return new PagedResult<TEntity>()
            {
                List = list,
                TotalResults = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.AsNoTracking().Where(predicate).ToListAsync();

        public virtual async Task AddAsync(TEntity obj) =>
            await DbSet.AddAsync(obj);

        public virtual void Update(TEntity obj) =>
            DbSet.Update(obj);

        public virtual async Task<int> SaveChangesAsync() =>        
            await Db.SaveChangesAsync();

        public virtual void Remove(TEntity obj) =>        
            DbSet.Remove(obj);

        public void Dispose() =>
            Db.Dispose();
    }
}
