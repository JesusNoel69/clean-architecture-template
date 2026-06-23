using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdTrackedAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpSertAsync(T entity);
        Task DeleteAsync(T entity);
    }
}