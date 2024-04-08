using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Servicies.Repositories;

public class UserRepository(TransactionDBContext transactionDbContext) : Repository<User>(transactionDbContext), IUserRepository
{
    public Task<User> FindByEmailAsync(string email)
    {
        return _dbContext.Users.FirstOrDefaultAsync(o => o.Email == email);
    }

    public Task<bool> IsEmailUniqueAsync(string email)
    {
        return _dbContext.Users.AllAsync(u => u.Email != email);
    }
}
