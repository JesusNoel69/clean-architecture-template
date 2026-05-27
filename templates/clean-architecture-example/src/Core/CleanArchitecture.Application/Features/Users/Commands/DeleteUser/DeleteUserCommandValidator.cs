using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator
        : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}