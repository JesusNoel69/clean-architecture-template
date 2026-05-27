using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string Id) : IRequest<User>;
}