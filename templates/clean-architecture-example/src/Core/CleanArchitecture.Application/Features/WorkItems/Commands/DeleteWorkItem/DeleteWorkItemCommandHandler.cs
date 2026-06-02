using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommandHandler(IWorkItemRepository workItemRepository) : IRequestHandler<DeleteWorkItemCommand, Unit>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;

        public async Task<Unit> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);

            if (workItem is null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }

            await _workItemRepository.DeleteAsync(workItem);
            return Unit.Value;
        }
    }
}