using System.Linq;
using Domain.Utils.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IRepository<T, TKey> where T : AggregateRoot<TKey> where TKey : notnull
    {
        Task<T> CreateAsync(T entity, bool saveChanges = true);
        Task<List<T>> CreateRangeAsync(List<T> entities, bool saveChanges = true);
        Task UpdateAsync(T entity, bool saveChanges = true);
        Task UpdateRangeAsync(IEnumerable<T> obj, bool saveChanges = true);
        Task DeleteAsync(T entity, bool saveChanges = true);
        Task DeleteRangeAsync (IEnumerable<T> entity, bool saveChanges = true);
        Task SaveChangesAsync();
        IQueryable<T> GetQueryable();
    }
}