using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Queries.TransactionQueries;
using TransactionsAPI.Models.TransactionModels;
using Application.Commands.TransactionCommands;

namespace TransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetTransactions")]
        public async Task<IEnumerable<ReadTransactionModel>> GetTransactions()
        {
            GetAllTransactionsQuery getAllTransactionsQuery = new GetAllTransactionsQuery();

            var transactions = await _mediator.Send(getAllTransactionsQuery);

            if (transactions is null)
                return default;

            List<ReadTransactionModel> transactionsModels = new();

            foreach (var transaction in transactions)
            {
                ReadTransactionModel userModel = new ReadTransactionModel()
                {
                    Id = transaction.Id,
                    SenderEmail = transaction.Sender.FirstName,
                    ReceiverEmail = transaction.Receiver.Email,
                    Amount = transaction.Amount,
                    TimeStampUTC = transaction.TimestampUTC
                };

                transactionsModels.Add(userModel);
            }

            return transactionsModels;

        }

        [HttpPost]
        [Route("CreateTransaction")]
        public async Task<ActionResult> CreateTransaction(CreateTransactionModel model)
        {
            try
            {
                AddTransactionCommand addTransactionCommand = new(model.SenderEmail,
                                                              model.Password,
                                                              model.ReceiverEmail,
                                                              model.Amount);


                await _mediator.Send(addTransactionCommand);
                return Ok();
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
