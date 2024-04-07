using Application.Commands.UserCommands.Commands;
using Domain.Abstractions;
using FluentValidation;


namespace Application.Commands.UserCommands.Validators;

public sealed class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(o => o.Email).MustAsync(async (email, _) =>
        {
            return await userRepository.IsEmailUniqueAsync(email);
        }).WithMessage("Email exists.");
    }
}
