using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUserService _userService) : IRequestHandler<GetUsersQuery, List<User>>
    {
        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
           return await _userService.GetUsers();
        }
    }
}