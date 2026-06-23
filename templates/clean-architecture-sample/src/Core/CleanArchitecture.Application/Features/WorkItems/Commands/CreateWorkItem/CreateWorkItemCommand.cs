using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommand : IRequest<WorkItemDto>
    {
        public string UserId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public WorkItemPriority Priority { get; set; }
        public WorkItemStatus Status { get; set; }
    }
}