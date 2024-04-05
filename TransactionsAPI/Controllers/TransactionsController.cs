using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Queries.TransactionQueries;
using TransactionsAPI.Models.TransactionModels;
using Application.Commands.TransactionCommands;
using TransactionsAPI.Abstractions;

namespace TransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(IMediator mediator, ITransactionsService transactionsService)
        {
            _mediator = mediator;
            _transactionsService = transactionsService;
        }

        [HttpGet]
        [Route("GetTransactions")]
        public async Task<IEnumerable<ReadTransactionModel>> GetTransactions()
        {
            return await _transactionsService.GetAllTransactionsAsync();
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
