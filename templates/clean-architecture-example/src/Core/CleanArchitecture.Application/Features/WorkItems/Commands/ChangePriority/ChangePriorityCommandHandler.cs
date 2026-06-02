using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.ChangePriority
{
    public class ChangePriorityCommandHandler(IWorkItemRepository workItemRepository) : IRequestHandler<ChangePriorityCommand, Unit>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;

        public async Task<Unit> Handle(ChangePriorityCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);

            if (workItem is null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }

            workItem.ChangePriority(request.Priority);

            await _workItemRepository.UpdateAsync(workItem);
            return Unit.Value;
        }
    }
}