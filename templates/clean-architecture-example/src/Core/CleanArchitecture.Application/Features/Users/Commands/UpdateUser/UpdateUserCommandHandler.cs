using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(
        IUserService userService)
        : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserService _userService = userService;

        public async Task<User> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(
                new UpdateUserRequest
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                });
        }
    }
}