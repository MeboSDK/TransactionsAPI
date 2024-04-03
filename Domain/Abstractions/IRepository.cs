using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;
public interface IRepository<T> where T : class
{
    void AddAsync(T entity);
    void DeleteAsync(T entity);
    Task<T> GetByIdAsync(int id);
}
