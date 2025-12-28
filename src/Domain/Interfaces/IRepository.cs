using Core.DomainObjects;
using Core.Models;
using System.Linq.Expressions;

namespace Equinox.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity?> GetByIdAsync(Guid id);

        Task<PagedResult<TEntity>> GetAllAsync(int pageSize, int pageIndex);

        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity obj);

        void Update(TEntity obj);

        void Remove(TEntity obj);

        Task<int> SaveChangesAsync();
    }
}
