using AutoMapper;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetPendingWorkItems
{
    public class GetPendingWorkItemsQueryHandler(IWorkItemRepository workItemRepository, IMapper mapper) : IRequestHandler<GetPendingWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<WorkItemDto>> Handle(GetPendingWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemRepository.GetPendingByUserId(request.UserId);
            return _mapper.Map<List<WorkItemDto>>(workItem);
        }
    }
}