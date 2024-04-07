using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using Application.Queries.UserQueries;
using Domain.Entities;
using TransactionsAPI.Models.UserModels;
using FluentValidation;
using Application.Commands.UserCommands.Commands;
namespace TransactionsAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddUserCommand> _validator;

    public UsersController(IMediator mediator,IValidator<AddUserCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet("{id}")]
    public async Task<ReadUserModel> GetUser(int id)
    {
        GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery(id);

        var user = await _mediator.Send(getUserByIdQuery);

        if (user is null)
            return default;

        ReadUserModel userModel = new ReadUserModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return userModel;

    }

    [HttpGet]
    public async Task<IEnumerable<ReadUserModel>> GetUsers()
    {
        GetAllUsersQuery getAllUserQuery = new GetAllUsersQuery();

        var users = await _mediator.Send(getAllUserQuery);

        if (users is null)
            return default;

        List<ReadUserModel> userModels = new List<ReadUserModel>();

        foreach (var user in users)
        {
            ReadUserModel userModel = new ReadUserModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            userModels.Add(userModel);
        }

        return userModels;
    }


    [HttpPost]
    public async Task<IResult> CreateUser(CreateUserModel model)
    {

            AddUserCommand command = new AddUserCommand(model.FirstName,
                                                               model.LastName,
                                                               model.Email,
                                                               model.Password);

            var result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            await _mediator.Send(command);

            return Results.Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(id);

            await _mediator.Send(deleteUserCommand);

            return Ok();
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
}
