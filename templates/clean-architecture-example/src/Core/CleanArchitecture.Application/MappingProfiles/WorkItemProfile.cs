using AutoMapper;
using CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem;
using CleanArchitecture.Application.Features.WorkItems.Commands.UpdateWorkItem;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.MappingProfiles
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<CreateWorkItemCommand, WorkItem>();
            CreateMap<UpdateWorkItemCommand, WorkItem>();
            CreateMap<WorkItem, WorkItemDto>().ReverseMap();
        }
    }
}