using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler(IMapper mapper, IWorkItemRepository workItemRepository)
        : IRequestHandler<CreateWorkItemCommand, WorkItemDto>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;

        public async Task<WorkItemDto> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var exists = await _workItemRepository.WorkItemExists(request.UserId, request.Title);
            
            if (exists)
            {
                throw new BadRequestException("A work item with this title already exists.");
            }

            var workItem = _mapper.Map<WorkItem>(request);

            await _workItemRepository.CreateAsync(workItem);

            return _mapper.Map<WorkItemDto>(workItem);
            
        }
    }
}