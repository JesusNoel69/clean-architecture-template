using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetWorkItemById
{
    public record GetWorkItemByIdQuery(int Id) : IRequest<WorkItemDto>;
}