using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler(IMapper mapper, IWorkItemRepository workItemRepository) : IRequestHandler<UpdateWorkItemCommand, WorkItemDto>
    { private readonly IMapper _mapper = mapper;
    private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        public async Task<WorkItemDto> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetByIdAsync(request.Id);
            if (workItem is null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }
            _mapper.Map(request, workItem);
            await _workItemRepository.UpdateAsync(workItem);
            return _mapper.Map<WorkItemDto>(workItem);
        }
    }
}