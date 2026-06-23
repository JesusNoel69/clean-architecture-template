using CleanArchitecture.Application.Features.WorkItems.Commands.ChangePriority;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.UnitTests.Common.Mocks
{
    public static class WorkItemMockData
    {
        public static WorkItem GetWorkItem(WorkItemPriority? priority)
        {
            return new WorkItem(
                "Task",
                "Description",
                "user1",
                priority?? WorkItemPriority.Medium);
        }

        public static List<WorkItem> GetWorkItems()
        {
            return
            [
                GetWorkItem(null),
                new WorkItem(
                    "Task 2",
                    "Description 2",
                    "user2",
                    WorkItemPriority.High)
            ];
        }
    }
}