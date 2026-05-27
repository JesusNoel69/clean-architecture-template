using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler(IUserService _userService, ICurrentUserService _currentUserService) : IRequestHandler<GetCurrentUserQuery, User>
    {
        public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException();
            }

            return await _userService.GetUser(userId);
        }
    }
}