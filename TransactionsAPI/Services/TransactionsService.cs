using Application.Queries.TransactionQueries;
using Application.Queries.UserQueries;
using MediatR;
using TransactionsAPI.Abstractions;
using TransactionsAPI.Models.TransactionModels;

namespace TransactionsAPI.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IMediator _mediator;
        public TransactionsService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IEnumerable<ReadTransactionModel>> GetAllTransactionsAsync()
        {
            GetAllTransactionsQuery getAllTransactionsQuery = new GetAllTransactionsQuery();

            var transactions = await _mediator.Send(getAllTransactionsQuery);

            if (transactions is null)
                return default;

            List<ReadTransactionModel> transactionsModels = new();

            foreach (var transaction in transactions)
            {
                GetUserByIdQuery getSenderUserByIdQuery = new GetUserByIdQuery(transaction.SenderUserId);

                var senderUser = await _mediator.Send(getSenderUserByIdQuery);

                GetUserByIdQuery getReceiverUserByIdQuery = new GetUserByIdQuery(transaction.ReceiverUserId);

                var receiverUser = await _mediator.Send(getReceiverUserByIdQuery);

                ReadTransactionModel userModel = new ReadTransactionModel()
                {
                    Id = transaction.Id,
                    SenderEmail = senderUser.Email,
                    ReceiverEmail = receiverUser.Email,
                    Amount = transaction.Amount,
                    TimeStampUTC = transaction.TimestampUTC
                };

                transactionsModels.Add(userModel);
            }

            return transactionsModels;
        }
    }
}
