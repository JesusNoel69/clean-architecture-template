using CleanArchitecture.Application.Models.Identity;
namespace CleanArchitecture.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(string userId);
        public string UserId { get; }
    }
}