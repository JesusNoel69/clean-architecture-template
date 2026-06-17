using CleanArchitecture.Application.Models.WorkItem;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetPendingWorkItems
{
    public record GetPendingWorkItemsQuery() : IRequest<List<WorkItemDto>>;
}