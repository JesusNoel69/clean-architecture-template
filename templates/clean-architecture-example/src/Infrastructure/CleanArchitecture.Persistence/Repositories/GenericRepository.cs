using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context = context;

        public async Task CreatedAsync(T entity)
        {
            await _context.AddAsync(entity);
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
                .AsNoTracking()
                .FirstOrDefaultAsync(q=>q.Id==id);
        }

        public async Task UpdatedAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpSertAsync(List<T> entities)
        {
            entities.ForEach(entity => 
                _context.Entry(entity).State = EntityState.Modified);
            await _context.SaveChangesAsync();

        }
    }
}