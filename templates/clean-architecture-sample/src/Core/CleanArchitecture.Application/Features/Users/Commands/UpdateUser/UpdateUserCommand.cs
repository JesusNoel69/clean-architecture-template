using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<User>
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}