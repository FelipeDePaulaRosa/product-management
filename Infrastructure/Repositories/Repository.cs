using System.Linq;
using Domain.Contracts;
using Domain.Utils.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> 
        where T : AggregateRoot<TKey> where TKey : notnull
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> DbSet;
        protected readonly IQueryable<T> DbSetNt;

        protected Repository(DbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
            DbSetNt = DbSet.AsNoTracking();
        }
        
        public async Task<T> CreateAsync(T entity, bool saveChanges = true)
        {
            await DbSet.AddAsync(entity);

            if (saveChanges)
                await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> CreateRangeAsync(List<T> entities, bool saveChanges = true)
        {
            await DbSet.AddRangeAsync(entities);
        
            if(saveChanges)
                await _context.SaveChangesAsync();
        
            return entities;
        }

        public async Task UpdateAsync(T entity, bool saveChanges = true)
        {
            DbSet.Update(entity);
        
            if(saveChanges)
                await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            DbSet.UpdateRange(entities);
        
            if(saveChanges)
                await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            DbSet.Remove(entity);
        
            if(saveChanges)
                await _context.SaveChangesAsync();
        }
    
        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            DbSet.RemoveRange(entities);
        
            if(saveChanges)
                await _context.SaveChangesAsync();
        }
    
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}