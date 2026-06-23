using CleanArchitecture.Application.Models.WorkItem;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetOverdueWorkItems
{
    public record GetOverdueWorkItemsQuery() : IRequest<List<WorkItemDto>>;
}