using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}