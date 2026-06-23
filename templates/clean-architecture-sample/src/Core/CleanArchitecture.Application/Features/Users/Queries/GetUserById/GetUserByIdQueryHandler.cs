using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUserService _userService) : IRequestHandler<GetUserByIdQuery, User>
    {
        public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUser(request.Id);

            if (user is null)
            {
                throw new NotFoundException("User",request.Id);
            }
            return user;
        }
    }
}