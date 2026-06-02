using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetMyWorkItems
{
    public record GetMyWorkItemsQuery(string UserId) : IRequest<List<WorkItemDto>>;
}