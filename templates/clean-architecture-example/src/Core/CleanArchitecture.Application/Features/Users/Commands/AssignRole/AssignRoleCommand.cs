using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchitecture.Application.Features.Users.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}