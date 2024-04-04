using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TransactionCommands
{
    public record AddTransactionCommand(string SenderEmail,string Password,string ReceiverEmail,decimal Amount) : IRequest;
    
}
