using System.Security.Claims;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Services
{
     public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        //public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

        public string? UserId =>
            _contextAccessor.HttpContext?
                .User?
                .FindFirstValue(CustomClaimTypes.Uid);
        public async Task<User> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("The user with id ", userId);
            }
            return new User
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            return users.Select(q => new User
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName
            }).ToList();
        }
    }
}