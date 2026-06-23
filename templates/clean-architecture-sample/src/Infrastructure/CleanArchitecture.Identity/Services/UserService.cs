using System.Security.Claims;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Services
{
    public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IAppLogger<UserService> logger) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly IAppLogger<UserService> _logger = logger;
        public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimTypes.Uid);
        private static readonly HashSet<string> ValidRoles = [Roles.Administrator, Roles.Employee];

       public async Task AssignRole(string userId, string role)
        {
            _logger.LogInformation(
                "Assigning role {Role} to user {UserId}",
                role,
                userId);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning(
                    "Role assignment failed. User {UserId} not found",
                    userId);

                throw new NotFoundException("User", userId);
            }

            if (!ValidRoles.Contains(role))
            {
                _logger.LogWarning("Role assignment failed. Role {Role} not found", role);

                throw new NotFoundException("Role", role);
            }

            if (await _userManager.IsInRoleAsync(user, role))
            {
                _logger.LogWarning("User {UserId} already has role {Role}", userId, role);

                return;
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Role assignment failed for user {UserId}. Errors: {Errors}", userId, string.Join(", ", result.Errors.Select(x => x.Code)));

                throw new BadRequestException(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
            _logger.LogInformation("Role {Role} assigned successfully to user {UserId}", role, userId);
        }

        public async Task DeleteUser(string userId)
        {
            _logger.LogInformation("Deleting user {UserId}", userId);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Delete failed. User {UserId} not found", userId);

                throw new NotFoundException("User", userId);
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Delete failed for user {UserId}. Errors: {Errors}", userId, string.Join(", ", result.Errors.Select(x => x.Code)));

                throw new BadRequestException(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(x => x.Description)));
            }
            _logger.LogInformation("User {UserId} deleted successfully", userId);
        }

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

        public async Task<User> UpdateUser(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
            {
                _logger.LogWarning("Update failed. User {UserId} not found", request.Id);
                throw new NotFoundException("User", request.Id);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogWarning(
                    "Update failed for user {UserId}. Errors: {Errors}",
                    request.Id,
                    string.Join(", ", result.Errors.Select(x => x.Code)));
                throw new Exception(
                    string.Join("\n",
                        result.Errors.Select(x => x.Description)));
            }
            _logger.LogInformation("User {UserId} updated successfully", user.Id);
            return new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
            };
        }
    }
}