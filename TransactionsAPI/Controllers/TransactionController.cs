using Microsoft.AspNetCore.Mvc;
using TransactionsAPI.Models;
using MediatR;
using Application.Commands;
using System.Collections.Generic;
namespace TransactionsAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediatR;
    public TransactionController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    [HttpGet("GetUser")]
    public IEnumerable<UserModel> Get()
    {
        List<UserModel> users = new List<UserModel>();
        users.Add(new UserModel() { FirstName = "Msda", LastName = "asdas", Email = "merabi@.c", Password = "asdas" });
        return users;
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> Create(UserModel model)
    {
        try
        {
            AddUserCommand addUserCommand = new AddUserCommand(model.FirstName, model.LastName, model.Email, model.Password);

            await _mediatR.Send(addUserCommand);

            return Ok();
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return Ok();
        }
        catch
        {
            return NotFound();
        }
    }
}
