using Application.Servicies;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TransactionCommands
{
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand>
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHashService _passwordHashService;
        public AddTransactionCommandHandler(IUnitOfWork unitOfWork, IRepository<Transaction> transactionRepository, IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var senderUser = await _userRepository.FindByEmailAsync(request.SenderEmail);

            if(senderUser == null)
                throw new Exception("Sender email or password is not correct");

            var verifiedPassword = _passwordHashService.VerifyPassword(request.Password, senderUser.HashedPassword);

            if(!verifiedPassword)
                throw new Exception("Sender email or password is not correct");

            var receiverUser = _userRepository.FindByEmailAsync(request.ReceiverEmail);

            if (receiverUser == null)
                throw new Exception("Receiver email doesn't not exists");

            Transaction transaction = new()
            {
                SenderUserId = senderUser.Id,
                ReceiverUserId = receiverUser.Id,
                Amount = request.Amount,
                TimestampUTC = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CommitAsync();
        }
    }
}
