using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Models.WorkItem
{
    public class WorkItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public WorkItemPriority Priority { get; set; }
        public WorkItemStatus Status { get; set; }
        public string UserId { get; set; } = default!;
    }
}