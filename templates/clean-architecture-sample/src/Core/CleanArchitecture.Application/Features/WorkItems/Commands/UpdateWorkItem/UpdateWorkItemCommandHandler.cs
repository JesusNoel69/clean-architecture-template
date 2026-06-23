using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler(IMapper mapper, IWorkItemRepository workItemRepository, IAppLogger<UpdateWorkItemCommandHandler> logger) : IRequestHandler<UpdateWorkItemCommand, WorkItemDto>
    { 
        private readonly IMapper _mapper = mapper;
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IAppLogger<UpdateWorkItemCommandHandler> _logger = logger;

        public async Task<WorkItemDto> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);
            if (workItem is null)
            {
                _logger.LogWarning("Update failed. WorkItem {WorkItemId} not found", request.Id);
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }
            _mapper.Map(request, workItem);
            await _workItemRepository.UpdateAsync(workItem);
            _logger.LogInformation("WorkItem {WorkItemId} updated", workItem.Id);
            return _mapper.Map<WorkItemDto>(workItem);
        }
    }
}