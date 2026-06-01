using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class WorkItem : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        //public WorkItemPriority Priority { get; private set; }
        //public WorkItemStatus Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public string UserId { get; private set; }
        /*public void Complete()
        {
            Status = WorkItemStatus.Completed;
        }
        public void ChangePriority(WorkItemPriority priority)
        {
            Priority = priority;
        }*/
    }
}