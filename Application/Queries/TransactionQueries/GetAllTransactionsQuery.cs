using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TransactionQueries;

public record GetAllTransactionsQuery(Expression<Func<Transaction,bool>> filter = null) : IRequest<IEnumerable<Transaction>>;
