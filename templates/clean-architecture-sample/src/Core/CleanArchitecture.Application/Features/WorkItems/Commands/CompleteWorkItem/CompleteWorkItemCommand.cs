using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CompleteWorkItem
{
    public class CompleteWorkItemCommand : IRequest<Unit>
    {
        public int Id {get; set;}
    }
}