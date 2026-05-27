using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<List<User>>;
}