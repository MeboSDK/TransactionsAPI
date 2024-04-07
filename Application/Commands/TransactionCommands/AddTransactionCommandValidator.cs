using Application.Servicies;
using Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Application.Commands.TransactionCommands;

public sealed class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
{
    public AddTransactionCommandValidator(IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        //I am working on this 

        RuleFor(o => o)
            .MustAsync(async (ob, _) => {
                var sender = await userRepository.FindByEmailAsync(ob.SenderEmail);

                if (sender == null)
                    return false;

                var verifiedPassword = passwordHashService.VerifyPassword(ob.Password, sender.HashedPassword);

                if (!verifiedPassword)
                    return false;

                return true;
            }).WithMessage("Sender email or password is not correct.");


        RuleFor(o => o.ReceiverEmail)
                 .MustAsync(async (email, _) => await userRepository.FindByEmailAsync(email) != null)
                 .WithMessage("Receiver email doesn't exist.");


        RuleFor(x => x.SenderEmail)
               .NotEqual(x => x.ReceiverEmail)
               .WithMessage("You can't send money to yourself!");

    }
}
