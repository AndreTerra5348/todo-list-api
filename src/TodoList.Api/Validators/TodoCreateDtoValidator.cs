using FluentValidation;
using TodoList.Api.Dtos;

namespace TodoList.Api.Validators
{
    public class TodoCreateDtoValidator : AbstractValidator<TodoCreateDto>
    {
        public TodoCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.UserId)
                .NotEmpty();

        }
    }
}