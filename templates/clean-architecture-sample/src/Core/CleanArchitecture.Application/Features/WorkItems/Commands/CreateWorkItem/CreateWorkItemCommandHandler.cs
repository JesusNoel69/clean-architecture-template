using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler(IMapper mapper, IWorkItemRepository workItemRepository, IAppLogger<CreateWorkItemCommandHandler> logger)
        : IRequestHandler<CreateWorkItemCommand, WorkItemDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IAppLogger<CreateWorkItemCommandHandler> _logger = logger;

        public async Task<WorkItemDto> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var exists = await _workItemRepository.WorkItemExists(request.UserId, request.Title);
            
            if (exists)
            {
                _logger.LogWarning("WorkItem creation failed. User {UserId} already has a work item with title {Title}", request.UserId, request.Title);
                throw new BadRequestException("A work item with this title already exists.");
            }

            var workItem = _mapper.Map<WorkItem>(request);

            await _workItemRepository.CreateAsync(workItem);

            _logger.LogInformation("WorkItem {WorkItemId} created for user {UserId}", workItem.Id, workItem.UserId);

            return _mapper.Map<WorkItemDto>(workItem);
            
        }
    }
}