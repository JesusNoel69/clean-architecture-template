using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain
{
    public class WorkItem : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public WorkItemPriority Priority { get; private set; }
        public WorkItemStatus Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        private WorkItem() { }
        public WorkItem(
            string title,
            string description,
            string userId,
            WorkItemPriority priority)
        {
            Title = title;
            Description = description;
            UserId = userId;
            Priority = priority;

            Status = WorkItemStatus.Pending;
        }
         public void Complete()
        {
            if (Status == WorkItemStatus.Completed)
                throw new InvalidOperationException("Work item already completed.");

            Status = WorkItemStatus.Completed;
        }

        public void Start()
        {
            if (Status == WorkItemStatus.Completed)
                throw new InvalidOperationException("Completed work item cannot be started.");

            Status = WorkItemStatus.InProgress;
        }

        public void Cancel()
        {
            if (Status == WorkItemStatus.Completed)
                throw new InvalidOperationException("Completed work item cannot be cancelled.");

            Status = WorkItemStatus.Cancelled;
        }

        public void ChangePriority(WorkItemPriority priority)
        {
            Priority = priority;
        }
    }
}