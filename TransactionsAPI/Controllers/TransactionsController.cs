using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Queries.TransactionQueries;
using TransactionsAPI.Models.TransactionModels;
using Application.Commands.TransactionCommands;
using TransactionsAPI.Abstractions;
using FluentValidation;

namespace TransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionsService _transactionsService;
        private readonly IValidator<AddTransactionCommand> _validator;
        public TransactionsController(IMediator mediator, ITransactionsService transactionsService, IValidator<AddTransactionCommand> validator = null)
        {
            _mediator = mediator;
            _transactionsService = transactionsService;
            _validator = validator;
        }

        [HttpGet]
        [Route("GetTransactions")]
        public async Task<IEnumerable<ReadTransactionModel>> GetTransactions()
        {
            return await _transactionsService.GetAllTransactionsAsync();
        }

        [HttpPost]
        [Route("CreateTransaction")]
        public async Task<IResult> CreateTransaction(CreateTransactionModel model)
        {

                AddTransactionCommand command = new(model.SenderEmail,
                                                              model.Password,
                                                              model.ReceiverEmail,
                                                              model.Amount);

                var result = await _validator.ValidateAsync(command);

                if (!result.IsValid)
                    return Results.ValidationProblem(result.ToDictionary());

                await _mediator.Send(command);

                return Results.Ok();
       
        }
    }
}
