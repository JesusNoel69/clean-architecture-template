using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CompleteWorkItem
{
    public class CompleteWorkItemCommandHandler(IWorkItemRepository workItemRepository, IAppLogger<CompleteWorkItemCommandHandler> logger)
        : IRequestHandler<CompleteWorkItemCommand, Unit>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IAppLogger<CompleteWorkItemCommandHandler> _logger = logger;
        
        public async Task<Unit> Handle(CompleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);

            if (workItem is null)
            {
                _logger.LogWarning("Complete failed. WorkItem {WorkItemId} not found", request.Id);
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }
            workItem.Complete();

            await _workItemRepository.UpdateAsync(workItem);
            _logger.LogInformation("WorkItem {WorkItemId} completed", workItem.Id);
            return Unit.Value;
        }
    }
}