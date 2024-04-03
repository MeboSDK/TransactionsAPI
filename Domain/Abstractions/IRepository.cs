﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Delete(T entity);
    Task<T> GetByIdAsync(int id);
}
