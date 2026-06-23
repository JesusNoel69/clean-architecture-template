using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context = context;

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(q=>q.Id==id);
        }
        
        public async Task<T> GetByIdTrackedAsync(int id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(q=>q.Id==id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpSertAsync(T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
            if (existingEntity is not null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.Entry(existingEntity).State = EntityState.Modified;
            }
            else
            {
                await _context.Set<T>().AddAsync(entity); 
            }
            await _context.SaveChangesAsync();
        }
    }
}