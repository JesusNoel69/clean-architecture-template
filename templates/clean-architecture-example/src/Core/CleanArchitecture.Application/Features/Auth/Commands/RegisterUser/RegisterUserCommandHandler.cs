using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IAuthService authService) 
        : IRequestHandler<RegisterUserCommand, RegistrationResponse>
    {
        private readonly IAuthService _authService = authService;
        public async Task<RegistrationResponse> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.Register(new RegistrationRequest
            {
                Email=request.Email,
                Password=request.Password,
                FirstName=request.FirstName,
                LastName=request.LastName,
                UserName=request.UserName,
            });
        }
    }
}