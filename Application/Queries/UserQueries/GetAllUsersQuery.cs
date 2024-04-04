using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries;

public record GetAllUsersQuery(Expression<Func<User, bool>> filter = null) : IRequest<IEnumerable<User>>;
