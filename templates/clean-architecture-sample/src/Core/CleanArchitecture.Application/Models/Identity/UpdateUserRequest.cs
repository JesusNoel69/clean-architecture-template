namespace CleanArchitecture.Application.Models.Identity
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}