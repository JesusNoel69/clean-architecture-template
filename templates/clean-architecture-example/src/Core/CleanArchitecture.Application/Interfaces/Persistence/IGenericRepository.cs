using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task CreatedAsync(T entity);
        Task UpdatedAsync(T entity);
        Task UpSertAsync(List<T> entity);
        Task DeleteAsync(T entity);
    }
}