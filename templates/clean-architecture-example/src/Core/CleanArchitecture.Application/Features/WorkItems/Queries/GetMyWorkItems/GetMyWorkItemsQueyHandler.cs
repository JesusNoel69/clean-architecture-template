using AutoMapper;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using MediatR;

namespace CleanArchitecture.Application.Features.WorkItems.Queries.GetMyWorkItems
{
    public class GetMyWorkItemsQueryHandler(IWorkItemRepository workItemRepository, IMapper mapper, IUserService userService) : IRequestHandler<GetMyWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemRepository _workItemRepository = workItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        public async Task<List<WorkItemDto>> Handle(GetMyWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var itemWork = await _workItemRepository.GetByUserId(_userService.UserId);
            return _mapper.Map<List<WorkItemDto>>(itemWork);
        }
    }
}