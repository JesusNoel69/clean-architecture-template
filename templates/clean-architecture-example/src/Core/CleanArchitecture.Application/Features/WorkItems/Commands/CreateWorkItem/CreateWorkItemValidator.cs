using FluentValidation;

namespace CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandValidator: AbstractValidator<CreateWorkItemCommand>
    {
        public CreateWorkItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }
    }
}