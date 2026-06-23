using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IAuthService authService)
        : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IAuthService _authService = authService;
        public async Task<AuthResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}