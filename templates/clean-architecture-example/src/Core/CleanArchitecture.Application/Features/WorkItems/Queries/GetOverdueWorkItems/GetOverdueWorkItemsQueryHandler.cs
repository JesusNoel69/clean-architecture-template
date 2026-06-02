using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using AutoMapper;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetOverdueWorkItems
{
    public class GetOverdueWorkItemsQueryHandler(IWorkItemRepository workItemRepository, IMapper mapper) : IRequestHandler<GetOverdueWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<WorkItemDto>> Handle(GetOverdueWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var overDueWorkItems = await _workItemRepository.GetOverdueByUserId(request.UserId);
            return _mapper.Map<List<WorkItemDto>>(overDueWorkItems);
        }
    }
}