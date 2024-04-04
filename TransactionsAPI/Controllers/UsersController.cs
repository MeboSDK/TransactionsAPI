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
    private readonly IMediator _mediatR;
    public UsersController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    [HttpGet]
    public async Task<ReadUserModel> GetUser(int id)
    {
        GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery(id);

        var user = await _mediatR.Send(getUserByIdQuery);

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
        GetAllUserQuery getAllUserQuery = new GetAllUserQuery();

        var users = await _mediatR.Send(getAllUserQuery);

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


    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Create(CreateUserModel model)
    {
        try
        {
            AddUserCommand addUserCommand = new AddUserCommand(model.FirstName,
                                                               model.LastName,
                                                               model.Email,
                                                               model.Password);

            await _mediatR.Send(addUserCommand);

            return Ok();
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(id);

            await _mediatR.Send(deleteUserCommand);

            return Ok();
        }
        catch(Exception e)
        {
            return Problem(e.Message);
        }
    }
}
