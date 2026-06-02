using AutoMapper;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetMyWorkItems
{
    public class GetMyWorkItemsQueryHandler(IWorkItemRepository workItemRepository, IMapper mapper) : IRequestHandler<GetMyWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<WorkItemDto>> Handle(GetMyWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var itemWork = await _workItemRepository.GetByUserId(request.UserId);
            return _mapper.Map<List<WorkItemDto>>(itemWork);
        }
    }
}