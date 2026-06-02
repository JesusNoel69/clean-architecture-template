using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Interfaces.Persistence
{
    public interface IWorkItemRepository : IGenericRepository<WorkItem>
    {
        Task Addworkitems(List<WorkItem> workItems);
        Task<bool> WorkItemExists(string userId, string title);
        Task<List<WorkItem>> GetByUserId(string userId);
        Task<List<WorkItem>> GetPendingByUserId(string userId);
        Task<List<WorkItem>> GetOverdueByUserId(string userId);
        Task<List<WorkItem>> GetWorkItemsWithDetails();        
    }
}