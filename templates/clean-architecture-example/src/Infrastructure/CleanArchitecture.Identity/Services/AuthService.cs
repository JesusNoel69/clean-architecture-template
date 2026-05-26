using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using CleanArchitecture.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService(
    UserManager<ApplicationUser> userManager,
    IOptions<JwtSettings> jwtSettings,
    SignInManager<ApplicationUser> signInManager,
    IdentityDbContext context) : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IdentityDbContext _context = context;
        
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User Not Found.");
                //throw new NotFoundException($"User {request.Email} not found", request.Email);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded == false)
            {
                throw new Exception("Crdentials for email are not valid.");
                //throw new BadRequestException($"Credentials for {request.Email} are not valid.");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
            return response;
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(
                    System.Security.Cryptography.RandomNumberGenerator.GetBytes(64)
                ),

                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token)
        {
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u =>
                    u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new Exception("Invalid token.");
            }

            var refreshToken = user.RefreshTokens
                .Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                throw new Exception("Inactive token.");
            }

            // revoke old token
            refreshToken.Revoked = DateTime.UtcNow;

            // generate new refresh token
            var newRefreshToken = GenerateRefreshToken();

            refreshToken.ReplacedByToken = newRefreshToken.Token;

            user.RefreshTokens.Add(newRefreshToken);

            await _context.SaveChangesAsync();

            // generate new jwt
            var jwtToken = await GenerateToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(jwtToken),

                RefreshToken = newRefreshToken.Token
            };
        }
        public async Task RevokeTokenAsync(string token)
        {
            var user = await _context.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u =>
                    u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new Exception("Token not found.");
            }

            var refreshToken = user.RefreshTokens
                .Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                throw new Exception("Token already inactive.");
            }

            refreshToken.Revoked = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCreentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signinCreentials
            );
            return jwtSecurityToken;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Employee);
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    str.AppendFormat("*{0}\n", error.Description);
                }
                throw new Exception($"{string.Join("\n", result.Errors.Select(e => e.Description))}");
                //throw new BadRequestException($"{string.Join("",result.Errors)}");
            }
        }
        
    }
}