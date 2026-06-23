namespace CleanArchitecture.Application.Models.Identity
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}