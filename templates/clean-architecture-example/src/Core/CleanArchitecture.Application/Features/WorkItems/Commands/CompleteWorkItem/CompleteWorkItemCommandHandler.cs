using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CompleteWorkItem
{
    public class CompleteWorkItemCommandHandler(
        IWorkItemRepository workItemRepository)
        : IRequestHandler<CompleteWorkItemCommand, Unit>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;

        public async Task<Unit> Handle(CompleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);

            if (workItem is null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }
            workItem.Complete();

            await _workItemRepository.UpdateAsync(workItem);
            return Unit.Value;
        }
    }
}