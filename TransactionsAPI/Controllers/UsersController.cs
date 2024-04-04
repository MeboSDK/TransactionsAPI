using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using Application.Queries.UserQueries;
using Application.Commands.UserCommands;
using Domain.Entities;
using TransactionsAPI.Models.UserModels;
namespace TransactionsAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
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
    public async Task<ActionResult> CreateUser(CreateUserModel model)
    {
        try
        {
            AddUserCommand addUserCommand = new AddUserCommand(model.FirstName,
                                                               model.LastName,
                                                               model.Email,
                                                               model.Password);

            await _mediator.Send(addUserCommand);

            return Ok();
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
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
