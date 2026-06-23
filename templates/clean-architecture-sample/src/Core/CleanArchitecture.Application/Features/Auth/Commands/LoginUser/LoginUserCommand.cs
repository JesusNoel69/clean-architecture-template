using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}