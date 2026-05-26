using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler(
         IAuthService authService)
        : IRequestHandler<RevokeTokenCommand, Unit>
    {
        private readonly IAuthService _authService = authService;
        public async Task<Unit> Handle(
            RevokeTokenCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.RevokeTokenAsync(request.RefreshToken);
            return Unit.Value;
        }
    }
}