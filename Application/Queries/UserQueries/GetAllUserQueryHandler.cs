using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<User>>
    {
        private readonly IRepository<User> _userRepository;
        public GetAllUserQueryHandler(IRepository<User> userRepository,
                                     IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAllAsync(request.filter);
        }
    }
}
