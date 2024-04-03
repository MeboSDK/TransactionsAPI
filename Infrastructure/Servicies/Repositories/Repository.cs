using Domain.Abstractions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Servicies.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly TransactionDBContext _dbContext;

    public Repository(TransactionDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
