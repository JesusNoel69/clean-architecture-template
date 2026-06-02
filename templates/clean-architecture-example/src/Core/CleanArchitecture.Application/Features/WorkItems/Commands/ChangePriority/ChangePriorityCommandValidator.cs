using FluentValidation;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.ChangePriority
{
    public class ChangePriorityCommandValidator : AbstractValidator<ChangePriorityCommand>
    {
        public ChangePriorityCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}