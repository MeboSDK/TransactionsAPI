using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IRepository <T> where T : class
    {
        void Insert(T entity);
        void Delete(T entity);
        T Read(int Id);
    }
}
