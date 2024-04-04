using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Servicies.Repositories
{
    public class UserRepository(TransactionDBContext transactionDbContext) : Repository<User>(transactionDbContext), IUserRepository
    {
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(o => o.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _dbContext.Users.AllAsync(u => u.Email != email);
        }
    }
}
