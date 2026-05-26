using CleanArchitecture.Application.Models.Identity;
namespace CleanArchitecture.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<AuthResponse> RefreshTokenAsync(string token);
        Task RevokeTokenAsync(string token);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        
    }
}