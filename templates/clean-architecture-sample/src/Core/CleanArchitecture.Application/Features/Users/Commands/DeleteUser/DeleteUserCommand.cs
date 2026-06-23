using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}